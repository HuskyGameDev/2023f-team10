using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FollowHealthbar : Healthbar
{
    [SerializeField] private Vector3 offset;

    public override void Start()
    {
        slider.gameObject.SetActive(false);
    }

    public override void SetHealth(float health, float maxHealth)
    {
        base.SetHealth(health, maxHealth);
        slider.gameObject.SetActive(health < maxHealth);
    }

    void Update()
    {
        transform.position = transform.parent.position + offset;
        transform.rotation = Camera.main.transform.rotation;
    }
}
