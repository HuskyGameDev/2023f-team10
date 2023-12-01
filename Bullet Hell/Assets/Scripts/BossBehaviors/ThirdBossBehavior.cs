using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class ThirdBossBehavior : BossBehavior
{
    private float delayTime = 3;
    protected override void Start()
    {
        phaseOneActions.Add(Orbit);
        phaseOneActions.Add(FourCorners);
        phaseOneActions.Add(Sweep);
        phaseTwoActions.Add(RandBehavior);
        base.Start();
    }

    public override void NextPhase()
    {
        delayTime = 2;
        base.NextPhase();
    }

    public void Orbit()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (called == 0)
        {
            shooters[0].Shoot(-1);
            shooters[3].Shoot(-1, player);
            movement.Circle(2, Orbit);
        }
        else if (called == 1)
        { 
            shooters[0].Shoot(0);
            shooters[3].Shoot(0);
            movement.MoveTo(initialPosition, 1, Orbit);
        }
        else
        {
            shooters[1].Shoot(1, player);
            movement.Wait(delayTime, NextBehavior);
        }
        called++;
    }

    public void FourCorners()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        switch (called)
        {
            case 0:
                movement.MoveTo(new Vector2(3, 3), 1, FourCorners);
                break;
            case 2:
                movement.MoveTo(new Vector2(3, -3), 1, FourCorners);
                break;
            case 4:
                movement.MoveTo(new Vector2(-3, -3), 1, FourCorners);
                break;
            case 6:
                movement.MoveTo(new Vector2(-3, 3), 1, FourCorners);
                break;
            case 8:
                movement.MoveTo(initialPosition, 1, FourCorners);
                break;
            case 9:
                movement.Wait(delayTime, NextBehavior);
                break;
            default:
                shooters[1].Shoot(1, FourCorners, player);
                phaseTwoShooters[0].Shoot(1);
                break;
        }

        called++;
    }

    private int sweepI = 0;

    public void Sweep()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        int[] yPos = { -3, 3, 0 };
        switch (called)
        {
            case 0:
                sweepI = 0;
                movement.MoveTo(new Vector2(5, transform.position.y), 1, Sweep);
                break;
            case 2:
                movement.MoveTo(new Vector2(-5, transform.position.y), 1, Sweep);
                shooters[2].Shoot(-1, player);
                break;
            case 4:
                movement.MoveTo(new Vector2(5, transform.position.y), 1, Sweep);
                shooters[2].Shoot(-1, player);
                break;
            case 6:
                movement.MoveTo(new Vector2(-5, transform.position.y), 1, Sweep);
                shooters[2].Shoot(-1, player);
                break;
            case 7:
                shooters[2].Shoot(0);
                movement.Teleport(new Vector2(0, 6), Sweep); 
                break;
            case 8:
                shooters[2].Shoot(0);
                movement.MoveTo(initialPosition, 1, NextBehavior);
                break;
            default:
                shooters[2].Shoot(0);
                movement.Teleport(new Vector2(transform.position.x, yPos[sweepI]), Sweep);
                sweepI++;
                break;
        }
        called++;
    }

    public void RandBehavior()
    {
        switch (called < 9)
        {
            case true:
                Vector2 randPos = new Vector2(Random.Range(-3.75f, 3.75f), Random.Range(1f, 3f));
                while ((new Vector3(randPos.x, randPos.y, transform.position.z) - transform.position).magnitude < 1.5)
                {
                    randPos = new Vector2(Random.Range(-2.75f, 2.75f), Random.Range(1f, 3f));
                }
                phaseTwoShooters[0].Shoot(-1);
                phaseTwoShooters[1].Shoot(-1);
                movement.MoveTo(randPos, 2, RandBehavior);
                break;
            case false:
                if(called == 9)
                {
                    phaseTwoShooters[0].Shoot(0);
                    phaseTwoShooters[1].Shoot(0);
                    movement.Wait(1.5f, RandBehavior);
                }
                else
                {
                    movement.MoveTo(initialPosition, 1, NextBehavior);
                }
                
                break;
        }
        called++;
    }
}
