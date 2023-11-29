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

    //this should be a rectangle/square object with a collider
    [SerializeField] Collider2D topSpawnZone;
    [SerializeField] Collider2D leftSpawnZone;
    [SerializeField] Collider2D rightSpawnZone;

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

    //gets a random location in the defined area
    private Vector3 getRandomTopLocation()
    {
        float randomX = Random.Range(topSpawnZone.bounds.min.x, topSpawnZone.bounds.max.x);

        float randomY = Random.Range(topSpawnZone.bounds.min.y, topSpawnZone.bounds.max.y);

        return new Vector3(randomX, randomY, 0);
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
            Instantiate(enemySpawningScript.spawnBasic().enemyPrefab, getRandomTopLocation(), new Quaternion(0, 0, 180, 0));
            currentTimer = timeBetweenSpawns;
        }

        //pick a random number representing the availble ice enemies
        if (waveLevel >= 1)
        {
            earthCost += enemySpawningScript.spawnRandQuad().enemyPointValue;
            Instantiate(enemySpawningScript.spawnRandQuad().enemyPrefab, getRandomTopLocation(), new Quaternion(0, 0, 180, 0));
            currentTimer = timeBetweenSpawns;
        }

        //pick a random number representing the availble fire enemies
        if (waveLevel >= 2)
        {
            earthCost += enemySpawningScript.spawnFlare().enemyPointValue;
            Instantiate(enemySpawningScript.spawnFlare().enemyPrefab, getRandomTopLocation(), new Quaternion(0,0, 180, 0));
            currentTimer = timeBetweenSpawns;
        }

        return earthCost+iceCost+fireCost;
    }

    public void spawnWorld1()
    {

    }

    public void spawnWorld2()
    {

    }

    public void spawnWorld3()
    {

    }
}
