using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossBehavior))]

public class BossHealth : Health
{
    private BossBehavior behavior;

    protected override void Start()
    {
        base.Start();
        behavior = GetComponent<BossBehavior>();
    }
    public override void damage(float amount)
    {
        float previousHealth = currentHealth;
        base.damage(amount);
        if(previousHealth > maxHealth / 2 && currentHealth <= maxHealth / 2)
        {
            behavior.NextPhase();
        }
    }
}
