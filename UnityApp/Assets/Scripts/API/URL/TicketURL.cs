public static class TicketURL
{
    public const string TICKET_ID = "1";
    public static string GET_TICKET_BY_ID => ProjectConfig.SERVER_URL + "api/tickets/" + TICKET_ID;
    public static string ALL_TICKET_URL => ProjectConfig.SERVER_URL + "api/tickets";
    public static string RANDOM_TICKET_URL => ProjectConfig.SERVER_URL + "api/tickets/random";
}