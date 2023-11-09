using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehavior : MonoBehaviour
{
    [SerializeField] private GameObject deathExplosion;

    public virtual void Die()
    {
        GameObject explosion = Instantiate(deathExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
