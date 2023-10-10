using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*moves the object this script is placed on (such as an enemy) in a straight line in one of the following methods:
 *Vertical: moves the enemy up or down at the specified speed
 *Horizontal:moves the enemy left or right at the specified speed
 *Diagonal: moves the enemy diagonally at the angle and speed specified
*/
public class StraightLine : MonoBehaviour
{
    enum MoveOptions{VERTICAL, HORIZONTAL, DIAGONAL};

    //tweak this value in the editor
    [SerializeField] MoveOptions moveType;

    [SerializeField] float speed = 0;
    [SerializeField] float angle = 0;

    //updating on fixed update for consistent behavior
    private void FixedUpdate()
    {
        //do vertical/horzontal movement by putting speed for the corresponding axis and zero-ing the rest of a vector and adding that position to the gameobject's position
        if (moveType == MoveOptions.VERTICAL)
            transform.position -= new Vector3(0, speed, 0) * Time.fixedDeltaTime;
        else if (moveType == MoveOptions.HORIZONTAL)
            transform.position += new Vector3(speed, 0, 0) * Time.fixedDeltaTime;

        //doing some math here based on the Pythagoras theorem (SOH CAH TOA) where the hypotenuse is 1 (since we move 1 unit at a time) we back calculate from
        //the angle to get the horzontal/verticle distance at a right angle to eachother and input that in a similar way to the vertical/horizontal movement methods above
        //see this for more https://www.geeksforgeeks.org/how-to-find-an-angle-in-a-right-angled-triangle/
        //the math might be a bit off but it gives a close enough for what's needed
        else if (moveType == MoveOptions.DIAGONAL)
        {
            //convert from degrees into radians
            double b = angle * Math.PI / 180;

            //get the inverse cosine to get the horizontal distance
            double a = Math.Acos(b);

            if(angle < 0)
            {
                //update the transform with a modified speed since the a becomes > 2 which greatly impacts the speed
                transform.position -= new Vector3((float)a, (float)(1 - (a * a)), 0) * (speed/(float)(2+a)) * Time.fixedDeltaTime;
            }
            else
            {
                //update the transform
                transform.position -= new Vector3((float)a, (float)(1 - (a * a)), 0) * speed * Time.fixedDeltaTime;
            }
        }
    }
}
