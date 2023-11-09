using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shooter))]

public class OrbitalLaserBehavior : MonoBehaviour
{
    private Shooter shooter;
    void Start()
    {
        shooter = GetComponent<Shooter>();
        shooter.Shoot(-1);
    }

    void Update()
    {
        transform.position = new Vector3(Random.Range(-3.5f, 3.5f), 5, transform.position.z);
    }
}
