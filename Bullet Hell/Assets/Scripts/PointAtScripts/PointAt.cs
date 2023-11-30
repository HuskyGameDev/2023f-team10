using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAt : MonoBehaviour
{
    protected Vector3 target;
    [SerializeField] private float jitterAmount = 0;

    protected virtual void Update()
    {
        transform.right = target - transform.position;
        float jitter = Random.Range(-jitterAmount, jitterAmount);
        transform.Rotate(new Vector3(0, 0, 1), -90 + jitter);
    }

}
