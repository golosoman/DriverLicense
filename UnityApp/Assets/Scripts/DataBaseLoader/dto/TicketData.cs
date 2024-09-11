using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketData
{
    public int id;
    public string typeIntersection;
    public string title;
    public string question;
    public string correctAnswer;
    public RoadUserData[] roadUsersArr;
    public SignData[] signsArr;
    public TrafficLightData[] trafficLightsArr;
}
