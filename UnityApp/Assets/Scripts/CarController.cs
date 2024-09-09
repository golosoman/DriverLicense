using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private CarMovement carMovement;

    void Start()
    {
        carMovement = GetComponent<CarMovement>();
        Debug.Log(carMovement.GetLengthRoute());
        if (carMovement != null)
        {
            Debug.Log("CarMovement is found. Route length: " + carMovement.GetLengthRoute());
        }
        else
        {
            Debug.LogError("CarMovement component is missing on the car object.");
        }
    }

    void OnMouseDown()
    {
        if (carMovement != null)
        {
            // Debug.Log(carMovement.route.Length);
            carMovement.StartMovement();
        }
        else
        {
            Debug.LogError("CarMovement is null. Cannot start movement.");
        }
    }
}
