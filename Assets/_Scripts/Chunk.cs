using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] List<float> lanes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnFence();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnFence()
    {
        Vector3 spawnPos = new Vector3(Random.Range(0, lanes.Count),0,0);
        Debug.Log(spawnPos);
        Instantiate(fencePrefab, spawnPos, Quaternion.identity);
    }
}
