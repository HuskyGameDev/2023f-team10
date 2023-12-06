using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegen : MonoBehaviour
{
    [SerializeField] private float healthRegenSpeed = 1;

    private Health health;

    private void Start()
    {
        health = GetComponent<Health>();
    }

    private void FixedUpdate()
    {
        health.heal(healthRegenSpeed * Time.deltaTime);
    }
}
