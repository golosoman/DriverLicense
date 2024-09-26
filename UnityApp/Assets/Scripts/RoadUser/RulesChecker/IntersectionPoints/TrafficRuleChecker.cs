using UnityEngine;

public static class TrafficRuleChecker
{
    public static bool CheckIntersectionWithAnotherRoadUser(GameObject roadUser, GameObject otherRoadUser, RoadUserMovement roadUserMovement, RoadUserMovement otherRoadUserMovement)
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

    public static bool CheckIntersectionVectorType(Vector3 P1_0, Vector3 P1_1, Vector3 P2_0, Vector3 P2_1)
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

    public static bool CheckRoadUserOnRight(GameObject gameObject, RoadUserMovement userMovement, RaycastHit2D hit)
    {
        if (RaycastingUtils.CheckRoadUser(hit) is not null)
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

    public static bool CheckObstacleWithRays(GameObject gameObject, RoadUserMovement userMovement, Vector3 rayPosition, Vector3 direction)
    {
        RaycastHit2D firstRay = RaycastingUtils.LetOutRay(rayPosition, (direction - rayPosition).normalized);
        RaycastHit2D secondRay = RaycastingUtils.LetOutRay(rayPosition, Quaternion.Euler(0f, 0f, 6f) * (direction - rayPosition).normalized);
        return CheckRoadUserOnRight(gameObject, userMovement, firstRay) ? true : CheckRoadUserOnRight(gameObject, userMovement, secondRay);
    }
}