using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public GameObject hand;
    public delegate void DragEndedDelegate(Draggable draggableObject);

    public DragEndedDelegate dragEndedCallback;

    private bool isDragged = false;
    private Vector3 mouseDragStartPosition;
    private Vector3 spriteDragStartPosition;

    private void Start()
    {
        hand = GameObject.Find("Hand");
    }
    private void OnMouseDown()
    {
        isDragged = true;
        transform.localPosition += Vector3.up;
        mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spriteDragStartPosition = transform.localPosition;
    }

    private void OnMouseDrag()
    {
        if (isDragged)
        {
            transform.localPosition = spriteDragStartPosition + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDragStartPosition);
            hand.transform.localPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector3.forward);
        }
    }

    private void OnMouseUp()
    {
        isDragged = false;
        transform.localPosition += Vector3.down;
        dragEndedCallback(this);
    }
}
