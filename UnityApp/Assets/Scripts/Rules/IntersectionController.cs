using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionController : MonoBehaviour
{
    public bool isRegulated; // Регулируемый или нерегулируемый

    public void HandleIntersection(GameObject roadUser)
    {
        RoadUserMovement movement = roadUser.GetComponent<RoadUserMovement>();
        if (movement != null)
        {
            if (isRegulated)
            {
                HandleTrafficLights(movement);
            }
            else
            {
                HandleSigns(movement);
                CheckRightOfWay(movement);
            }
        }
    }

    private void HandleTrafficLights(RoadUserMovement movement)
    {
        TrafficLightController light = FindObjectOfType<TrafficLightController>();
        if (light != null && !light.IsGreenLight())
        {
            movement.StopMovement();
        }
    }

    private void HandleSigns(RoadUserMovement movement)
    {
        SignController sign = FindObjectOfType<SignController>();
        if (sign != null && sign.ShouldYield())
        {
            movement.StopMovement();
        }
    }

    private void CheckRightOfWay(RoadUserMovement movement)
    {
        // Проверка на наличие помехи справа
    }
}
