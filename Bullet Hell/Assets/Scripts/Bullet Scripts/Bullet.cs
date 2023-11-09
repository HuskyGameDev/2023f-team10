using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletDamage;
    [SerializeField] private AudioClip fireSound;

    void OnEnable()
    {
        EventManager.OnBossDefeated += ClearBullet;
    }

    void OnDisable()
    {
        EventManager.OnBossDefeated -= ClearBullet;
    }

    public float Damage
    {
        get { return bulletDamage; }
    }

    public virtual void Hit()
    {
        Debug.Log("The Hit() method of this bullet type has not been implemented yet. Please override it in the implementing script!");
    }

    public AudioClip GetFireSound()
    {
        return fireSound;
    }

    private void ClearBullet()
    {
        Destroy(gameObject);
    }
}
