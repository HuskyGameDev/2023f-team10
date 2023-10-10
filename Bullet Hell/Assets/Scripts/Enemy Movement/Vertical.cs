using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Moves the enemy on a vertical axis (positive speed = down while -speed = up)
public class Verticle : MonoBehaviour
{
    //tweak this value in the editor
    [SerializeField] float speed = 0;

    private void FixedUpdate()
    {
        //updating on fixed update for consistent behavior
        transform.position -= new Vector3(0, speed, 0) * Time.fixedDeltaTime;
    }
}
