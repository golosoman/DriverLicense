using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetPriority : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
            CarMovement carMovement = other.gameObject.GetComponent<CarMovement>();
            carMovement.HasPriority = gameObject.name.Contains("YieldSign") ? true : false;
            Debug.Log(other.gameObject.name + "    " + carMovement.HasPriority);
        }
    }
}
