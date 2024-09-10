using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadUserController : MonoBehaviour
{
    private RoadUserMovement roadUserMovement;

    void Start()
    {
        roadUserMovement = GetComponent<RoadUserMovement>();
        Debug.Log(roadUserMovement.GetLengthRoute());
        if (roadUserMovement != null)
        {
            Debug.Log("RoadUserMovement is found. Route length: " + roadUserMovement.GetLengthRoute());
        }
        else
        {
            Debug.LogError("RoadUserMovement component is missing on the roadUser object.");
        }
    }

    void OnMouseDown()
    {
        if (roadUserMovement != null)
        {
            roadUserMovement.StartMovement();
        }
        else
        {
            Debug.LogError("RoadUserMovement is null. Cannot start movement.");
        }
    }
}
