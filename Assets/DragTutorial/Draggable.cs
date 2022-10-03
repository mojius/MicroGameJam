using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public GameObject hand;
    public GameObject handChild;
    public Hand handComponent;
    public delegate bool DragEndedDelegate(Draggable draggableObject);

    public DragEndedDelegate dragEndedCallback;

    private bool colliding;
    private bool isDragged = false;
    private Vector3 handDragStartPosition;
    private Vector3 spriteDragStartPosition;
    public GameObject shadowRef;
    private GameObject shadow = null;

    private AudioSource pickup;
    private AudioSource drop;

    private void Start()
    {
        colliding = false;
        hand = GameObject.Find("Hand");
        handChild = hand.transform.GetChild(0).gameObject;
        handComponent = hand.GetComponent<Hand>();

        pickup = GetComponents<AudioSource>()[0];
        drop = GetComponents<AudioSource>()[1];

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Hand Collider")
            colliding = true;
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
            if (colliding && handComponent.isDragging == false)
            {
                //confirm status as being dragged, and make the hand's status to be dragged
                isDragged = true;
                handComponent.isDragging = true;
                handComponent.anim.SetBool("Dragging", true);
                //move the pencil 2 units up
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 2, -4f);
                pickup.Play();
            }


            //attempt to put the pencil down
            else if (isDragged && handComponent.isDragging)
            {
                //if you do put the pencil down, move it back down 2 units and reset the states
                //if 
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 2, -2f);
                if (dragEndedCallback(this))
                {
                    isDragged = false;
                    handComponent.isDragging = false;
                    handComponent.anim.SetBool("Dragging", false);
                    drop.Play();
                }
                else
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 2, -4f);

                }
            }
        }


        if (isDragged)
        {
            //The pencil should follow the x coordinate of the hand box collider
            transform.localPosition = new Vector3(handChild.transform.position.x, transform.localPosition.y, transform.localPosition.z);
        }

    }



}
