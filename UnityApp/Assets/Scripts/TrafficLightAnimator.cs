using UnityEngine;
using UnityEngine.UI;

public class TrafficLightAnimator : MonoBehaviour
{
    public Image redLight;
    public Image yellowLight;
    public Image greenLight;

    public TrafficLightData trafficLightData; // Данные светофора

    // Метод для установки начального состояния
    public void SetInitialState()
    {
        trafficLightData.State = "initial"; 
    }

    // Метод для переключения состояния
    public void SwitchState()
    {
        switch (trafficLightData.State)
        {
            case "initial":
                trafficLightData.State = "red"; 
                break;
            case "red":
                trafficLightData.State = "yellow";
                break;
            case "yellow":
                trafficLightData.State = "green";
                break;
            case "green":
                trafficLightData.State = "yellow";
                break;
        }
    }
}
