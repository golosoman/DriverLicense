using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [SerializeField]
    private SceneBuilder sceneBuilder; // Ссылка на SceneBuilder

    public void SaveQuestion()
    {
        // Собираем данные из GlobalStateForConstructor
        PrimaryInfoData info = GlobalStateForConstructor.primaryInfoData;

        // Собираем данные о знаках, светофорах и участниках движения
        List<PlacedSignData> signs = new List<PlacedSignData>(sceneBuilder.GetSigns());
        List<PlacedTrafficLightData> trafficLights = new List<PlacedTrafficLightData>(sceneBuilder.GetTrafficLights());
        List<PlacedObjectData> trafficParticipants = new List<PlacedObjectData>(sceneBuilder.GetTrafficParticipants());
        // Создаем объект запроса
        QuestionRequest questionRequest = new QuestionRequest(info, signs, trafficLights, trafficParticipants, sceneBuilder.GetIntersectionName());

        // Отправляем POST-запрос
        string url = "http://localhost:8080/api/questions"; // Укажите ваш URL
        ApiHandler.SendPostRequest(url, questionRequest, this, HandleSaveResponse);
    }

    private void HandleSaveResponse(ApiResponse response)
    {
        Debug.Log("Status Code: " + response.StatusCode);
        Debug.Log("Response Body: " + response.Body);

        if (response.StatusCode == 201)
        {
            Debug.Log("Билет успешно сохранен!");
        }
        else
        {
            Debug.LogError("Ошибка " + response.StatusCode + " при сохранении билета: " + response.Body);
        }
    }
}
