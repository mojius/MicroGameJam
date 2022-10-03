using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hand : MonoBehaviour
{
    
    public float speed = 10.4f;
    public bool isDragging = false;
    
    void Update()
    {
        transform.Translate(speed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f,0f, 0f);
    }
}
