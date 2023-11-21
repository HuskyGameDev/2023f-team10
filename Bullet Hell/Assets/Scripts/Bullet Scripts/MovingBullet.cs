using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class MovingBullet : Bullet
{
    protected Rigidbody2D rb;

    public float bulletSpeed;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;
    }

    public override void Hit()
    {
        // TODO: add a hit sound.
        Destroy(gameObject);
    }
}
