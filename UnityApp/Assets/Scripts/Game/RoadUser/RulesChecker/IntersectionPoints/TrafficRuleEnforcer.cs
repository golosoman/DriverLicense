using System;
using UnityEngine;

public abstract class TrafficRuleEnforcer : MonoBehaviour
{
    public RoadManager RoadUserManager { get; set; }
    public bool hasObstacle = false;


    private void Start()
    {
        RoadUserManager = FindObjectOfType<RoadManager>();
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == TagObjectNamesTypes.CAR)
        {
            CarMovement userMovement = other.gameObject.GetComponent<CarMovement>();
            Vector3 position = gameObject.transform.position;

            // if (RoadUserManager.TrafficLightDatas.Length > 0)
            // {
            //     CheckRuleForRegulatedIntersection(other.gameObject, userMovement, position);
            // }
            if (RoadUserManager.SignDatas.Length > 0)
            {
                CheckRuleForUnregulatedIntersection(other.gameObject, userMovement, position);
            }
            // if (true)
            // {
            //     CheckRuleForEquivalentIntersection(other.gameObject, userMovement, position);
            // }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == TagObjectNamesTypes.CAR)
        {
            CarMovement userMovement = other.gameObject.GetComponent<CarMovement>();
            Vector3 position = gameObject.transform.position;

            // if (RoadUserManager.TrafficLightDatas.Length > 0)
            // {
            //     ReCheckRuleForRegulatedIntersection(position);
            // }
            if (RoadUserManager.SignDatas.Length > 0)
            {
                ReCheckRuleForUnregulatedIntersection(other.gameObject, userMovement, position);
            }
            // if (true)
            // {
            //     ReCheckRuleForEquivalentIntersection(position);
            // }
        }
    }

    protected void ReCheckRuleForRegulatedIntersection(Vector3 pointPosition)
    {
        // Реализация для повторной проверки регулируемого перекрестка
    }

    protected void ReCheckRuleForUnregulatedIntersection(GameObject car, CarMovement userMovement, Vector3 pointPosition)
    {
        hasObstacle = !ReCheckPriority(car, userMovement, pointPosition);
        if (userMovement.HasTurned())
        {
            userMovement.HasPriority = !userMovement.HasPriority;
            Debug.Log("Приоритет был изменен!");
        }
        if (hasObstacle)
        {
            Debug.Log("Машина не пропустила помеху!");
            RoadUserManager.ViolationRules = true;
        }
    }

    protected void ReCheckRuleForEquivalentIntersection(Vector3 pointPosition)
    {
        hasObstacle = ReCheckObstacleOnRight(pointPosition);
        if (hasObstacle)
        {
            Debug.Log("Машина не пропустила помеху!");
            RoadUserManager.ViolationRules = true;
        }
    }

    protected void CheckRuleForEquivalentIntersection(GameObject gameObject, CarMovement userMovement, Vector3 pointPosition)
    {
        hasObstacle = CheckObstacleOnRight(gameObject, userMovement, pointPosition);

        if (hasObstacle)
        {
            userMovement.StopMovement();
            ShowUserDialog();
        }
    }

    public void CheckRuleForUnregulatedIntersection(GameObject gameObject, CarMovement userMovement, Vector3 pointPosition)
    {
        Debug.Log("Проверяем ПДД на нерегулируемом перекрестке!");
        hasObstacle = !CheckPriority(gameObject, userMovement, pointPosition);
        if (hasObstacle)
        {
            userMovement.StopMovement();
            ShowUserDialog();
        }
    }

    public void CheckRuleForRegulatedIntersection(GameObject gameObject, RoadUserMovement userMovement, Vector3 pointPosition)
    {
        // Реализация для регулируемого перекрестка
    }

    protected bool CheckPriorityInternal(GameObject gameObject, CarMovement userMovement, Vector3 rayPosition, string sideDirection)
    {
        Vector3 direction = RoadUserManager.RoadUserSpawnPoints[sideDirection].transform.position;
        RaycastHit2D firstRay = RaycastingUtils.LetOutRay(rayPosition, (direction - rayPosition).normalized);
        RaycastHit2D secondRay = RaycastingUtils.LetOutRay(rayPosition, Quaternion.Euler(0f, 0f, 6f) * (direction - rayPosition).normalized);
        GameObject otherCar = RaycastingUtils.CheckRoadUser(firstRay) != null ? firstRay.collider.gameObject : RaycastingUtils.CheckRoadUser(secondRay) != null ? secondRay.collider.gameObject : null;
        if (otherCar != null)
        {
            CarMovement otherCarMovement = otherCar.GetComponent<CarMovement>();
            if (userMovement.HasPriority && !otherCarMovement.HasPriority)
            {
                return true;
            }
            if (userMovement.HasPriority && otherCarMovement.HasPriority && !TrafficRuleChecker.CheckIntersectionWithAnotherRoadUser(gameObject, otherCar, userMovement, otherCarMovement))
            {
                return true;
            }
            if (!userMovement.HasPriority && !otherCarMovement.HasPriority && !TrafficRuleChecker.CheckIntersectionWithAnotherRoadUser(gameObject, otherCar, userMovement, otherCarMovement))
            {
                return true;
            }
            return false;
        }
        else
        {
            return true;
        }
    }

    protected bool ReCheckPriorityInternal(GameObject gameObject, CarMovement userMovement, Vector3 rayPosition, Vector2 rightDirection, Vector2 downDirection)
    {
        GameObject rightCar = RaycastingUtils.CheckRoadUser(RaycastingUtils.LetOutRay(rayPosition, rightDirection));
        GameObject downCar = RaycastingUtils.CheckRoadUser(RaycastingUtils.LetOutRay(rayPosition, downDirection, 12));

        if (rightCar != null)
        {
            CarMovement rightCarMovement = rightCar.GetComponent<CarMovement>();
            if (userMovement.HasPriority && !rightCarMovement.HasPriority)
            {
                return true;
            }
            return false;
        }

        if (downCar != null)
        {
            CarMovement downCarMovement = downCar.GetComponent<CarMovement>();
            if (userMovement.HasPriority && !downCarMovement.HasPriority)
            {
                return true;
            }
            if (!userMovement.HasPriority && !downCarMovement.HasPriority)
            {
                return true;
            }
            return false;
        }

        return true;
    }

    public abstract bool CheckObstacleOnRight(GameObject gameObject, RoadUserMovement userMovement, Vector3 rayPosition);
    public abstract bool ReCheckObstacleOnRight(Vector3 rayPosition);
    public abstract bool CheckPriority(GameObject gameObject, CarMovement userMovement, Vector3 rayPosition);
    public abstract bool ReCheckPriority(GameObject gameObject, CarMovement userMovement, Vector3 rayPosition);


    public virtual void ShowUserDialog()
    {
        // Заменить это на диалоговое окно с опциями
        Debug.Log("Обнаружено препятствие справа. Пропустить или не пропустить?");
    }
}