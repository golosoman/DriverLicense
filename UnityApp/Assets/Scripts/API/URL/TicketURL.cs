public static class TicketURL
{
    public static string GET_TICKET_BY_ID(int ticketId) => ProjectConfig.SERVER_URL + "api/tickets/" + ticketId;
    public static string ALL_TICKET_URL => ProjectConfig.SERVER_URL + "api/tickets";
    public static string RANDOM_TICKET_URL => ProjectConfig.SERVER_URL + "api/tickets/random";
}