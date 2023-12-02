using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

/*moves the object this script is placed on (such as an enemy) in a straight line in one of the following methods:
 *Vertical: moves the enemy up or down at the specified speed
 *Horizontal:moves the enemy left or right at the specified speed
 *Diagonal: moves the enemy diagonally at the angle and speed specified
*/
public class EnemyMovement : MonoBehaviour
{
    private EnemyAttack enemyAttackScript = null;

    //give the unit some time to make an attack (otherwise the bool switches to fast for thigns to process)
    [SerializeField] float timeForAttack = .5f;
    private float currentAttackTimer = 0f;

    //randomhold is to have the enemy find a random position onscreen and hold position once it gets there
    private enum MoveOptions { VERTICAL, HORIZONTAL, DIAGONAL, ZIGZAG, RANDOMHOLD, RANDOM, SEEK};

    //general variables
    [SerializeField] MoveOptions moveType;
    [SerializeField] float speed = 0;

    //for diagonal and zig zag movement
    [SerializeField] float angle = 0;

    //zig zag will change direction every amount of seconds specified
    [SerializeField] float moveChangeTimer = 0;

    //for random hold movement
    private Collider2D targetOrigin = null;

    //used to keep track of what the current countdown until an event is (being used for zig zag only  as of writing this)
    private float secondTimer = 0;

    //used to control randomHold
    private bool foundSpot = false;
    private Vector3 direction = Vector3.up;
    public Vector3 target = Vector3.up;

    private void Awake()
    {
        enemyAttackScript = GetComponent<EnemyAttack>();

        GameObject temp = null;
        temp = GameObject.FindGameObjectWithTag("RandomMoveZone");

        if (temp != null)
            targetOrigin = temp.GetComponent<Collider2D>();
    }

    //updating on fixed update for consistent behavior
    private void FixedUpdate()
    {
        //do vertical/horzontal movement by putting speed for the corresponding axis and zero-ing the rest of a vector and adding that position to the gameobject's position
        if (moveType == MoveOptions.VERTICAL)
            transform.position -= new Vector3(0, speed, 0) * Time.fixedDeltaTime;
        else if (moveType == MoveOptions.HORIZONTAL)
            transform.position += new Vector3(speed, 0, 0) * Time.fixedDeltaTime;

        //see the corresponding methods for details on these move types
        else if (moveType == MoveOptions.DIAGONAL)
            diagonalMove(angle);

        else if (moveType == MoveOptions.ZIGZAG)
            zigZagMove();

        //handles random hold and ranom move
        else if (moveType == MoveOptions.RANDOMHOLD || moveType == MoveOptions.RANDOM)
            randomMovement();

        //handles verticle movment seeking the player
        else if (moveType == MoveOptions.SEEK)
            seekPlayer();

        //count the timer down every second
        secondTimer -= Time.fixedDeltaTime;
        currentAttackTimer -= Time.fixedDeltaTime;
    }

    //gets a random location in an area tagged with "RandomMoveZone"
    private Vector3 getRandomMoveLocation()
    {
        float randomX = Random.Range(targetOrigin.bounds.min.x, targetOrigin.bounds.max.x);

        float randomY = Random.Range(targetOrigin.bounds.min.y, targetOrigin.bounds.max.y);

        return new Vector3(randomX, randomY, 0);
    }

    //doing some math here based on the Pythagoras theorem (SOH CAH TOA) where the hypotenuse is 1 (since we move 1 unit at a time) we back calculate from
    //the angle to get the horzontal/verticle distance at a right angle to eachother and input that in a similar way to the vertical/horizontal movement methods above
    //see this for more https://www.geeksforgeeks.org/how-to-find-an-angle-in-a-right-angled-triangle/
    //the math might be a bit off but it gives a close enough for what's needed
    public void diagonalMove(float diagAngle)
    {
        //convert from degrees into radians
        double b = diagAngle * Math.PI / 180;

        //get the inverse cosine to get the horizontal distance
        double a = Math.Acos(b);

        if (diagAngle < 0)
        {
            //update the transform with a modified speed since the a becomes > 2 which greatly impacts the speed
            transform.position -= new Vector3((float)a, (float)(1 - (a * a)), 0) * (speed / (float)(2 + a)) * Time.fixedDeltaTime;
        }
        else
        {
            //update the transform
            transform.position -= new Vector3((float)a, (float)(1 - (a * a)), 0) * speed * Time.fixedDeltaTime;
        }
    }

    //moves the object in a left to right diagonal and then switches going right to left at a given angle 
    public void zigZagMove()
    {
        //change the objects diagnoal direction or move it
        if (secondTimer <= 0)
        {
            secondTimer = moveChangeTimer;
            speed = -speed;
            angle = -angle;
        }

        diagonalMove(angle);
    }

    //has the object choose a random position within a given radius and then go there and remain until something else changes its position
    public void randomMovement()
    {
        if (!foundSpot && currentAttackTimer <= 0)
        {
            //don't attack until position is found/held
            enemyAttackScript.canAttack = false;

            //pick a location within the given bounds
            target = getRandomMoveLocation();

            //calculate the normalized vector distance between target and this object
            direction = (target - transform.position).normalized;

            foundSpot = true;
        }

        //go towards the target position until the object gets there
        if (foundSpot && (target - transform.position).magnitude > .5)
        {
            transform.position += direction * speed * Time.fixedDeltaTime;
            currentAttackTimer = timeForAttack;
        }
        else
        {
            //now holding position so begin attacks
            enemyAttackScript.canAttack = true;

            //if the target should find a new position to go to after getting where it's going
            if (moveType == MoveOptions.RANDOM)
                foundSpot = false;
        }
    }

    public void seekPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Vector3 temp = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.fixedDeltaTime);

        Vector3 temp2 = transform.position;

        transform.position = new Vector3(temp.x, (temp2.y - speed * Time.fixedDeltaTime), 0);
    }
}
