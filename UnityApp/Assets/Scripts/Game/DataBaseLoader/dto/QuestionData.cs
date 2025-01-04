using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionData
{
    public int Id { get; set; }
    public string Question { get; set; }
    public string Explanation { get; set; }
    public string IntersectionType { get; set; }
    public TrafficLightData[] TrafficLights { get; set; }
    public TrafficParticipantData[] TrafficParticipants { get; set; }
    public SignData[] Signs { get; set; }
    public int CategoryId { get; set; }
}
