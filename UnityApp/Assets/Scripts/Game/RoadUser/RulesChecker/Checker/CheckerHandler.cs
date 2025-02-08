
using System;
using UnityEngine;

public abstract class CheckerHandler : MonoBehaviour
{
    public RoadManager RoadUserManager { get; private set; }

    public delegate void HasObstacle(string message);
    public static event HasObstacle OnHasObstacle; // Событие экземпляра

    protected virtual void Start()
    {
        BaseInit();

    }

    private void HasObstacleHandler(GameObject trafficParticipant)
    {
        Debug.Log("!!!!!!!!!Сталкновение");
        OnHasObstacle?.Invoke("Аварийная ситуация!");
    }

    public void BaseInit()
    {
        RoadUserManager = FindObjectOfType<RoadManager>();
        CarPoint1Checker.OnHasObstacle += HasObstacleHandler;
        CarPoint2Checker.OnHasObstacle += HasObstacleHandler;
        CarPoint3Checker.OnHasObstacle += HasObstacleHandler;
        CarPoint4Checker.OnHasObstacle += HasObstacleHandler;

        CarPoint1Checker.OnStopCar += StopCarHandler;
        CarPoint2Checker.OnStopCar += StopCarHandler;
        CarPoint3Checker.OnStopCar += StopCarHandler;
        CarPoint4Checker.OnStopCar += StopCarHandler;
    }

    private void StopCarHandler(GameObject trafficParticipant)
    {
        trafficParticipant.GetComponent<CarMovement>().StopMovement();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
            CheckTrafficRules(other.gameObject);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
            ReCheckTrafficRules(other.gameObject);
        }
    }

    protected void CheckTrafficRules(GameObject trafficParticipant)
    {
        CheckRuleForEquivalentIntersection(trafficParticipant);
        // CheckRuleForUnregulatedIntersection(trafficParticipant);
        // if (RoadUserManager.TrafficLightDatas.Length > 0)
        // {
        //     CheckRuleForRegulatedIntersection(trafficParticipant);
        // }
        // else if (RoadUserManager.SignDatas.Length > 0)
        // {
        //     CheckRuleForUnregulatedIntersection();
        // }
        // else
        // {
        //     CheckRuleForEquivalentIntersection(trafficParticipant);
        // }
    }

    protected void ReCheckTrafficRules(GameObject trafficParticipant)
    {
        ReCheckRuleForEquivalentIntersection(trafficParticipant);
        // ReCheckRuleForUnregulatedIntersection(trafficParticipant);
        // if (RoadUserManager.TrafficLightDatas.Length > 0)
        // {
        //     ReCheckRuleForRegulatedIntersection(trafficParticipant);
        // }
        // else if (RoadUserManager.SignDatas.Length > 0)
        // {
        //     ReCheckRuleForUnregulatedIntersection();
        // }
        // else
        // {
        //     ReCheckRuleForEquivalentIntersection(trafficParticipant);
        // }
    }

    public abstract void CheckObstacleOnRight(GameObject trafficParticipant);
    public abstract void ReCheckObstacleOnRight(GameObject trafficParticipant);
    public abstract void CheckSignPriority(GameObject trafficParticipant);
    public abstract void ReCheckSignPriority(GameObject trafficParticipant);

    protected void CheckRuleForRegulatedIntersection(GameObject trafficParticipant)
    {
    }

    protected void ReCheckRuleForRegulatedIntersection(GameObject trafficParticipant)
    {

    }

    protected void CheckRuleForUnregulatedIntersection(GameObject trafficParticipant)
    {
        CheckSignPriority(trafficParticipant);
    }

    protected void ReCheckRuleForUnregulatedIntersection(GameObject trafficParticipant)
    {
        ReCheckSignPriority(trafficParticipant);
    }

    protected void CheckRuleForEquivalentIntersection(GameObject trafficParticipant)
    {
        CheckObstacleOnRight(trafficParticipant);
    }

    protected void ReCheckRuleForEquivalentIntersection(GameObject trafficParticipant)
    {
        ReCheckObstacleOnRight(trafficParticipant);
    }

}