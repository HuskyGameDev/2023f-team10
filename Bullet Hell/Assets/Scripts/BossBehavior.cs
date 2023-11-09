using System;
using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        actions.Add(XBehavior);
        actions.Add(OscBehavior);
        movement = GetComponent<BossMovement>();
        movement.MoveTo(new Vector2(0, 2), 0.5f, NextBehavior);
    }

    public void PickShooter()
    {
        currentShooter = UnityEngine.Random.Range(0, shooters.Count);
    }

    public void NextBehavior()
    {
        int randBehavior = UnityEngine.Random.Range(0, actions.Count);
        PickShooter();
        called = 0;
        movement.Wait(0.25f, actions[randBehavior]);
    }

    public void OscBehavior()
    {
        movement.Oscillate(4, Mathf.PI / 2, 1, NextBehavior);
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
                break;
            case 4:
                movement.MoveTo(new Vector2(2.75f, 1), 2, XBehavior);
                break;
            case 6:
                movement.MoveTo(new Vector2(-2.75f, 3), 2, XBehavior);
                break;
            case 8:
                movement.MoveTo(new Vector2(0, 2), 1, NextBehavior);
                break;
            default:
                movement.Wait(0.25f, XBehavior);
                break;
        }
        called++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
