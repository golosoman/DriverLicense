using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class DatabaseLoader : MonoBehaviour
{
    private CreateObjectManager createObjectManager;
    // private TrafficRulesManager trafficRulesManager;

    void Start()
    {
        createObjectManager = ScriptableObject.CreateInstance<CreateObjectManager>();
        StartCoroutine(LoadTicketData());
    }

    IEnumerator LoadTicketData()
    {
        UnityWebRequest request = UnityWebRequest.Get(TicketURL.GET_TICKET_BY_ID);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonData = request.downloadHandler.text;
            try
            {
                TicketData ticketData = JsonConvert.DeserializeObject<TicketData>(jsonData);
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


        

