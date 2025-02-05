using UnityEngine;

public class StatisticsUploader : MonoBehaviour
{
    private string apiUrl = "http://localhost:8080/api/statistics";

    void OnEnable()
    {
        UploadingQuestionsFromTicket.OnTicketCompleted += HandleTicketCompleted;
    }

    void OnDisable()
    {
        UploadingQuestionsFromTicket.OnTicketCompleted -= HandleTicketCompleted;
    }

    private void HandleTicketCompleted(StatisticRequest statisticRequest)
    {
        string bearerToken = GlobalState.userToken; // Получаем токен
        ApiHandler.SendPostRequest(apiUrl, statisticRequest, this, OnStatisticsSent, bearerToken);
    }

    private void OnStatisticsSent(ApiResponse response)
    {
        if (response.StatusCode != 201)
        {
            Debug.LogError("Error sending statistics: " + response.Body);
        }
        else
        {
            Debug.Log("Statistics sent successfully: " + response.Body);
        }
    }
}
