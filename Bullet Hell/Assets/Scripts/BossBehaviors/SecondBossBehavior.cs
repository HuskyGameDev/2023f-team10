using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBossBehavior : BossBehavior
{
    [SerializeField] private float circleFireRate;
    [SerializeField] private int circleFireAmount;

    protected override void Start()
    {
        phaseOneActions.Add(AlternateFire);
        base.Start();
        for(int i = 0; i < 3; i++)
        {
            shooters[i].BurstInterval = circleFireRate;
            shooters[i].BulletsPerBurst = circleFireAmount;
        }
    }

    public void AlternateFire()
    {
        for(int i = 0; i < 3; i++)
        {
            shooters[i].BulletsPerBurst = circleFireAmount + called;
        }
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
}
