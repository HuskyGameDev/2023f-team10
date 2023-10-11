using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer sr;
    private Sprite defaultSprite;
    [SerializeField] private Sprite dashingSprite;
    private CapsuleCollider2D hurtbox;
    private Rigidbody2D rb;
    
    [SerializeField] private float maxSpeed;
    public float MaxSpeed { 
        get 
        { 
            return maxSpeed; 
        } 
        set 
        { 
            maxSpeed = value; 
        } 
    }

    [SerializeField] private float acceleration;
    [SerializeField] private float dashSpeed;
    /// <summary>
    /// How long the player has to wait between dashes.
    /// </summary>
    [SerializeField] private float dashInterval;
    [SerializeField] private float dashDistance;
    /// <summary>
    /// How long the player remains invulnerable after the dash ends.
    /// </summary>
    [SerializeField] private float extraInvulTime;
    /// <summary>
    /// How much further the dash will go when maxSpeed is increased.
    /// </summary>
    [SerializeField] private float extraDistancePerSpeed;

    private float nextDashTime = 0;

    private bool isDashing = false;
    public bool IsDashing { get; private set; }

    private bool CanDash
    {
        get { return Time.time >= nextDashTime; } 
    }

    private float DashDuration
    {
        get { return (dashDistance + (extraDistancePerSpeed * (maxSpeed - 1))) / (dashSpeed * maxSpeed); }
    }

    private bool dashStopped = false;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultSprite = sr.sprite;
        rb = GetComponent<Rigidbody2D>();
        hurtbox = GetComponent<CapsuleCollider2D>();
    }

    public void MoveDirection(Vector2 direction)
    {
        if(!isDashing)
        {
            if(direction.magnitude > 0)
            {
                rb.AddForce(acceleration * maxSpeed * direction);
            }
            else if(direction.magnitude == 0 && rb.velocity.magnitude > 0.01f)
            {
                Decelerate();
            }
        
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
        else
        {
            if(direction.magnitude > 0 && Vector2.Angle(direction, rb.velocity) > 45)
            {
                dashStopped = true;
            }
            if(dashStopped)
            {
                dashStopped = false;
                isDashing = false;

                StartCoroutine(EndDashInvulRoutine());

                if (direction.magnitude > 0)
                {
                    rb.velocity = direction.normalized * maxSpeed / 2;
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }
            }
        }
    }

    /// <summary>
    /// Reduces velocity to 0. Effectively friction.
    /// </summary>
    private void Decelerate()
    {
        if(rb.velocity.magnitude > acceleration * maxSpeed * Time.fixedDeltaTime)
        {
            rb.AddForce(-1f * acceleration * maxSpeed * rb.velocity.normalized);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Uses the player's dash ability if possible.
    /// </summary>
    /// <param name="direction"></param>
    public void Dash(Vector2 direction)
    {
        if(CanDash)
        {
            isDashing = true;
            sr.sprite = dashingSprite;
            nextDashTime = Time.time + DashDuration + dashInterval + extraInvulTime;
            rb.velocity = direction.normalized * dashSpeed * maxSpeed;
            hurtbox.enabled = false;
            StartCoroutine(DashRoutine());
        }
    }

    /// <summary>
    /// Resets isDashing to false once the DashDuration has expired.  
    /// </summary>
    private IEnumerator DashRoutine()
    {
        yield return new WaitForSeconds(DashDuration);
        if(isDashing)
        {
            dashStopped = true;
        }
    }

    private IEnumerator EndDashInvulRoutine()
    {
        yield return new WaitForSeconds(extraInvulTime);
        sr.sprite = defaultSprite;
        hurtbox.enabled = true;
    }
}
