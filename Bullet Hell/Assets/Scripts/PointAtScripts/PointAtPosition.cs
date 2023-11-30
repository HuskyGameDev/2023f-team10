using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtPosition : PointAt
{
    [SerializeField] private Vector2 position;

    public Vector2 Position
    {
        get { return position; }
        set 
        {
            position = value;
            UpdateTarget();
        }
    }

    void Start()
    {
        UpdateTarget();
    }

    private void UpdateTarget()
    {
        target = new Vector3(position.x, position.y);
    }
}
