using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapController : MonoBehaviour
{
    public List<Transform> snapPoints;
    public List<Draggable> draggableObjects;
    public float snapRange = 0.5f;
    private MicrogameJamController mgj;
    


    private void Start()
    {
        mgj = GameObject.Find("Microgame Controller").GetComponent<MicrogameJamController>();
        foreach(Draggable draggable in draggableObjects)
        {
            draggable.dragEndedCallback = OnDragEnded;
        }
    }


    private void OnDragEnded(Draggable draggable)
    {
        //closest distance to a snap point
        float closestDistance = -1;
        //closest snap point
        Transform closestSnapPoint = null;

        //Search all snap points, because the drag has just ended
        foreach (Transform snapPoint in snapPoints)
        {
            //The current distance is the distance between the draggable's position and the snap point
            float currentDistance = Vector2.Distance(draggable.transform.localPosition, snapPoint.localPosition);
            //If there is no closest snap point yet, or if this distance is smaller than the previous "closest distance"...
            if (closestSnapPoint == null || currentDistance < closestDistance)
            {
                //The closest snap point is THIS snap point! And the closest distance is the current distance right now!
                closestSnapPoint = snapPoint;
                closestDistance = currentDistance;
            }

        }
        
        //if a "closest snap point" exists and the closest distance is within the snap range,
        if (closestSnapPoint != null && closestDistance <= snapRange)
        {

            foreach (Draggable drag in draggableObjects)
            {
                if ((drag.transform.localPosition == closestSnapPoint.transform.localPosition) && (drag.gameObject != draggable.gameObject))
                    return;
            }

            //Set the position baybeee!!
            draggable.transform.localPosition = closestSnapPoint.localPosition;
        }

        CheckArrangement();
    }
    
    //Cursed as hell
    private void CheckArrangement()
    {
        bool correct = true;
        //From element 6 to 11, check
        for (int i = 0; i < 7; i++)
        {
            if (snapPoints[i+5].transform.localPosition != draggableObjects[i].transform.localPosition)
                correct = false;
        }

        if (correct)
            mgj.WinGame();

    }

}
