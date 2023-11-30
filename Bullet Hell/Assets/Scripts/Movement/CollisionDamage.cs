using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //damage player
            this.GetComponent<Health>().damage(100);

            //damage enemy
            collision.gameObject.GetComponent<EnemyBasicDeath>().Die();
        }

        else if (collision.CompareTag("Boss"))
        {
            //damage player
            this.GetComponent<Health>().damage(100);

            //damage boss
            collision.gameObject.GetComponent<Health>().damage(100);
        }
    }
}
