using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossBehavior : BossBehavior
{
    [SerializeField] private GameObject orbitalLaser;
    private GameObject ol;

    protected override void Start()
    {
        phaseOneActions.Add(XBehavior);
        phaseOneActions.Add(OscBehavior);
        phaseTwoActions.Add(RandBehavior);
        base.Start();
    }

    public override void NextPhase()
    {
        base.NextPhase();
        ol = Instantiate(orbitalLaser);
        destroyOnDeath.Add(ol);
    }

    public void OscBehavior()
    {
        movement.Oscillate(4, Mathf.PI / 2, 1, OscReset);
        shooters[currentShooter].Shoot(-1);
    }

    public void OscReset()
    {
        movement.MoveTo(initialPosition, 1, NextBehavior);
    }

    public void XBehavior()
    {
        switch (called)
        {
            case 0:
                movement.MoveTo(new Vector2(-2.75f, 1), 2, XBehavior);
                break;
            case 2:
                movement.MoveTo(new Vector2(2.75f, 3), 2, XBehavior);
                shooters[currentShooter].Shoot(-1);
                break;
            case 4:
                movement.MoveTo(new Vector2(2.75f, 1), 2, XBehavior);
                break;
            case 6:
                movement.MoveTo(new Vector2(-2.75f, 3), 2, XBehavior);
                shooters[currentShooter].Shoot(-1);
                break;
            case 8:
                movement.MoveTo(initialPosition, 1, NextBehavior);
                break;
            default:
                movement.Wait(waitTime, XBehavior);
                shooters[currentShooter].Shoot(0);
                break;
        }
        called++;
    }

    public void RandBehavior()
    {
        switch (called < 5)
        {
            case true:
                Vector2 randPos = new Vector2(Random.Range(-2.75f, 2.75f), Random.Range(1f, 3f));
                while((new Vector3(randPos.x, randPos.y, transform.position.z) - transform.position).magnitude < 1.5)
                {
                    randPos = new Vector2(Random.Range(-2.75f, 2.75f), Random.Range(1f, 3f));
                }
                shooters[currentShooter].Shoot(-1);
                movement.MoveTo(randPos, 2, RandBehavior);
                break;
            case false:
                shooters[currentShooter].Shoot(0);
                movement.Wait(1.5f, OscReset);
                break;
        }
        called++;
    }
}
