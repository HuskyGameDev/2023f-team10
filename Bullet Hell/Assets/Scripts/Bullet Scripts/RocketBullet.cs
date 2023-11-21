using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketBullet : MovingBullet
{
    [SerializeField] private float lifetime = 2;
    public float maxAdjustment;

    private float deathtime;

    public GameObject target;

    protected override void Start()
    {
        base.Start();
        deathtime = Time.time + lifetime;
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
            rb.velocity = transform.up * bulletSpeed;
        }
    }

    private void Update()
    {
        if(deathtime <= Time.time)
        {
            Hit();
        }
    }
}
