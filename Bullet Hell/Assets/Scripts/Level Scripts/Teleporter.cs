using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A simple script so I can teleport units back to the top of the screen when they go off it so testing is faster
public class Teleporter : MonoBehaviour
{
    [SerializeField] GameObject toHere;

    //using trigger so if we wish to do collisions this won't affect anything
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.position = toHere.transform.position;
    }
}
