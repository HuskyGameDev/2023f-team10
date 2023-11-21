using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtPlayer : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.right = player.transform.position - transform.position;
        transform.Rotate(new Vector3(0, 0, 1), -90);
    }
}
