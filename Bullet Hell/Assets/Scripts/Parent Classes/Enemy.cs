using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable<float>
{
    protected float maxHealth;
    protected float currentHealth;
    protected float xpValue;

    public void Kill()
    {
        // TODO: spawn xp, death animation, play sound, create damaging explosion if player has "enemies explode on kill" upgrade.
    }

    public virtual void Damage(float damageTaken)
    {
        currentHealth -= damageTaken;
        if(currentHealth < 0)
        {
            currentHealth = 0;
            Kill();
        }

        // TODO: damage animation, play sound
    }
}
