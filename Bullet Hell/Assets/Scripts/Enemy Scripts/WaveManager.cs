using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//this script will spawn random enemies for a wave based on a theme type and point limit (and maybe also time limit)
public class WaveManager : MonoBehaviour
{
    private EnemySpawning enemySpawningScript;

    [SerializeField] int maxPointValue = 0;
    private int currentPointVal = 0;
    [SerializeField] int waveLevel = 0;
    [SerializeField] GameObject setSpawnPoint;

    //this should be a rectangle/square object with a collider
    [SerializeField] Collider2D spawnRange;

    [SerializeField] float timeBetweenSpawns = 0;
    private float currentTimer = 0;


    // Start is called before the first frame update
    void Start()
    {
        enemySpawningScript = GetComponent<EnemySpawning>();
    }

    private void FixedUpdate()
    {
        //spawn an enemy until we reach the max point value (might go over, could fix by making maxPointValue into maxPointValue-highestCostOfEnemy)
        if (currentPointVal < maxPointValue && currentTimer <= 0)
            currentPointVal += spawnBatch();

        currentTimer -= Time.fixedDeltaTime;
    }

    //gets a random location in the defined area (not working rn)
    private Transform getRandomLocation()
    {
        float randomX = Random.Range(spawnRange.bounds.min.x, spawnRange.bounds.max.x);

        float randomY = Random.Range(spawnRange.bounds.min.y, spawnRange.bounds.max.y);

        Vector3 temp = new Vector3(randomX, randomY, 0);

        Transform temp2 = spawnRange.transform;
        temp2.position = temp;

        return temp2;
    }

    private int spawnBatch()
    {
        int earthCost = 0;
        int iceCost = 0;
        int fireCost = 0;

        //pick a random number representing the availble earth enemies
        if (waveLevel >= 0)
        {
            earthCost += enemySpawningScript.spawnBasic().enemyPointValue;
            Instantiate(enemySpawningScript.spawnBasic().enemyPrefab, getRandomLocation());
            currentTimer = timeBetweenSpawns;
        }

        //pick a random number representing the availble ice enemies
        if (waveLevel >= 1)
        {
            earthCost += enemySpawningScript.spawnRandQuad().enemyPointValue;
            Instantiate(enemySpawningScript.spawnRandQuad().enemyPrefab, getRandomLocation());
            currentTimer = timeBetweenSpawns;
        }

        //pick a random number representing the availble fire enemies
        if (waveLevel >= 2)
        {
            earthCost += enemySpawningScript.spawnFlare().enemyPointValue;
            Instantiate(enemySpawningScript.spawnFlare().enemyPrefab, getRandomLocation());
            currentTimer = timeBetweenSpawns;
        }

        return earthCost+iceCost+fireCost;
    }
}
