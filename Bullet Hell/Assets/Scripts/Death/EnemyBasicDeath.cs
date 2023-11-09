using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicDeath : DeathBehavior
{
    public override void Die()
    {
        base.Die();
    }

    void OnEnable()
    {
        EventManager.OnBossDefeated += Die;
    }

    void OnDisable()
    {
        EventManager.OnBossDefeated -= Die;
    }
}
