using System.Collections.Generic;
using UnityEngine;

public class PlacedObjectData
{
    public string participantType; // Тип объекта (например, "Car", "Sign", "TrafficLight")
    public string modelName; // Позиция объекта
    public string direction; // Вращение объекта
    public string numberPosition; // Дополнительная информация, если необходимо
    public string sidePosition;
    public TriggerCarSpawnZone srcBySpawnPoint;
}

public class PrimaryInfoData
{
    public string question;
    public string explanation;
    public string categoryName;

    public PrimaryInfoData(string question, string explanation, string categoryName)
    {
        this.question = question;
        this.explanation = explanation;
        this.categoryName = categoryName;
    }
}

public class PlacedSignData
{
    public string modelName; // Позиция объекта
    public string sidePosition;
    public TriggerSignSpawnZone srcBySpawnPoint;
}

public class PlacedTrafficLightData
{
    public string modelName; // Позиция объекта
    public string sidePosition;
    public string state;
    public TriggerTrafficLightZone srcBySpawnPoint;
}

[System.Serializable]
public class SignRequest
{
    public string modelName;
    public string sidePosition;

    public SignRequest(PlacedSignData placedSignData)
    {
        this.modelName = ExtractSignModelName(placedSignData.modelName);
        this.sidePosition = placedSignData.sidePosition;
    }

    private string ExtractSignModelName(string modelName)
    {
        // Извлечение названия знака
        int startIndex = modelName.IndexOf("Img") + 3; // Индекс после "Img"
        int endIndex = modelName.IndexOf("("); // Индекс первой открывающейся скобки
        return modelName.Substring(startIndex, endIndex - startIndex);
    }
}

[System.Serializable]
public class TrafficLightRequest
{
    public string modelName;
    public string sidePosition;
    public string state;

    public TrafficLightRequest(PlacedTrafficLightData placedTrafficLightData)
    {
        this.modelName = ExtractTrafficLightModelName(placedTrafficLightData.modelName);
        this.sidePosition = placedTrafficLightData.sidePosition; // Исправлено: должно быть sidePosition
        this.state = ExtractTrafficLightState(placedTrafficLightData.modelName);
    }

    private string ExtractTrafficLightModelName(string modelName)
    {
        // Извлечение названия светофора
        int startIndex = modelName.IndexOf("Img") + 3; // Индекс после "Img"
        int endIndex = modelName.IndexOf("("); // Индекс первой открывающейся скобки
        return modelName.Substring(startIndex, endIndex - startIndex);
    }

    private string ExtractTrafficLightState(string modelName)
    {
        // Определение состояния светофора
        if (modelName.ToLower().Contains("red"))
        {
            return "Red";
        }
        else if (modelName.ToLower().Contains("green"))
        {
            return "Green";
        }
        return "Unknown"; // Если состояние не определено
    }
}

[System.Serializable]
public class TrafficParticipantRequest
{
    public string modelName;
    public string sidePosition;
    public string numberPosition;
    public string participantType;
    public string direction;

    public TrafficParticipantRequest(PlacedObjectData placedObjectData)
    {
        this.modelName = ExtractCarModelName(placedObjectData.modelName);
        this.sidePosition = placedObjectData.sidePosition;
        this.numberPosition = placedObjectData.numberPosition;
        this.participantType = placedObjectData.participantType;
        this.direction = placedObjectData.direction;
    }

    private string ExtractCarModelName(string modelName)
    {
        // Извлечение названия автомобиля
        int startIndex = modelName.IndexOf("Img") + 3; // Индекс после "Img"
        int endIndex = modelName.IndexOf("("); // Индекс первой открывающейся скобки
        return modelName.Substring(startIndex, endIndex - startIndex);
    }
}

[System.Serializable]
public class QuestionRequest : BaseRequest
{
    public string question;
    public string explanation;
    public string categoryName;
    public string intersectionType;
    public List<SignRequest> signs;
    public List<TrafficLightRequest> trafficLights;
    public List<TrafficParticipantRequest> trafficParticipants;

    public QuestionRequest(PrimaryInfoData info, List<PlacedSignData> signs, List<PlacedTrafficLightData> trafficLights,
        List<PlacedObjectData> trafficParticipants, string intersectionType)
    {
        this.question = info.question;
        this.explanation = info.explanation;
        this.categoryName = info.categoryName;

        this.intersectionType = ExtractIntersectionType(intersectionType);

        // Инициализация списков
        this.signs = new List<SignRequest>();
        foreach (var sign in signs)
        {
            this.signs.Add(new SignRequest(sign));
        }

        this.trafficLights = new List<TrafficLightRequest>();
        foreach (var trafficLight in trafficLights)
        {
            this.trafficLights.Add(new TrafficLightRequest(trafficLight));
        }

        this.trafficParticipants = new List<TrafficParticipantRequest>();
        foreach (var participant in trafficParticipants)
        {
            this.trafficParticipants.Add(new TrafficParticipantRequest(participant));
        }
    }

    private string ExtractIntersectionType(string intersectionType)
    {
        // Извлечение названия перекрестка
        int endIndex = intersectionType.IndexOf("("); // Индекс первой открывающейся скобки
        return endIndex > 0 ? intersectionType.Substring(0, endIndex) : intersectionType; // Возвращаем строку до скобки
    }
}
