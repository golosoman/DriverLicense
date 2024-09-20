using System;
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
         if (other.gameObject.tag == TagObjectNamesTypes.CAR){
            RoadUserMovement userMovement = other.gameObject.GetComponent<RoadUserMovement>();
            CheckRuleForEquivalentIntersection(other.gameObject, userMovement);
         }
        // if (other.gameObject.tag == TagObjectNamesTypes.CAR)
        // {
        //     RoadUserMovement userMovement = other.gameObject.GetComponent<RoadUserMovement>();

        //     if(RoadUserManager.TrafficLightDatas.Length > 0) {
        //         CheckRuleForRegulatedIntersection(other.gameObject, userMovement);
        //     }
        //     else if (RoadUserManager.SignDatas.Length > 0) {
        //         CheckRuleForUnregulatedIntersection(other.gameObject, userMovement);
        //     }

        //     if(true) {
        //         CheckRuleForEquivalentIntersection(other.gameObject, userMovement);
        //     }
        // }
    }



    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == TagObjectNamesTypes.CAR)
        {
            hasObstacleOnRight = CheckObstacleOnRight(gameObject.transform.position);
            // Проверяем, была ли помеха справа
            if (hasObstacleOnRight)
            {
                Debug.Log("Машина не пропустила помеху!");
                RoadUserManager.ViolationRules = true;
            }
        }
    }

    public void CheckRuleForEquivalentIntersection(GameObject gameObject, RoadUserMovement userMovement){

        // hasObstacleOnRight = CheckRoadUserOnRight(LetOutRay(gameObject.transform.position, Quaternion.Euler(0f, 0f, 270f) * Vector2.right, 20f));
        hasObstacleOnRight = CheckObstacleOnRight(gameObject.transform.position);

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

    protected static bool CheckIntersectionSecondType(Vector2 P1_0, Vector2 P1_1, Vector2 P2_0, Vector2 P2_1)
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

    public bool CheckRoadUserOnRight(RaycastHit2D hit){
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Car")) 
        {
            // Обнаружено препятствие, которое является машиной
            Debug.Log("Обнаружено препятствие (машина): " + hit.collider.gameObject.name);
            return true;
            // Выполните ваши действия здесь
        } 
        else 
        {
            // Препятствие не найдено или это не машина
            Debug.Log("Препятствие не найдено или это не машина");
            // Выполните ваши действия здесь
        }

        return false;
    }

    public RaycastHit2D LetOutRay(Vector3 startPostition, Vector3 nextPosition, float langthRay){
        RaycastHit2D hit = Physics2D.Raycast(startPostition, nextPosition, langthRay);

        Debug.DrawLine(startPostition, 
                       gameObject.transform.position + 
                       nextPosition * langthRay,
                       Color.red, // Цвет линии
                       2f); // Длительность отображения линии (в секундах)
        return hit;
    }

    public abstract bool CheckObstacleOnRight(Vector3 startPosition);

    public virtual void ShowUserDialog()
    {
        // Заменить это на диалоговое окно с опциями
        Debug.Log("Обнаружено препятствие справа. Пропустить или не пропустить?");
    }
}