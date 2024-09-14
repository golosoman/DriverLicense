using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class DatabaseLoader : MonoBehaviour
{
    private const string baseUrl = "http://localhost:5000";
    private CreateObjectManager createObjectManager;
    // private TrafficRulesManager trafficRulesManager;

    void Start()
    {
        createObjectManager = ScriptableObject.CreateInstance<CreateObjectManager>();
        // trafficRulesManager = FindObjectOfType<TrafficRulesManager>();

        // if (trafficRulesManager == null)
        // {
        //     Debug.LogError("TrafficRulesManager not found in the scene.");
        //     return;
        // }

        StartCoroutine(LoadTicketData());
    }

    IEnumerator LoadTicketData()
    {
        string ticketId = "2";
        string url = $"{baseUrl}/tickets/{ticketId}";

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonData = request.downloadHandler.text;
            try
            {
                TicketData ticketData = JsonConvert.DeserializeObject<TicketData>(jsonData);
                Debug.Log("Ticket Data Loaded: " + ticketData.TypeIntersection);
                createObjectManager.ProcessTicketData(ticketData);
                // trafficRulesManager.Initialize(ticketData);
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


        

