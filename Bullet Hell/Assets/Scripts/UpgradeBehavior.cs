using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class UpgradeBehavior : MonoBehaviour
{
    private enum UpgradeType {ATTACK_SPEED, CRITICAL_HIT_CHANCE, ATTACK_DAMAGE, MOVEMENT_SPEED, MAX_HEALTH, HEALTH_REGENERATION};
    private UpgradeType upgradeType;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = Vector3.down;
    }

    public void Collect()
    {
        switch(upgradeType)
        {
            case UpgradeType.ATTACK_SPEED:
                break;
            case UpgradeType.CRITICAL_HIT_CHANCE:
                break;
            case UpgradeType.ATTACK_DAMAGE:
                break;
            case UpgradeType.MOVEMENT_SPEED:
                break;
            case UpgradeType.MAX_HEALTH:
                break;
            case UpgradeType.HEALTH_REGENERATION:
                break;
            default:
                break;
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collect();
        }
    }
}
