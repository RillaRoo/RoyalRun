using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chunk : MonoBehaviour
{
	[SerializeField] GameObject fencePrefab;
	[SerializeField] GameObject applePrefab;
	[SerializeField] GameObject coinPrefab;
	[SerializeField] List<int> lanes;

	[SerializeField] float appleChance = 0.6f;
	[SerializeField] float coinChance = 0.5f;
	[SerializeField] float coinConsecutiveChance = 0.5f;
	public int coinLane = -1;

	public GameObject pastChunk;

	List<int> availableLanes = new List<int> { 0, 1, 2 };


	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		SpawnCoin();
		SpawnFence();
		SpawnApple();
	}

	private int SelectLane()
	{
		int randomLaneIndex = Random.Range(0, availableLanes.Count);
		int selectedLane = availableLanes[randomLaneIndex];
		availableLanes.RemoveAt(randomLaneIndex);
		return selectedLane;
	}
	private int SpawnPrefab(GameObject prefab)
	{
		int selectedLane = SelectLane();
		Vector3 spawnPos = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
		Instantiate(prefab, spawnPos, Quaternion.identity, transform);
		return selectedLane;
	}

	private void SpawnConsecutiveCoin()
	{
		Vector3 spawnPos = new Vector3(lanes[coinLane], transform.position.y, transform.position.z);
		Instantiate(coinPrefab, spawnPos, Quaternion.identity, transform);
	}

	private int SpawnGoodies(GameObject prefab, float chance)
	{
		float spawnRandomizer = Random.Range(0f, 1f);
		if (availableLanes.Count <= 0)
		{
			return -1;
		}
		if (spawnRandomizer < chance)
		{
			return SpawnPrefab(prefab);
		}
		return -1;
	}
	private void SpawnFence()
	{
		int fencesToSpawn = Random.Range(0, availableLanes.Count);

		for (int i = 0; i < fencesToSpawn; i++)
		{
			SpawnPrefab(fencePrefab);
		}
	}

	private void SpawnApple()
	{
		SpawnGoodies(applePrefab, appleChance);
	}

	private void SpawnCoin()
	{
		if (pastChunk)
		{
			Chunk pastChunkScript = pastChunk.GetComponent<Chunk>();
			if (pastChunkScript.coinLane >= 0)
			{
				coinLane = pastChunkScript.coinLane;
				SpawnCoinAtLane();
			}
			else
			{
				coinLane = SpawnGoodies(coinPrefab, coinChance);
			}
		}
		else
		{
			coinLane = SpawnGoodies(coinPrefab, coinChance);
		}
	}

	private void SpawnCoinAtLane()
	{
		float spawnRandomizer = Random.Range(0f, 1f);

		if (spawnRandomizer < coinConsecutiveChance)
		{
			SpawnConsecutiveCoin();
			availableLanes.RemoveAt(coinLane);
			coinLane = -1;
		}

	}
}
