using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this contains relevant information on enemies for spawning
public class EnemySpawnInfo
{
    public GameObject enemyPrefab;

    //0 = earth, 1 = ice, 2 = fire
    public int enemyLevel;

    //how expensive is this (proportional to the difficulty of this enemy)
    public int enemyPointValue;

    public EnemySpawnInfo(GameObject enemyPrefab, int enemyLevel, int enemyPointValue)
    {
        this.enemyPrefab = enemyPrefab;
        this.enemyLevel = enemyLevel;
        this.enemyPointValue = enemyPointValue;
    }
}
