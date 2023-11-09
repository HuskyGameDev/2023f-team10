using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private bool instatiateAsChild = false;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int burstCount; // Number of bursts in a burst group
    [SerializeField] private int bulletsPerBurst; // Number of bullets shot in a burst
    [SerializeField][Range(0, 359)] private float angleSpread;
    [SerializeField] private float startingDistance = 0.1f;
    [SerializeField] private Vector2 startingDirection = Vector2.down;
    [SerializeField] private float shotWidth;
    [SerializeField] private float burstInterval; // The amount of time between each burst in a group
    [SerializeField] private float restTime; // The amount of time between each burst group
    [SerializeField] private bool rotate; // Whether the direction of each burst should be rotating or constant
    [SerializeField] private float rotationPerBurst; // Change in angle per burst
    
    private Action callWhenFinished = null;

    private bool isShooting = false;
    private bool shouldShoot = false;
    private int burstsLeft = 0;

    public float BurstInterval { get { return burstInterval; } set { burstInterval = value; } }
    public float RestTime { get { return restTime; } set { restTime = value; } }
    public int BulletsPerBurst { get { return bulletsPerBurst; } set { bulletsPerBurst = value; } }
    public float RotationPerBurst { get { return rotationPerBurst; } set { rotationPerBurst = value; } }

    /// <summary>
    /// Tells the shooter script to begin shooting n burst groups.<br></br><br></br>
    /// 
    /// n = -1 will make it shoot continuously when called.<br></br>
    /// n = 0 will stop the shooter from shooting after the current burst group has finished shooting.<br></br>
    /// </summary>
    /// <param name="n">The number of burst groups to shoot</param>
    public void Shoot(int n)
    {
        shouldShoot = n != 0;
        burstsLeft = n;
        callWhenFinished = null;
    }

    /// <summary>
    /// Tells the shooter script to begin shooting n burst groups and call function f when done.<br></br><br></br>
    /// 
    /// n = -1 will make it shoot continuously when called.<br></br>
    /// n = 0 will stop the shooter from shooting after the current burst group has finished shooting.<br></br>
    /// </summary>
    /// <param name="n">The number of burst groups to shoot</param>
    /// <param name="f">The function to call once all burst groups have been shot</param>
    public void Shoot(int n, Action f)
    {
        shouldShoot = n != 0;
        burstsLeft = n;
        callWhenFinished = f;
    }

    private void Update()
    {
        if(!isShooting && shouldShoot)
        {
            isShooting = true;
            StartCoroutine(ShootRoutine());
            if(burstsLeft > 0)
            {
                burstsLeft--;
                if (burstsLeft == 0)
                {
                    shouldShoot = false;
                }
            }
        }
        else if(!isShooting && callWhenFinished != null)
        {
            callWhenFinished();
            callWhenFinished = null;
        }
    }

    private IEnumerator ShootRoutine()
    {
        Vector2 targetDirection = startingDirection;
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
                Vector2 pos = FindBulletSpawnPos(currentAngle, instatiateAsChild);
                float xOffset = bulletsPerBurst > 1 ? (-shotWidth / 2f) + (shotWidth * (j / (float)(bulletsPerBurst - 1))) : 0;
                pos.x += xOffset;

                GameObject newBullet;
                if (instatiateAsChild)
                {
                    newBullet = Instantiate(bulletPrefab, gameObject.transform, false);
                }
                else
                {
                    newBullet = Instantiate(bulletPrefab, pos, transform.rotation);
                }
                
                newBullet.transform.Rotate(new Vector3(0, 0, 1), currentAngle + 90);

                currentAngle += angleStep;
            }

            currentAngle = startAngle + (rotationPerBurst * (i + 1));

            // TODO: play the sound attached to the bullet here

            yield return new WaitForSeconds(burstInterval);
        }

        yield return new WaitForSeconds(restTime);

        LaserBullet laserBullet;
        if ((laserBullet = bulletPrefab.GetComponent<LaserBullet>()) != null && burstsLeft == 0)
        {
            yield return new WaitForSeconds(laserBullet.ChargeTime + 0.1f);
        }

        isShooting = false;
    }

    private Vector2 FindBulletSpawnPos(float currentAngle, bool asChild)
    {
        float xOffset = asChild ? 0 : transform.position.x;
        float yOffset = asChild ? 0 : transform.position.y;
        float x = xOffset + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = yOffset + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        Vector2 pos = new Vector2(x, y);

        return pos;
    }
}
