using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class DatabaseLoader : MonoBehaviour
{
    private const string baseUrl = "http://localhost:5000";
    private CreateObjectManager createObjectManager;

    void Start()
    {
        createObjectManager = ScriptableObject.CreateInstance<CreateObjectManager>();
        StartCoroutine(LoadTicketData());
    }

    IEnumerator LoadTicketData()
    {
        string ticketId = "1";
        string url = $"{baseUrl}/tickets/{ticketId}";

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonData = request.downloadHandler.text;
            try
            {
                TicketData ticketData = JsonConvert.DeserializeObject<TicketData>(jsonData);
                Debug.Log("Ticket Data Loaded: " + ticketData.typeIntersection);
                createObjectManager.ProcessTicketData(ticketData);
            }
            catch (JsonException ex)
            {
                Debug.LogError("Failed to parse JSON data: " + ex.Message);
            }
        }
        else
        {
            Debug.LogError("Failed to load ticket data: " + request.error);
        }
    }

    
}

        

