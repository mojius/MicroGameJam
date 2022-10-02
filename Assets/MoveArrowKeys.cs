using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveArrowKeys : MonoBehaviour
{
    
    public float speed = 10.4f;
    
    void Update()
    {
        transform.Translate(speed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, (speed * Input.GetAxis("Vertical")) * Time.deltaTime, 0f);
    }
}
