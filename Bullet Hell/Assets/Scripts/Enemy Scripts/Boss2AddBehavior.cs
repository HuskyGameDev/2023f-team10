using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Boss2AddBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    private Shooter shooter;
    [SerializeField] private float initialSpeed;
    [SerializeField] private float rotationAmount;

    private bool startedShooting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shooter = GetComponent<Shooter>();

        rb.angularVelocity = rotationAmount;
        rb.velocity = new Vector2(0, -initialSpeed);
    }

    private void Update()
    {
        if(startedShooting == false && rb.velocity.magnitude < 0.05f)
        {
            shooter.Shoot(-1);
            startedShooting = true;
        }
    }
}
