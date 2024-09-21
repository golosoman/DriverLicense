using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class TrafficRuleEnforcer : MonoBehaviour
{
    public RoadManager RoadUserManager { get; set; }
    public bool hasObstacleOnRight = false;

    private void Start()
    {
        RoadUserManager = FindObjectOfType<RoadManager>();
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == TagObjectNamesTypes.CAR)
        {
            RoadUserMovement userMovement = other.gameObject.GetComponent<RoadUserMovement>();

            if (RoadUserManager.TrafficLightDatas.Length > 0) 
            {
                CheckRuleForRegulatedIntersection(other.gameObject, userMovement);
            }
            else if (RoadUserManager.SignDatas.Length > 0) 
            {
                CheckRuleForUnregulatedIntersection(other.gameObject, userMovement);
            }

            CheckRuleForEquivalentIntersection(other.gameObject, userMovement);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == TagObjectNamesTypes.CAR)
        {
            hasObstacleOnRight = ReCheckObstacleOnRight(gameObject.transform.position);
            if (hasObstacleOnRight)
            {
                Debug.Log("Машина не пропустила помеху!");
                RoadUserManager.ViolationRules = true;
            }
        }
    }

    protected void CheckRuleForEquivalentIntersection(GameObject gameObject, RoadUserMovement userMovement)
    {
        hasObstacleOnRight = CheckObstacleOnRight(gameObject, userMovement, this.gameObject.transform.position);

        if (hasObstacleOnRight)
        {
            userMovement.StopMovement();
            ShowUserDialog();
        }
    }

    protected bool CheckRoadUser(RaycastHit2D hit)
    {
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Car")) 
        {
            Debug.Log("Обнаружен транспорт (машина): " + hit.collider.gameObject.name);
            return true;
        } 
        else 
        {
            Debug.Log("Транспорт не найден или это не машина");
        }

        return false;
    }

    protected bool CheckIntersectionWithAnotherRoadUser(GameObject roadUser, GameObject otherRoadUser, RoadUserMovement roadUserMovement, RoadUserMovement otherRoadUserMovement)
    {
        if (otherRoadUserMovement.CurrentPoint + 1 < otherRoadUserMovement.Route.Length && roadUserMovement.CurrentPoint + 1 < roadUserMovement.Route.Length)
        {
            Vector3 roadUserPositionStart = roadUser.transform.position;
            Vector3 roadUserPositionEnd = roadUserMovement.Route[roadUserMovement.CurrentPoint + 1].transform.position;
            Vector3 otherRoadUserPositionStart = otherRoadUser.transform.position;
            Vector3 otherRoadUserPositionEnd = otherRoadUserMovement.Route[otherRoadUserMovement.CurrentPoint + 1].transform.position;
            
            if (CheckIntersectionVectorType(roadUserPositionStart, roadUserPositionEnd, otherRoadUserPositionStart, otherRoadUserPositionEnd))
            {
                return true;
            }
        }
        return false;
    }

    protected bool CheckRoadUserOnRight(GameObject gameObject, RoadUserMovement userMovement, RaycastHit2D hit)
    {
        if (CheckRoadUser(hit)) 
        {
            if (CheckIntersectionWithAnotherRoadUser(gameObject, hit.collider.gameObject, userMovement, 
                hit.collider.gameObject.GetComponent<RoadUserMovement>())) 
            {
                Debug.Log("Впереди есть машина препятствие!");
                return true;
            }
        } 
        else 
        {
            Debug.Log("Препятствие не найдено или это не машина");
        }

        return false;
    }

    protected RaycastHit2D LetOutRay(Vector3 startPostition, Vector3 nextPosition, float lengthRay = 7f)
    {
        RaycastHit2D hit = Physics2D.Raycast(startPostition, nextPosition, lengthRay);

        Debug.DrawLine(startPostition, 
                       gameObject.transform.position + 
                       nextPosition * lengthRay,
                       Color.red, // Цвет линии
                       2f); // Длительность отображения линии (в секундах)
        return hit;
    }

    protected bool CheckObstacleWithRays(GameObject gameObject, RoadUserMovement userMovement, Vector3 rayPosition, Vector3 direction)
    {
        RaycastHit2D firstRay = LetOutRay(rayPosition, (direction-rayPosition).normalized);
        RaycastHit2D secondRay = LetOutRay(rayPosition, Quaternion.Euler(0f, 0f, 6f) * (direction-rayPosition).normalized);
        return CheckRoadUserOnRight(gameObject, userMovement, firstRay) ? true : CheckRoadUserOnRight(gameObject, userMovement, secondRay);
    }

    public void CheckRuleForUnregulatedIntersection(GameObject gameObject, RoadUserMovement userMovement)
    {
        // Реализация для нерегулируемого перекрестка
    }

    public void CheckRuleForRegulatedIntersection(GameObject gameObject, RoadUserMovement userMovement)
    {
        // Реализация для регулируемого перекрестка
    }

    public abstract bool CheckObstacleOnRight(GameObject gameObject, RoadUserMovement userMovement, Vector3 rayPosition);
    public abstract bool ReCheckObstacleOnRight(Vector3 rayPosition);

    protected static bool CheckIntersectionVectorType(Vector3 P1_0, Vector3 P1_1, Vector3 P2_0, Vector3 P2_1)
    {
        Vector3 d1 = P1_1 - P1_0; // Вектор направления первого участника
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
        // Проверка, находятся ли точки пересечения на отрезках траекторий машин
        if (t1 >= 0f && t1 <= 1.1f && t2 >= 0f && t2 <= 1.1f)
        {
            return true;
        }
        
        return false;
    }

    public virtual void ShowUserDialog()
    {
        // Заменить это на диалоговое окно с опциями
        Debug.Log("Обнаружено препятствие справа. Пропустить или не пропустить?");
    }
}