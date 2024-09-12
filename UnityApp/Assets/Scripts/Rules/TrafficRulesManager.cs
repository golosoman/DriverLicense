using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class TrafficRulesManager : MonoBehaviour
{
    private TicketData currentTicket;
    private List<RoadUserMovement> roadUsers;
    private List<int> userSequence;
    private List<int> correctSequence;

    public void Initialize(TicketData ticketData)
    {
        currentTicket = ticketData;
        roadUsers = new List<RoadUserMovement>();
        userSequence = new List<int>();
        correctSequence = new List<int>();

        // Заполняем список участников движения
        foreach (var roadUserData in ticketData.roadUsersArr)
        {
            GameObject[] roadUserObjects;
            switch (roadUserData.typeParticipant)
            {
                case "car":
                    roadUserObjects = GameObject.FindGameObjectsWithTag("Car");
                    break;
                case "human":
                    roadUserObjects = GameObject.FindGameObjectsWithTag("Human");
                    break;
                case "tram":
                    roadUserObjects = GameObject.FindGameObjectsWithTag("Tram");
                    break;
                default:
                    Debug.LogWarning($"Unknown typeParticipant: {roadUserData.typeParticipant}. Using default tag.");
                    roadUserObjects = GameObject.FindGameObjectsWithTag("Untagged");
                    break;
            }

            var roadUser = roadUserObjects.FirstOrDefault(r => r.name == $"{roadUserData.modelName}_{roadUserData.sidePosition}_{roadUserData.numberPosition}")?.GetComponent<RoadUserMovement>();
            if (roadUser != null)
            {
                roadUsers.Add(roadUser);
            }
            else
            {
                Debug.LogError($"RoadUserMovement component not found for model: {roadUserData.modelName} and position: {roadUserData.sidePosition}");
            }
        }

        // Определяем правильную последовательность движения
        DetermineCorrectSequence();
    }

    public void ResetUserSequence()
    {
        userSequence.Clear();
    }

    private void DetermineCorrectSequence()
    {
        // Здесь вы определяете правильную последовательность движения
        // Например, если правильная последовательность — это объекты с индексами 0, 2, 1, 3
        correctSequence.Add(0);
        correctSequence.Add(2);
        correctSequence.Add(1);
    }

    public void UserSelectRoadUser(RoadUserMovement roadUser)
    {
        int index = roadUsers.IndexOf(roadUser);
        if (index != -1)
        {
            userSequence.Add(index);

            // Проверяем, выбрал ли пользователь всех участников движения
            if (userSequence.Count == correctSequence.Count)
            {
                CheckUserSequence();
            }
        }
    }



    private void CheckUserSequence()
    {
        if (userSequence.Count != correctSequence.Count)
        {
            ShowResultMessage("Incorrect sequence: The number of selected road users does not match the correct sequence.");
            return;
        }

        for (int i = 0; i < userSequence.Count; i++)
        {
            if (userSequence[i] != correctSequence[i])
            {
                ShowResultMessage("Incorrect sequence: The order of selected road users does not match the correct sequence.");
                return;
            }
        }

        ShowResultMessage("Correct sequence: The order of selected road users matches the correct sequence.");
        // Здесь вы можете добавить логику для обработки правильной последовательности, например, завершение задания или переход к следующему этапу.
    }

    private void ShowResultMessage(string message)
    {
        // Пример отображения сообщения на UI-элементе
        Debug.Log(message);
        // Вместо Debug.Log можно использовать UI-элемент для отображения сообщения пользователю
    }
}
