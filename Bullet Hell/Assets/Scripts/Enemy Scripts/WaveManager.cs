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

    //gets a random location at the top of the screen (based on what collider is passed though)
    private Vector3 getRandomTopLocation()
    {
        float randomX = Random.Range(topSpawnZone.bounds.min.x, topSpawnZone.bounds.max.x);

        float randomY = Random.Range(topSpawnZone.bounds.min.y, topSpawnZone.bounds.max.y);

        return new Vector3(randomX, randomY, 0);
    }

    //gets a random location at the left of the screen (based on what collider is passed though)
    private Vector3 getRandomLeftLocation()
    {
        float randomX = Random.Range(leftSpawnZone.bounds.min.x, leftSpawnZone.bounds.max.x);

        float randomY = Random.Range(leftSpawnZone.bounds.min.y, leftSpawnZone.bounds.max.y);

        return new Vector3(randomX, randomY, 0);
    }

    //gets a random location at the right of the screen (based on what collider is passed though)
    private Vector3 getRandomRightLocation()
    {
        float randomX = Random.Range(rightSpawnZone.bounds.min.x, rightSpawnZone.bounds.max.x);

        float randomY = Random.Range(rightSpawnZone.bounds.min.y, rightSpawnZone.bounds.max.y);

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

    //spawns a wave of level enemies for a given level
    public void spawnWave(int level)
    {
        while (currentPointVal < maxPointValue && currentTimer <= 0)
        {
            EnemySpawnInfo enemyInfo = pickEnemy(level);

            GameObject enemy = enemyInfo.enemyPrefab;
            currentPointVal += enemyInfo.enemyPointValue;

            //instantiate the enemy at the correct spawn location (top, left, or right)
            switch (enemyInfo.spawnLocation)
            {
                case EnemySpawnInfo.SpawnArea.Top:
                    Instantiate(enemy, getRandomTopLocation(), new Quaternion(0, 0, 180, 0));
                    break;
                case EnemySpawnInfo.SpawnArea.Left:
                    Instantiate(enemy, getRandomLeftLocation(), new Quaternion(0, 0, 180, 0));
                    break;
                case EnemySpawnInfo.SpawnArea.Right:
                    Instantiate(enemy, getRandomRightLocation(), new Quaternion(0, 0, 180, 0));
                    break;
            }
        }
    }

    //this will randomly select a type of enemy up to the current level (only picks lvl 1's on the first stage but can still pick lvl 1 enemies on stage 3)
    private EnemySpawnInfo pickEnemy(int difficulty)
    {
        int thisDiff = Random.Range(1, difficulty);
        int enemyIndex = 0;

        switch(thisDiff)
        {
            case 1:
                enemyIndex = Random.Range(0, enemySpawningScript.availableEarthEnemies);
                return enemySpawningScript.lvlOneEnemies[enemyIndex];

            case 2:
                enemyIndex = Random.Range(0, enemySpawningScript.availableIceEnemies);
                return enemySpawningScript.lvlTwoEnemies[enemyIndex];

            case 3:
                enemyIndex = Random.Range(0, enemySpawningScript.availableFireEnemies);
                return enemySpawningScript.lvlThreeEnemies[enemyIndex];
        }

        return null;
    }
}
