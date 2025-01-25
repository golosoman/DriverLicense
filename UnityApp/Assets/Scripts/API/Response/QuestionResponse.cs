using System.Collections.Generic;
using JetBrains.Annotations;

[System.Serializable]
public class TrafficParticipant
{
    public int id;
    public string modelName;
    public string sidePosition;
    public string numberPosition;
    public string direction;
    public string participantType;
}

[System.Serializable]
public class Sign
{
    public int id;
    public string modelName;
    public string sidePosition;
}

[System.Serializable]
public class TrafficLight
{
    public int id;
    public string modelName;
    public string sidePosition;
    public string state;
}

[System.Serializable]
public class Question
{
    public int id;
    public string question;
    public string explanation;
    public string intersectionType;
    public TrafficParticipant[] trafficParticipants;
    public Sign[] signs;
    public TrafficLight[] trafficLights;
    public Category category;
}

[System.Serializable]
public class QuestionList
{
    public List<Question> questions;
}
