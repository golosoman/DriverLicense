using UnityEngine;
using System.Collections.Generic;

public class TicketManager : MonoBehaviour
{
    public void CreateTicket(string ticketName, List<int> questionIds)
    {
        string apiUrl = "http://localhost:8080/api/tickets"; // URL для создания нового билета

        TicketRequest newTicket = new TicketRequest(ticketName, questionIds); // Создаем новый билет

        // Отправляем POST-запрос
        ApiHandler.SendPostRequest(apiUrl, newTicket, this, OnTicketCreated);
    }

    private void OnTicketCreated(ApiResponse response)
    {
        if (response.StatusCode == 201) // Успешное создание (обычно 201 для POST)
        {
            Debug.Log("Билет успешно создан: " + response.Body);
        }
        else
        {
            Debug.LogError("Ошибка при создании билета: " + response.Body);
        }
    }
}
