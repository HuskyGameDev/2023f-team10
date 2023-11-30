using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class ThirdBossBehavior : BossBehavior
{
    private float delayTime = 3;
    protected override void Start()
    {
        // TODO: add phase 1 and 2 actions here
        // phaseOneActions.Add();
        // phaseTwoActions.Add();

        phaseOneActions.Add(Orbit);
        phaseOneActions.Add(FourCorners);
        phaseOneActions.Add(Sweep);
        base.Start();
    }

    public override void NextPhase()
    {
        delayTime = 2;
        base.NextPhase();
    }

    public void Orbit()
    {
        if(called == 0)
        {
            shooters[0].Shoot(-1);
            movement.Circle(2, Orbit);
        }
        else if (called == 1)
        { 
            shooters[0].Shoot(0);
            movement.MoveTo(initialPosition, 1, Orbit);
        }
        else
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
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
                shooters[3].Shoot(-1, player);
                break;
            case 4:
                movement.MoveTo(new Vector2(5, transform.position.y), 1, Sweep);
                shooters[3].Shoot(-1, player);
                break;
            case 6:
                movement.MoveTo(new Vector2(-5, transform.position.y), 1, Sweep);
                shooters[3].Shoot(-1, player);
                break;
            case 7:
                shooters[3].Shoot(0);
                movement.Teleport(new Vector2(0, 6), Sweep); 
                break;
            case 8:
                shooters[3].Shoot(0);
                movement.MoveTo(initialPosition, 1, NextBehavior);
                break;
            default:
                shooters[3].Shoot(0);
                movement.Teleport(new Vector2(transform.position.x, yPos[sweepI]), Sweep);
                sweepI++;
                break;
        }
        called++;
        
    }
}
