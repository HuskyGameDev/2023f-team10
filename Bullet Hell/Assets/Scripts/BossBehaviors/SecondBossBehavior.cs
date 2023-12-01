using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SecondBossBehavior : BossBehavior
{
    [SerializeField] private float circleFireRate;
    [SerializeField] private int circleFireAmount;

    [SerializeField] private float rocketFireRate;
    [SerializeField] private int rocketFireAmount;

    [SerializeField] private GameObject add;

    protected override void Start()
    {
        phaseOneActions.Add(AlternateFire);
        phaseOneActions.Add(FireRockets);
        base.Start();
        for(int i = 0; i < 3; i++)
        {
            shooters[i].BurstInterval = circleFireRate;
        }
        shooters[0].BulletsPerBurst = circleFireAmount;
        shooters[2].BulletsPerBurst = circleFireAmount;

        shooters[3].BurstInterval = rocketFireRate;
        shooters[5].BurstInterval = rocketFireRate;
        shooters[3].BulletsPerBurst = rocketFireAmount;
        shooters[5].BulletsPerBurst = rocketFireAmount;
    }

    public void AlternateFire()
    {
        if(called == 10)
        {
            shooters[0].Shoot(0);
            shooters[1].Shoot(0);
            shooters[2].Shoot(0);
            movement.Wait(2, NextBehavior);
            return;
        }
        else if(called == 0)
        {
            shooters[1].Shoot(-1);
            
        }
        else if(called == 1)
        {
            shooters[0].Shoot(-1);
            shooters[2].Shoot(-1);
        }
        called++;
        movement.Wait(shooters[1].BurstInterval / 2, AlternateFire);
    }

    public void FireRockets()
    {
        if(called == 1)
        {
            shooters[3].Shoot(0);
            shooters[4].Shoot(0);
            shooters[5].Shoot(0);
            movement.Wait(2, NextBehavior);
            return;
        }
        else if(called == 0)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            shooters[3].Shoot(5, player);
            shooters[4].Shoot(-1);
            shooters[5].Shoot(5, FireRockets, player);
        }
        called++;
    }

    public override void NextPhase()
    {
        base.NextPhase();
        StartCoroutine(AddAdds());
    }

    private IEnumerator AddAdds()
    {
        Instantiate(add, new Vector3(-2, transform.position.y, transform.position.z + 1), transform.rotation);

        yield return new WaitForSeconds(3.75f / 2.0f);

        Instantiate(add, new Vector3(2, transform.position.y, transform.position.z + 1), transform.rotation);
    }
}
