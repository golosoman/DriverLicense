using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class TrafficRuleEnforcer : MonoBehaviour
{
    public RoadUserManager RoadUserManager { get; set; }
    // public Dictionary<string, Dictionary<RoadUserData, GameObject>> RoadUsers { get; set; }

    private void Start()
    {
        RoadUserManager = FindObjectOfType<RoadUserManager>();
        // RoadUsers = RoadUserManager.RoadUsers;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == TagObjectNamesTypes.CAR)
        {
            RoadUserMovement userMovement = other.gameObject.GetComponent<RoadUserMovement>();

            bool hasObstacleOnRight = CheckObstacleOnRight(other.gameObject, userMovement);

            if (hasObstacleOnRight)
            {
                userMovement.StopMovement();
                ShowUserDialog();
            }
        }
    }

    protected bool CheckObstaclesConditions(string firstSideDirection, string secondSideDirection, 
        string thirdSideDirection, RoadUserData roadUserData) {
        if (roadUserData.SidePosition == firstSideDirection && (roadUserData.MovementDirection == DirectionMovementTypes.FORWARD || 
                    roadUserData.MovementDirection == DirectionMovementTypes.LEFT || 
                    roadUserData.MovementDirection == DirectionMovementTypes.BACKWARD))
                    { return true; }

        if (roadUserData.SidePosition == secondSideDirection && (roadUserData.MovementDirection == DirectionMovementTypes.LEFT || 
                    roadUserData.MovementDirection == DirectionMovementTypes.BACKWARD))
                    { return true; }
                
        if (roadUserData.SidePosition == thirdSideDirection && roadUserData.MovementDirection == DirectionMovementTypes.BACKWARD)
                    { return true; }
        return false;
    }

    protected static bool HasIntersection(Vector3 startPos1, Vector3 endPos1, Vector3 startPos2, Vector3 endPos2)
    {
        float m1 = (endPos1.y - startPos1.y) / (endPos1.x - startPos1.x);
        float b1 = startPos1.y - m1 * startPos1.x;

        float m2 = (endPos2.y - startPos2.y) / (endPos2.x - startPos2.x);
        float b2 = startPos2.y - m2 * startPos2.x;

        if (m1 == m2)
        {
            return false; // Параллельные линии
        }

        float x = (b2 - b1) / (m1 - m2);

        // Проверяем, находится ли точка пересечения в пределах отрезков
        if (x >= Mathf.Min(startPos1.x, endPos1.x) && x <= Mathf.Max(startPos1.x, endPos1.x) &&
            x >= Mathf.Min(startPos2.x, endPos2.x) && x <= Mathf.Max(startPos2.x, endPos2.x))
        {
            return true; // Точка пересечения в пределах отрезков
        }

        return false; // Точки пересечения нет
    }

    public abstract bool CheckObstacleOnRight(GameObject roadUserDataObject, RoadUserMovement roadUserMovement);

    public virtual void ShowUserDialog()
    {
        // Заменить это на диалоговое окно с опциями
        Debug.Log("Обнаружено препятствие справа. Пропустить или не пропустить?");
    }
}
