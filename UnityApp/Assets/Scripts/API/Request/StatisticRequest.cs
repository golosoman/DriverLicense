using System.Collections.Generic;

[System.Serializable]
public class StatisticRequest : BaseRequest
{
    public int ticketId;
    public bool result;
    public List<Answer> answers;
}

[System.Serializable]
public class Answer
{
    public int questionId;
    public bool result;
}