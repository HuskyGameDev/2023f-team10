using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBound : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Bullet"))
            Destroy(collision.gameObject);
    }
}
