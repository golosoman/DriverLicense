using System.Collections.Generic;

[System.Serializable]
public class TicketRequest : BaseRequest
{
    public string name; // Имя билета
    public List<int> questionIds; // Список идентификаторов вопросов

    public TicketRequest(string name, List<int> questionIds)
    {
        this.name = name;
        this.questionIds = questionIds;
    }
}