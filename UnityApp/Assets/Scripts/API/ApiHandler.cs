using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public static class ApiHandler
{
    public static void SendGetRequest(string url, MonoBehaviour caller, System.Action<ApiResponse> callback, string bearerToken = null)
    {
        caller.StartCoroutine(GetRequest(url, callback, bearerToken));
    }

    private static IEnumerator GetRequest(string url, System.Action<ApiResponse> callback, string bearerToken)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            if (!string.IsNullOrEmpty(bearerToken))
            {
                www.SetRequestHeader("Authorization", "Bearer " + bearerToken);
            }

            yield return www.SendWebRequest();

            int statusCode = (int)www.responseCode;
            string responseBody = www.downloadHandler.text;

            ApiResponse response = new ApiResponse(statusCode, responseBody);

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                Debug.Log("Response: " + responseBody);
            }

            callback?.Invoke(response);
        }
    }

    public static void SendPostRequest(string url, BaseRequest requestData, MonoBehaviour caller, System.Action<ApiResponse> callback)
    {
        caller.StartCoroutine(PostRequest(url, requestData, callback));
    }

    private static IEnumerator PostRequest(string url, BaseRequest requestData, System.Action<ApiResponse> callback)
    {
        string jsonBody = JsonUtility.ToJson(requestData);
        Debug.Log(jsonBody);
        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            int statusCode = (int)www.responseCode;
            string responseBody = www.downloadHandler.text;

            ApiResponse response = new ApiResponse(statusCode, responseBody);

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                Debug.Log("Response: " + responseBody);
            }

            callback?.Invoke(response);
        }
    }
}
