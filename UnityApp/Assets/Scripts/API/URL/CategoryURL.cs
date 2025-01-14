public static class CategoryURL
{
    public const string CATEGORY_ID = "1";
    public static string GET_CATEGORY_BY_ID => ProjectConfig.SERVER_URL + "api/categories/" + CATEGORY_ID;
    public static string ALL_CATEGORY_URL => ProjectConfig.SERVER_URL + "api/categories";
}