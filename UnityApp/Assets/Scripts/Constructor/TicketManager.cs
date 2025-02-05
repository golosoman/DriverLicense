using UnityEngine;
using System.Collections.Generic;

public class TicketManager : MonoBehaviour
{
    public delegate void ErrorOccurred(string message);
    public static event ErrorOccurred OnErrorOccurred;

    public void CreateTicket(string ticketName, List<int> questionIds)
    {
        if (!ValidateTicket(ticketName, questionIds))
        {
            return;
        }

        string apiUrl = "http://localhost:8080/api/tickets";

        TicketRequest newTicket = new TicketRequest(ticketName, questionIds);

        ApiHandler.SendPostRequest(apiUrl, newTicket, this, OnTicketCreated);
    }

    private bool ValidateTicket(string ticketName, List<int> questionIds)
    {
        if (string.IsNullOrEmpty(ticketName) || ticketName.Length < 5 || ticketName.Length > 30)
        {
            OnErrorOccurred?.Invoke("Название билета должно содержать от 5 до 30 символов.");
            return false;
        }

        if (questionIds.Count != 5)
        {
            OnErrorOccurred?.Invoke("Билет должен содержать ровно 5 вопросов.");
            return false;
        }

        return true;
    }

    private void OnTicketCreated(ApiResponse response)
    {
        if (response.StatusCode == 201)
        {
            Debug.Log("Билет успешно создан: " + response.Body);
        }
        else
        {
            OnErrorOccurred?.Invoke("Ошибка при создании билета: " + response.Body);
        }
    }
}
