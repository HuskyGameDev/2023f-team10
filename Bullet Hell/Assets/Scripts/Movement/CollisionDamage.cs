using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //damage player
        this.GetComponent<Health>().damage(100);

        //damage enemy
        if (collision.CompareTag("Enemy"))
            collision.gameObject.GetComponent<EnemyBasicDeath>().Die();

        //damage boss
        else if (collision.CompareTag("Boss"))
            collision.gameObject.GetComponent<Health>().damage(100);
    }
}
