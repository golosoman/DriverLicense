using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public static class ApiHandler
{
    public static void SendPostRequest(string url, BaseRequest requestData, MonoBehaviour caller, System.Action<ApiResponse> callback)
    {
        caller.StartCoroutine(PostRequest(url, requestData, callback));
    }

    private static IEnumerator PostRequest(string url, BaseRequest requestData, System.Action<ApiResponse> callback)
    {
        // Преобразуем данные в JSON
        string jsonBody = JsonUtility.ToJson(requestData);

        // Создаем UnityWebRequest с использованием UploadHandlerRaw для отправки JSON
        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json"); // Устанавливаем заголовок Content-Type

            // Отправляем запрос и ждем ответа
            yield return www.SendWebRequest();

            // Получаем код ответа и тело ответа
            int statusCode = (int)www.responseCode;
            string responseBody = www.downloadHandler.text;

            // Создаем объект ApiResponse
            ApiResponse response = new ApiResponse(statusCode, responseBody);

            // Проверяем наличие ошибок и выводим сообщение
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                Debug.Log("Response: " + responseBody);
            }

            // Вызываем коллбек с ответом
            callback?.Invoke(response);
        }
    }
}
