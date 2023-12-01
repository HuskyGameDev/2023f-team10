using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(BossMovement))]

public class BossBehavior : MonoBehaviour
{
    [SerializeField] protected Vector2 initialPosition;

    protected BossMovement movement;

    [SerializeField] protected List<Shooter> phaseOneShooters = new();
    [SerializeField] protected List<Shooter> phaseTwoShooters = new();
    protected List<Shooter> shooters;

    protected List<Action> phaseOneActions = new();
    protected List<Action> phaseTwoActions = new();
    protected List<Action> actions;
    private Action previousAction = null;

    protected int called;
    protected int currentShooter;
    [SerializeField] protected float waitTime = 0.25f;
    protected List<GameObject> destroyOnDeath = new();

    protected bool phaseTwoShown = false;
    protected bool phaseTwo = false;


    protected virtual void Start()
    {
        shooters = new List<Shooter>(phaseOneShooters);
        movement = GetComponent<BossMovement>();
        currentShooter = -1;
        actions = new List<Action>(phaseOneActions);
        movement.MoveTo(initialPosition, 0.5f, NextBehavior);
    }

    public virtual void PickShooter()
    {
        if(shooters.Count <= 0) { return; }
        currentShooter = UnityEngine.Random.Range(0, shooters.Count);
    }

    public virtual void NextBehavior()
    {
        int randBehavior = UnityEngine.Random.Range(0, actions.Count);

        if (!phaseTwoShown && phaseTwo && phaseTwoActions.Count > 0)
        {
            // ensure if phase two is started, the first behavior chosen is the first phase 2 action
            randBehavior = phaseOneActions.Count;
            phaseTwoShown = true;
        }
        else
        {
            // if the chosen behavior is the same as the previous behavior, roll at most 3 more times for a different behavior
            int i = 0;
            while (actions[randBehavior] == previousAction && i < 3)
            {
                randBehavior = UnityEngine.Random.Range(0, actions.Count);
                i++;
            }
        }
        if(currentShooter >= 0)
        {
            shooters[currentShooter].Shoot(0);
        }
        PickShooter();
        called = 0;

        

        previousAction = actions[randBehavior];
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
        foreach(Action action in phaseTwoActions){
            actions.Add(action);
        }
        movement.Speed = movement.Speed * 1.25f;
        
        waitTime *= 0.5f;

        phaseTwoShown = false;
        phaseTwo = true;
    }

    public virtual void OnDeath()
    {
        foreach(GameObject g in destroyOnDeath)
        {
            if(g != null)
            {
                Destroy(g);
            }
        }
    }
}
