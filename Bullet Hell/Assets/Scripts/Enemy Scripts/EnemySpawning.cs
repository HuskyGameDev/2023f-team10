using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script will contain methods to spawn indiviual enemies, these should be called from the wave manager
public class EnemySpawning : MonoBehaviour
{
    //NOTE: these names are palceholders, this is more so we have a template for future additions

    //a list of enemy prefabs, for this one we do need to remember indexing because this should only need to be set once (if it starts getting complicated I can change how this works)
    [SerializeField] GameObject[] earthEnemyPrefabs;
    [SerializeField] GameObject[] iceEnemyPrefabs;
    [SerializeField] GameObject[] fireEnemyPrefabs;

    [NonSerialized] public int availableEarthEnemies = 0;
    [NonSerialized] public int availableIceEnemies = 0;
    [NonSerialized] public int availableFireEnemies = 0;

    public EnemySpawnInfo[] lvlOneEnemies = null;
    public EnemySpawnInfo[] lvlTwoEnemies = null;
    public EnemySpawnInfo[] lvlThreeEnemies = null;

    private void Start()
    {
        availableEarthEnemies = earthEnemyPrefabs.Length;
        availableIceEnemies = iceEnemyPrefabs.Length;
        availableFireEnemies = fireEnemyPrefabs.Length;

        lvlOneEnemies = new EnemySpawnInfo[availableEarthEnemies];
        lvlTwoEnemies = new EnemySpawnInfo[availableIceEnemies];
        lvlThreeEnemies = new EnemySpawnInfo[availableFireEnemies];

        populateLevelOne();
        populateLevelTwo();
        populateLevelThree();
    }

    //will populate the level one enemies (all at cost 1 by default)
    private void populateLevelOne()
    {
        for(int i = 0; i < availableEarthEnemies; i++)
        {
            lvlOneEnemies[i] = new EnemySpawnInfo(earthEnemyPrefabs[i], 0, 1, EnemySpawnInfo.SpawnArea.Top);
        }
    }

    //will populate the level two enemies (all at cost 2 by default)
    private void populateLevelTwo()
    {
        for (int i = 0; i < availableIceEnemies; i++)
        {
            lvlTwoEnemies[i] = new EnemySpawnInfo(iceEnemyPrefabs[i], 0, 2, EnemySpawnInfo.SpawnArea.Top);
        }
    }

    //will populate the level three enemies (all at cost 3 by default)
    private void populateLevelThree()
    {
        for (int i = 0; i < availableFireEnemies; i++)
        {
            lvlThreeEnemies[i] = new EnemySpawnInfo(fireEnemyPrefabs[i], 0, 3, EnemySpawnInfo.SpawnArea.Top);
        }
    }
}
