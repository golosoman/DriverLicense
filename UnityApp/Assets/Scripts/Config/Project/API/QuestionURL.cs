public static class QuestionURLS
{
    public const string TICKET_ID = "1";
    public static string GET_TICKET_BY_ID => ProjectConfig.SERVER_URL + "api/questions/" + TICKET_ID;
}