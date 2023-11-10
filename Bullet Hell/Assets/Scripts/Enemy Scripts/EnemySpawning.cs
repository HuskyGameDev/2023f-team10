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

    private void Start()
    {
        availableEarthEnemies = earthEnemyPrefabs.Length;
        availableIceEnemies = iceEnemyPrefabs.Length;
        availableFireEnemies = fireEnemyPrefabs.Length;
    }


    //earth enemies
    public EnemySpawnInfo spawnBasic()
    {
        return new EnemySpawnInfo(earthEnemyPrefabs[0], 0, 1);
    }

    //ice enemies
    public EnemySpawnInfo spawnRandQuad()
    {
        return new EnemySpawnInfo(iceEnemyPrefabs[0], 1, 5);
    }

    //fire enemies
    public EnemySpawnInfo spawnFlare()
    {
        return new EnemySpawnInfo(fireEnemyPrefabs[0], 2, 10);
    }
}
