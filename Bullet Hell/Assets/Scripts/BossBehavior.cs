using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(BossMovement))]

public class BossBehavior : MonoBehaviour
{
    private BossMovement movement;
    [SerializeField] private List<Shooter> shooters = new();
    private List<Action> actions = new();
    private int called;
    private int currentShooter;

    [SerializeField] private GameObject orbitalLaser;

    void Start()
    {
        actions.Add(XBehavior);
        actions.Add(OscBehavior);
        movement = GetComponent<BossMovement>();
        movement.MoveTo(new Vector2(0, 2), 0.5f, NextBehavior);
        currentShooter = 0;
    }

    public void PickShooter()
    {
        currentShooter = UnityEngine.Random.Range(0, shooters.Count);
    }

    public void NextBehavior()
    {
        int randBehavior = UnityEngine.Random.Range(0, actions.Count);
        shooters[currentShooter].Shoot(0);
        PickShooter();
        called = 0;
        movement.Wait(0.25f, actions[randBehavior]);
    }

    public void NextPhase()
    {
        foreach(Shooter shooter in shooters)
        {
            shooter.BurstInterval = shooter.BurstInterval * 0.75f;
            shooter.RestTime = shooter.RestTime * 0.75f;
        }
        movement.Speed = movement.Speed * 1.25f;
        orbitalLaser = Instantiate(orbitalLaser);
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
                movement.Wait(0.25f, XBehavior);
                shooters[currentShooter].Shoot(0);
                break;
        }
        called++;
    }

    public void OnDeath()
    {
        Destroy(orbitalLaser);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
