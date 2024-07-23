using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyTypes;
    public Transform[] spawnLocations;
    public float initialSpawnDelay = 10f;
    public float minSpawnDelay = 5f;
    public float maxSpawnDelay = 10f;

    public float levelTime = 90f;
    public float level2Time = 140f;

    public float nextSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.time + initialSpawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            int spawnIndex = Random.Range(0, spawnLocations.Length);
            int enemyTypeIndex = Random.Range(0, enemyTypes.Length);

            Instantiate(enemyTypes[enemyTypeIndex], spawnLocations[spawnIndex].position, Quaternion.identity);

            float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            nextSpawnTime = Time.time + spawnDelay;
        }

        if (level2Time >= 0)
        {
            level2Time -= Time.deltaTime;
            levelTime -= Time.deltaTime;
        }

        if (levelTime <= 0)
        {
            minSpawnDelay = 3;
            maxSpawnDelay = 9;
        }

        if (level2Time <= 0)
        {
            minSpawnDelay = 1;
            maxSpawnDelay = 5;
        }

       
    }
}
