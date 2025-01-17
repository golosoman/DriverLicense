using System.Collections.Generic;

[System.Serializable]
public class Ticket
{
    public int id;
    public string name;
    public List<Question> questions;
}

[System.Serializable]
public class TicketList
{
    public List<Ticket> tickets;
}