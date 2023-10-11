using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            bullet.Hit();

            currentHealth -= bullet.Damage;
            if(currentHealth < 0)
            {
                currentHealth = 0;
                // TODO: kill this game object.
            }

            Debug.Log(currentHealth);
        }
    }
}
