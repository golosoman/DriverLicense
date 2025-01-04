public static class QuestionURL
{
    public const string TICKET_ID = "1";
    public static string GET_TICKET_BY_ID => ProjectConfig.SERVER_URL + "api/questions/" + TICKET_ID;
    public static string ALL_QUESTION_URL => ProjectConfig.SERVER_URL + "api/questions";
    public static string RANDOM_QESTION_URL => ProjectConfig.SERVER_URL + "api/questions/random";

}