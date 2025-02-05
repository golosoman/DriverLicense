using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [SerializeField]
    private SceneBuilder sceneBuilder; // Ссылка на SceneBuilder

    public delegate void ErrorOccurred(string message);
    public static event ErrorOccurred OnErrorOccurred;

    public delegate void SuccessOccurred(string message);
    public static event SuccessOccurred OnSuccessOccurred;

    public void SaveQuestion()
    {
        // Собираем данные из GlobalStateForConstructor
        PrimaryInfoData info = GlobalStateForConstructor.primaryInfoData;

        // Собираем данные о знаках, светофорах и участниках движения
        List<PlacedSignData> signs = new List<PlacedSignData>(sceneBuilder.GetSigns());
        List<PlacedTrafficLightData> trafficLights = new List<PlacedTrafficLightData>(sceneBuilder.GetTrafficLights());
        List<PlacedObjectData> trafficParticipants = new List<PlacedObjectData>(sceneBuilder.GetTrafficParticipants());

        // Валидация данных
        if (!ValidateTrafficParticipants(trafficParticipants))
        {
            return;
        }

        // Создаем объект запроса
        QuestionRequest questionRequest = new QuestionRequest(info, signs, trafficLights, trafficParticipants, sceneBuilder.GetIntersectionName());

        // Отправляем POST-запрос
        string url = "http://localhost:8080/api/questions"; // Укажите ваш URL
        ApiHandler.SendPostRequest(url, questionRequest, this, HandleSaveResponse);
    }

    private bool ValidateTrafficParticipants(List<PlacedObjectData> trafficParticipants)
    {
        if (trafficParticipants.Count < 2)
        {
            OnErrorOccurred?.Invoke("Количество участников движения должно быть не менее 2.");
            return false;
        }
        return true;
    }


    private void HandleSaveResponse(ApiResponse response)
    {
        Debug.Log("Status Code: " + response.StatusCode);
        Debug.Log("Response Body: " + response.Body);

        if (response.StatusCode == 201)
        {
            Debug.Log("Вопрос успешно сохранен!");
            OnSuccessOccurred?.Invoke("Вопрос успешно сохранен в БД.");
            sceneBuilder.ClearAllDict();
        }
        else
        {
            OnErrorOccurred?.Invoke("Ошибка " + response.StatusCode + " при отправке данных на сервер: " + response.Body);
            Debug.LogError("Ошибка " + response.StatusCode + " при сохранении билета: " + response.Body);
        }
    }
}
