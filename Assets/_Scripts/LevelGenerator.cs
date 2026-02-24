using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	[SerializeField] GameObject floorPrefab;

	[SerializeField] int startingFloorTilesNumber = 12;
	[SerializeField] Transform floorTilesParent;
	[SerializeField] Vector3 instanceDistance;
	[SerializeField] float moveSpeed = 8f;

	//GameObject[] chunks = new GameObject[12];
	List<GameObject> chunks = new List<GameObject>();

	private float chunkLenght = 10f;
	private float spawnPositionZ = 0f;
	

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		SpawnStartingChunks();
	}

	private void SpawnStartingChunks()
	{
		for (int i = 0; i < startingFloorTilesNumber; i++)
		{
			SpawnChunk();
		}
	}
	// Update is called once per frame
	void Update()
	{
		MoveChunks();
	}

	private void SpawnChunk()
	{
		spawnPositionZ = CalculateSpawnPositionZ();
		Vector3 chunkSpawnPos = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);
		GameObject newChunk = Instantiate(floorPrefab, chunkSpawnPos, Quaternion.identity, floorTilesParent); //pôr o floorTilesParent no fim faz com que cada instancia passe a ser filha dele
		instanceDistance = instanceDistance + new Vector3(0, 0, 10);

		chunks.Add(newChunk);
	}

	private float CalculateSpawnPositionZ()
	{
		if (chunks.Count == 0)
		{
			spawnPositionZ = transform.position.z;
		}
		else
		{
			spawnPositionZ = chunks[chunks.Count - 1].transform.position.z + chunkLenght;
		}

		return spawnPositionZ;
	}

	private void MoveChunks() 
	{
		for (int i = 0; i < chunks.Count; i++)
		{
			GameObject chunk = chunks[i];
			chunk.transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));

			if (chunk.transform.position.z <= Camera.main.transform.transform.position.z - chunkLenght)
			{
				chunks.Remove(chunk);
				Destroy(chunk);

				SpawnChunk();
			}
			
		}
	}
}
