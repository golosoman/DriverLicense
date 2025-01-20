[System.Serializable]
public class QuestionStatistics
{
    public long id;
    public string question;
    public double percentage;
}

[System.Serializable]
public class TicketStatistics
{
    public long id;
    public string ticketName;
    public double percentage;
}

[System.Serializable]
public class AdminStatisticsResponse
{
    public QuestionStatistics[] questionStatistics;
    public TicketStatistics[] ticketStatistics;
}

// Классы для десериализации JSON
[System.Serializable]
public class UserStatisticsResponse
{
    public CategoryStatistic[] categoryStatistics;
    public TicketStatistic[] ticketStatistics;
}

[System.Serializable]
public class TicketStatistic
{
    public int id;
    public string ticketName;
    public string date; // Дата в формате строки
    public int countErrors;
    public bool status;

    public string GetStatus()
    {
        return status ? "Сдал" : "Не сдал"; // Возвращаем статус
    }
}

[System.Serializable]
public class CategoryStatistic
{
    public int id;
    public string categoryName;
    public float percentage;
}