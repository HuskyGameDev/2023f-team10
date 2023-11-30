using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossBehavior))]

public class BossDeath : DeathBehavior
{
    private BossBehavior behavior;
    private LevelManager levelManager;

    private void Start()
    {
        behavior = GetComponent<BossBehavior>();
        levelManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>();
    }

    public override void Die()
    {
        behavior.OnDeath();
        EventManager.BossDefeated();
        base.Die();

        levelManager.onBossDefeat();
    }
}
