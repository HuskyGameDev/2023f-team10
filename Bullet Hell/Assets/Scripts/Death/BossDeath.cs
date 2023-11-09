using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossBehavior))]

public class BossDeath : DeathBehavior
{
    private BossBehavior behavior;
    private void Start()
    {
        behavior = GetComponent<BossBehavior>();
    }

    public override void Die()
    {
        behavior.OnDeath();
        EventManager.BossDefeated();
        base.Die();
    }
}
