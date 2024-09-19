using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class TrafficRuleEnforcer : MonoBehaviour
{
    public RoadManager RoadUserManager { get; set; }
    public bool hasObstacleOnRight = false;
    // public Dictionary<string, Dictionary<RoadUserData, GameObject>> RoadUsers { get; set; }

    private void Start()
    {
        RoadUserManager = FindObjectOfType<RoadManager>();
        // RoadUsers = RoadUserManager.RoadUsers;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == TagObjectNamesTypes.CAR)
        {
            RoadUserMovement userMovement = other.gameObject.GetComponent<RoadUserMovement>();

            if(RoadUserManager.TrafficLightDatas.Length > 0) {
                CheckRuleForRegulatedIntersection(other.gameObject, userMovement);
            }
            else if (RoadUserManager.SignDatas.Length > 0) {
                CheckRuleForUnregulatedIntersection(other.gameObject, userMovement);
            }

            if(true) {
                CheckRuleForEquivalentIntersection(other.gameObject, userMovement);
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == TagObjectNamesTypes.CAR)
        {
            // Проверяем, была ли помеха справа
            if (hasObstacleOnRight)
            {
                Debug.Log("Машина не пропустила помеху!");
                RoadUserManager.ViolationRules = true;
            }
            // Сбрасываем флаг
            hasObstacleOnRight = false;
        }
    }

    public void CheckRuleForEquivalentIntersection(GameObject gameObject, RoadUserMovement userMovement){
        bool hasObstacleOnRight = CheckObstacleOnRight(gameObject, userMovement);

        if (hasObstacleOnRight)
        {
            userMovement.StopMovement();
            ShowUserDialog();
        }
    }

    public void CheckRuleForUnregulatedIntersection(GameObject gameObject, RoadUserMovement userMovement){
        
    }

    public void CheckRuleForRegulatedIntersection(GameObject gameObject, RoadUserMovement userMovement){

    }

    protected bool CheckIntersectionFirstType(string firstSideDirection, string secondSideDirection, 
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

    protected bool CheckIntersectionSecondType(Vector2 P1_0, Vector2 P1_1, Vector2 P2_0, Vector2 P2_1)
    {
        Vector2 d1 = P1_1 - P1_0; // Вектор направления первого участника
        Vector2 d2 = P2_1 - P2_0; // Вектор направления второго участника

        float det = d1.x * d2.y - d1.y * d2.x; // Определитель системы уравнений

        if (Mathf.Approximately(det, 0f))
        {
            // Векторы параллельны, пересечения нет
            return false;
        }

        Vector2 diff = P2_0 - P1_0;
        float t1 = (diff.x * d2.y - diff.y * d2.x) / det;
        float t2 = (diff.x * d1.y - diff.y * d1.x) / det;
        // Debug.Log(t1 + " this  " + t2);
        // Проверка, находятся ли точки пересечения на отрезках траекторий машин
        if (t1 >= 0f && t1 <= 1.1f && t2 >= 0f && t2 <= 1.1f)
        {
            return true;
        }
        
        return false;
    }

    public abstract bool CheckObstacleOnRight(GameObject roadUserDataObject, RoadUserMovement roadUserMovement);

    public virtual void ShowUserDialog()
    {
        // Заменить это на диалоговое окно с опциями
        Debug.Log("Обнаружено препятствие справа. Пропустить или не пропустить?");
    }
}
