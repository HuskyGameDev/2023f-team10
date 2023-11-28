using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float speed = 5f; // Adjust this to control the speed of the background.

    private Vector2 initialPosition;
    private float minY = -10f;
    private float maxY = 10f;
    private float minX = -5f;
    private float maxX = 5f;

    void Start()
    {
        // Randomly choose x and y positions within the specified range.
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        initialPosition = new Vector2(randomX, randomY); // Set the initial position.
        transform.position = initialPosition; // Move the background to the initial position.
    }

    void Update()
    {
        // Move the background downward
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Check if the background has gone off the screen
        if (transform.position.y < minY)
        {
            // Randomly choose a new x and y position within the specified range.
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            // Reset the background to the new random position at the top
            transform.position = new Vector2(randomX, maxY);
        }
    }
}