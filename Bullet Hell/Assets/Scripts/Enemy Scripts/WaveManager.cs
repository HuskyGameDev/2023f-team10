using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//this script will spawn random enemies for a wave based on a theme type and point limit (and maybe also time limit)
public class WaveManager : MonoBehaviour
{
    private EnemySpawning enemySpawningScript;
    private LevelManager levelManager;

    [SerializeField] int maxPointValue = 0;
    [SerializeField] int pointLevelIncrease = 5;
    private int currentPointVal = 0;

    //this should be a rectangle/square object with a collider
    [SerializeField] Collider2D topSpawnZone;
    [SerializeField] Collider2D leftSpawnZone;
    [SerializeField] Collider2D rightSpawnZone;

    [SerializeField] float timeBetweenSpawns = 0;

    private bool waveDone = false;
    private int level = 0;


    // Start is called before the first frame update
    void Start()
    {
        enemySpawningScript = GetComponent<EnemySpawning>();
        levelManager = GetComponent<LevelManager>();
    }
    private void FixedUpdate()
    {
        //prep for a new wave to be called
        if(waveDone)
        {
            waveDone = false;
            levelManager.wavesCompleted++;
            currentPointVal = 0;
            maxPointValue += pointLevelIncrease;

            //spawn the next wave
            levelManager.checkWave();
        }
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

    //spawns a wave of level enemies for a given level
    public void spawnWave(int level)
    {
        CancelInvoke();
        this.level = level;

        //wait 5 seconds to spawn a wave then spawn an enemy every timeBetweenSpawns seconds
        InvokeRepeating("spawnWaveForReal", 5, timeBetweenSpawns);
    }

    private void spawnWaveForReal()
    {
        if (currentPointVal < maxPointValue)
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
        else
        {
            waveDone = true;
            CancelInvoke();
        }
    }

    //this will randomly select a type of enemy up to the current level (only picks lvl 1's on the first stage but can still pick lvl 1 enemies on stage 3)
    private EnemySpawnInfo pickEnemy(int difficulty)
    {
        int thisDiff = Mathf.Clamp( Random.Range(1,difficulty+1) + Random.Range(0, 3), 1, difficulty );

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
