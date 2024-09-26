public static class TicketURL
{
    public const string TICKET_ID = "1";
    public static string GET_TICKET_BY_ID => ProjectConfig.SERVER_URL + "/tickets/" + TICKET_ID;
}