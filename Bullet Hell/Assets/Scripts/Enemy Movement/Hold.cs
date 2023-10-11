using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//moves the object this is on (like an enemy) to a random point within a radius around a target
public class Hold : MonoBehaviour
{
    //set these variables in the editor
    [SerializeField] GameObject targetOrigin = null;
    [SerializeField] float targetingRadius = 0;
    [SerializeField] float speed = 0;

    Vector3 direction = Vector3.up;

    //call on awake so this is run when the object is instantiated during runtime
    private void Awake()
    {
        //pick a location within the given radius around the given target objects position
        Vector2 originPos = new Vector2(targetOrigin.transform.position.x, targetOrigin.transform.position.y);
        Vector3 target = originPos + (Random.insideUnitCircle * targetingRadius);

        //go to the target location
        direction = (target - transform.position).normalized;
    }


    private void FixedUpdate()
    {
        //rotate object towards the target (might need to make positive)
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        gameObject.GetComponent<Rigidbody2D>().rotation = -angle;

        //move towards the target
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x, direction.y) * speed * Time.fixedDeltaTime);
    }
}
