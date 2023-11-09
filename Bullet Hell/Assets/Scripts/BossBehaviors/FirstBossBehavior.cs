using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossBehavior : BossBehavior
{
    [SerializeField] private GameObject orbitalLaser;
    private GameObject ol;

    protected override void Start()
    {
        base.Start();
        actions.Add(XBehavior);
        actions.Add(OscBehavior);
        movement.MoveTo(new Vector2(0, 2), 0.5f, NextBehavior);
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
        movement.MoveTo(new Vector2(0, 2), 1, NextBehavior);
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
                movement.MoveTo(new Vector2(0, 2), 1, NextBehavior);
                break;
            default:
                movement.Wait(waitTime, XBehavior);
                shooters[currentShooter].Shoot(0);
                break;
        }
        called++;
    }
}
