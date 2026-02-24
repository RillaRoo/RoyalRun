using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
	[SerializeField] GameObject[] obstaclePrefabs;
	[SerializeField] Transform obstacleParent;
	[SerializeField] float spawnerTime = 2f;
	[SerializeField] float spawnWidth = 4f;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		StartCoroutine(SpawnObstacles());
	}

	IEnumerator SpawnObstacles()
	{
		while (true)
		{
			GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
			Vector3 spawnPos = new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z);
			yield return new WaitForSeconds(spawnerTime);
			Instantiate(obstaclePrefab, spawnPos, Random.rotation, obstacleParent);
		}
	}
}
