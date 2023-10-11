using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private bool dash = false;
    private Vector2 dashDirection;
    private Vector2 inputDirection;
    private Vector2 lastHeldDirection = Vector2.up;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();
    }

    /// <summary>
    /// Gets the player input and sets the appropriate variables.
    /// </summary>
    private void ReadInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector2(horizontal, vertical);
        lastHeldDirection = mostRecentNonZeroInput();

        if (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.Space))
        {
            dash = true;
            dashDirection = mostRecentNonZeroInput();
        }
    }

    /// <summary>
    /// Determines the most recent direction input provided by the player that is not 0.
    /// </summary>
    /// <returns>The player's most recent non-zero input.</returns>
    private Vector2 mostRecentNonZeroInput()
    {
        return inputDirection != Vector2.zero ? inputDirection : lastHeldDirection;
    }

    private void FixedUpdate()
    {
        if (dash)
        {
            dash = false;
            playerMovement.Dash(dashDirection);
        }

        playerMovement.MoveDirection(inputDirection);
    }
}
