using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketData
{
    public int Id { get; set; }
    public string TypeIntersection { get; set; }
    public string Title { get; set; }
    public string Question { get; set; }
    public string CorrectAnswer { get; set; }
    public RoadUserData[] RoadUsersArr { get; set; }
    public SignData[] SignsArr { get; set; }
    public TrafficLightData[] TrafficLightsArr { get; set; }
}
