using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : Bullet
{
    [SerializeField] private float chargeTime;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private SpriteRenderer warningSR;
    [SerializeField] private Transform laser;
    [SerializeField] private SpriteRenderer laserSR;

    private float startTime;
    private BoxCollider2D hitbox;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        warningSR.color = new Color(warningSR.color.r, warningSR.color.b, warningSR.color.g, 0);
        laser.localScale = new Vector3(0, 1, 1);
        hitbox = GetComponent<BoxCollider2D>();
        hitbox.enabled = false;
        StartCoroutine(HitboxRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1), 0.1f * rotationSpeed);
        if (Time.time < chargeTime + startTime)
        {
            warningSR.color = new Color(warningSR.color.r, warningSR.color.b, warningSR.color.g, 0.25f * ((Time.time - startTime) / chargeTime));
            laser.localScale = new Vector3(0.1f * ((Time.time - startTime) / chargeTime) + .05f, 1, 1);
        }
        else
        {
            warningSR.color = new Color(warningSR.color.r, warningSR.color.b, warningSR.color.g, 0);
            laser.localScale = new Vector3(1, 1, 1);
            laserSR.color = new Color(laserSR.color.r, laserSR.color.b, laserSR.color.g, ((chargeTime) / (chargeTime + (Time.time - startTime - chargeTime))));
            if (Time.time > chargeTime + startTime + 0.1)
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator HitboxRoutine()
    {
        yield return new WaitForSeconds(chargeTime);
        hitbox.enabled = true;
    }

    public override void Hit()
    {
        hitbox.enabled = false;
    }
}
