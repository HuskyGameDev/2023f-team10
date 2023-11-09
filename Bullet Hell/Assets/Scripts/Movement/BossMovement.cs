using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class BossMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private float speedMult = 1;
    private float oscRate = 1;
    private Rigidbody2D rb;
    private enum MoveOptions { MoveTo, Oscillate, Wait, Finished };
    private MoveOptions moveType = MoveOptions.Finished;

    private Vector2 targetPos;
    private Action callWhenFinished = null;

    private float startTime;
    private float finishTime;

    public float Speed { get { return speed; } set {  speed = value; } }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Moves in a straight line to the provided 2D position, calls f when done.
    /// </summary>
    /// <param name="pos">Position to move to</param>
    /// <param name="multiplier">Speed multiplier, used to slow boss down if needed</param>
    /// <param name="f">Function to call when finished</param>
    public void MoveTo(Vector2 pos, float multiplier, Action f)
    {
        targetPos = pos;
        callWhenFinished = f;
        moveType = MoveOptions.MoveTo;
        speedMult = multiplier;
    }

    /// <summary>
    /// Begins moving left and right at a steady rate
    /// </summary>
    /// <param name="t"></param>
    /// <param name="rate"></param>
    /// <param name="f"></param>
    public void Oscillate(float t, float rate, float multiplier, Action f)
    {
        startTime = Time.time;
        finishTime = Time.time + t;
        callWhenFinished = f;
        moveType = MoveOptions.Oscillate;
        oscRate = rate;
        speedMult = multiplier;
    }

    public void Wait(float t, Action f)
    {
        startTime = Time.time;
        finishTime = Time.time + t;
        callWhenFinished = f;
        moveType = MoveOptions.Wait;
    }

    void FixedUpdate()
    {
        if(moveType != MoveOptions.Finished)
        {
            Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
            if (moveType == MoveOptions.MoveTo)
            {
                if((targetPos - currentPos).magnitude < 0.1f)
                {
                    rb.velocity = Vector2.zero;
                    FinishMovement();
                }
                else
                {
                    Vector2 direction = targetPos - currentPos;
                    rb.AddForce(direction.normalized * speed * speedMult);
                }
            }
            else if(moveType == MoveOptions.Oscillate)
            {
                if(finishTime <= Time.time)
                {
                    rb.velocity = Vector2.zero;
                    FinishMovement();
                }
                else
                {
                    float t = Time.time - startTime;
                    rb.velocity = new Vector2(Mathf.Cos(t * oscRate) * speed * speedMult, 0);
                }
            }
            else if(moveType == MoveOptions.Wait && finishTime <= Time.time)
            {
                FinishMovement();
            }

            if (rb.velocity.magnitude > speed)
            {
                rb.velocity = rb.velocity.normalized * speed;
            }
        }
    }

    private void FinishMovement()
    {
        moveType = MoveOptions.Finished;
        if (callWhenFinished != null)
        {
            callWhenFinished();
        }
    }
}
