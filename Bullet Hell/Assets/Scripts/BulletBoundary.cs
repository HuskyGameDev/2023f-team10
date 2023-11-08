using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class BulletBoundary : MonoBehaviour
{   
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && collision.enabled)
        {
            Destroy(collision.gameObject);
        }
    }
}
