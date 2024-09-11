using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    private string currentLight = "Red";

    public void SetInitialState(string lightColor)
    {
        currentLight = lightColor;
        // Обновить визуализацию светофора
    }

    public bool IsGreenLight()
    {
        return currentLight == "Green";
    }

    public void SwitchLight()
    {
        currentLight = currentLight == "Red" ? "Green" : "Red";
        // Обновить визуализацию светофора
    }
}
