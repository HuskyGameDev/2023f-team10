using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private float maxHealth;
    private float currentHealth;

    private DeathBehavior deathBehavior;

    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetHealth(currentHealth, maxHealth);
        deathBehavior = gameObject.GetComponent<DeathBehavior>();
    }

    public void damage(float amount)
    {
        currentHealth -= amount;
        if(currentHealth < 0)
        {
            currentHealth = 0;
        }

        healthbar.SetHealth(currentHealth, maxHealth);

        if(currentHealth == 0)
        {
            deathBehavior.Die();
        }
    }

    public void heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthbar.SetHealth(currentHealth, maxHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            bullet.Hit();

            damage(bullet.Damage);

            Debug.Log(currentHealth);
        }
    }
}
