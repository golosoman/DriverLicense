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
        Debug.Log(QuestionURL.GET_TICKET_BY_ID);
        UnityWebRequest request = UnityWebRequest.Get(QuestionURL.ALL_QUESTION_URL + "/" + GlobalState.questionId);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonData = request.downloadHandler.text;
            Debug.Log(jsonData);
            try
            {
                Question ticketData = JsonConvert.DeserializeObject<Question>(jsonData);
                Debug.Log(ticketData);
                // Debug.Log(ticketData.Id + " " + ticketData.IntersectionType + " " + ticketData.Question + " " + ticketData.Explanation + " " + ticketData.SignsArr.ToString() + " " + ticketData.TrafficLightsArr.ToString() + " " + ticketData.TrafficParticipantsArr.ToString() + " ");
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




