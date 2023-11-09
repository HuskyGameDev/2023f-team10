using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(BossMovement))]

public class BossBehavior : MonoBehaviour
{
    protected BossMovement movement;
    [SerializeField] protected List<Shooter> phaseOneShooters = new();
    [SerializeField] protected List<Shooter> phaseTwoShooters = new();
    protected List<Shooter> shooters;
    protected List<Action> actions = new();
    protected int called;
    protected int currentShooter;
    [SerializeField] protected float waitTime = 0.25f;
    protected List<GameObject> destroyOnDeath = new();

    protected virtual void Start()
    {
        shooters = new List<Shooter>(phaseOneShooters);
        movement = GetComponent<BossMovement>();
        currentShooter = 0;
        Debug.Log("Base Start Done");
    }

    public virtual void PickShooter()
    {
        currentShooter = UnityEngine.Random.Range(0, shooters.Count);
    }

    public virtual void NextBehavior()
    {
        int randBehavior = UnityEngine.Random.Range(0, actions.Count);
        shooters[currentShooter].Shoot(0);
        PickShooter();
        called = 0;
        movement.Wait(waitTime, actions[randBehavior]);
    }

    public virtual void NextPhase()
    {
        foreach(Shooter shooter in shooters)
        {
            shooter.BurstInterval = shooter.BurstInterval * 0.75f;
            shooter.RestTime = shooter.RestTime * 0.75f;
        }
        foreach(Shooter shooter in phaseTwoShooters)
        {
            shooters.Add(shooter);
        }
        movement.Speed = movement.Speed * 1.25f;
        
        waitTime *= 0.5f;
    }

    public virtual void OnDeath()
    {
        foreach(GameObject g in destroyOnDeath)
        {
            if(g != null)
            {
                Debug.Log("Destroying!");
                Destroy(g);
            }
        }
    }
}
