using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtPlayer : PointAt
{
    private GameObject player;
    private bool playerExists;
    private Vector3 previousPosition = Vector3.zero;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerExists = player != null;
    }

    protected override void Update()
    {
        if (playerExists)
        {
            try
            {
                target = player.transform.position;
                previousPosition = target;
            }
            catch (MissingReferenceException e)
            {
                playerExists = false;
                target = previousPosition;
            }
        }
        
        base.Update();
    }
}
