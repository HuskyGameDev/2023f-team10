using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int burstCount; // Number of bursts in a burst group
    [SerializeField] private int bulletsPerBurst; // Number of bullets shot in a burst
    [SerializeField][Range(0, 359)] private float angleSpread;
    [SerializeField] private float startingDistance = 0.1f;
    [SerializeField] private float burstInterval; // The amount of time between each burst in a group
    [SerializeField] private float restTime; // The amount of time between each burst group
    [SerializeField] private bool rotate; // Whether the direction of each burst should be rotating or constant
    [SerializeField] private float rotationPerBurst; // Change in angle per burst

    private bool isShooting = false;

    private void Update()
    {
        if(!isShooting)
        {
            StartCoroutine(ShootRoutine());
        }
    }

    private IEnumerator ShootRoutine()
    {
        isShooting = true;

        Vector2 targetDirection = Vector2.down;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        float startAngle = targetAngle;
        float endAngle = targetAngle;
        float currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        float angleStep = 0f;

        if(angleSpread != 0)
        {
            angleStep = angleSpread / (bulletsPerBurst - 1);
            halfAngleSpread = angleSpread / 2f;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }

        for (int i = 0; i < burstCount; i++)
        {
            for (int j = 0; j < bulletsPerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle);

                GameObject newBullet = Instantiate(bulletPrefab, pos, transform.rotation);
                // newBullet.transform.up = newBullet.transform.position - transform.position
                newBullet.transform.Rotate(new Vector3(0, 0, 1), currentAngle + 90);

                currentAngle += angleStep;
            }

            currentAngle = startAngle + (rotationPerBurst * (i + 1));

            // TODO: play the sound attached to the bullet here

            yield return new WaitForSeconds(burstInterval);
        }

        yield return new WaitForSeconds(restTime);

        isShooting = false;
    }

    private Vector2 FindBulletSpawnPos(float currentAngle)
    {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        Vector2 pos = new Vector2(x, y);

        return pos;
    }
}
