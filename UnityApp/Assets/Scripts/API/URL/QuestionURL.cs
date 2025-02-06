public static class QuestionURL
{
    public static string GET_QUESTION_BY_ID(int questionId) => ProjectConfig.SERVER_URL + "api/questions/" + questionId;
    public static string ALL_QUESTION_URL => ProjectConfig.SERVER_URL + "api/questions";
    public static string RANDOM_QESTION_URL => ProjectConfig.SERVER_URL + "api/questions/random";
}