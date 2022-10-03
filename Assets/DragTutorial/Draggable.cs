using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public GameObject hand;
    public Hand handComponent;
    public delegate void DragEndedDelegate(Draggable draggableObject);

    public DragEndedDelegate dragEndedCallback;

    private bool colliding = false;
    private bool isDragged = false;
    private Vector3 handDragStartPosition;
    private Vector3 spriteDragStartPosition;
    public GameObject shadowRef;
    private GameObject shadow = null;

    private void Start()
    {
        hand = GameObject.Find("Hand");
        handComponent = hand.GetComponent<Hand>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Hand Collider")
            colliding = true;
        else
            colliding = false;
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Hand Collider")
            colliding = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (colliding)
            {
                isDragged = true;
                handDragStartPosition = hand.transform.GetChild(0).transform.localPosition;
                spriteDragStartPosition = transform.localPosition;
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 2, -4f);
                shadow = Instantiate(shadowRef, new Vector3(transform.localPosition.x, transform.localPosition.y, -3f), new Quaternion());
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (isDragged)
            {
                transform.localPosition = spriteDragStartPosition + (hand.transform.GetChild(0).transform.localPosition);
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 2, -4f);
                shadow.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 2, -3f);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isDragged)
            {
                handComponent.isDragging = false;
                isDragged = false;
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 2, -2f);
                dragEndedCallback(this);
                Destroy(shadow);
            }

        }

    }
}
