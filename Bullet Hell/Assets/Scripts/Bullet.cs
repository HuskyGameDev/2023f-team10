using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float bulletSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;
    }
}
