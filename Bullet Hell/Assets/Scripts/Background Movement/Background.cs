using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{   
    public float speed;

    [SerializeField] 
    private Renderer bgRenderer;
    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(0f,speed * Time.deltaTime);
    }
}
