using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class RocketBullet : Bullet
{
    private Rigidbody2D rb;

    public float bulletSpeed;

    [SerializeField] private float lifetime = 2;
    public float maxAdjustment;

    private float deathtime;

    public GameObject target;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        deathtime = Time.time + lifetime;
        rb.velocity = transform.up * bulletSpeed / 10;
    }

    private void FixedUpdate()
    {
        if(target != null)
        {
            Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
            Vector2 target2D = new Vector2(target.transform.position.x, target.transform.position.y);

            float angle = Vector2.SignedAngle(transform.up, target2D - position2D);

            float adjustment = Mathf.Sign(angle) * Mathf.Min(maxAdjustment, Mathf.Abs(angle));

            transform.Rotate(new Vector3(0, 0, 1), adjustment);
            Vector2 upVec = new Vector2(transform.up.x, transform.up.y);
            rb.AddForce(upVec.normalized * Mathf.Min(bulletSpeed / (1 + Mathf.Abs(angle) / 10), 5));
        }
        if(rb.velocity.magnitude > bulletSpeed)
        {
            rb.velocity = rb.velocity.normalized * bulletSpeed;
        }
    }

    private void Update()
    {
        if(deathtime <= Time.time)
        {
            Hit();
        }
    }

    public override void Hit()
    {
        // TODO: add a hit sound.
        Destroy(gameObject);
    }
}
