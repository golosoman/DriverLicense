using UnityEngine;

public static class TrafficRuleChecker
{
    public static float offset = 1.5f;
    public static float byPointOffset = 4f;

    public static bool CheckIntersectionWithAnotherRoadUser(GameObject roadUser, GameObject otherRoadUser, RoadUserMovement roadUserMovement, RoadUserMovement otherRoadUserMovement)
    {
        // Получаем индексы текущих и следующих точек
        int roadUserNextPointIndex = roadUserMovement.CurrentPoint + 1;
        int otherRoadUserNextPointIndex = otherRoadUserMovement.CurrentPoint + 1;

        // Проверяем, не превышает ли индекс длину маршрута, и корректируем его
        if (roadUserNextPointIndex >= roadUserMovement.Route.Length)
        {
            roadUserNextPointIndex = roadUserMovement.Route.Length - 1; // Берем последнюю точку
        }

        if (otherRoadUserNextPointIndex >= otherRoadUserMovement.Route.Length)
        {
            otherRoadUserNextPointIndex = otherRoadUserMovement.Route.Length - 1; // Берем последнюю точку
        }

        // Получаем позиции начальных и конечных точек
        Vector2 roadUserPositionStart = roadUser.transform.position;
        Vector2 roadUserPositionEnd = roadUserMovement.Route[roadUserNextPointIndex].transform.position;
        Vector2 otherRoadUserPositionStart = otherRoadUser.transform.position;
        Vector2 otherRoadUserPositionEnd = otherRoadUserMovement.Route[otherRoadUserNextPointIndex].transform.position;

        // Добавляем отступ к начальным и конечным точкам
        Vector2 roadUserDirection = (roadUserPositionEnd - roadUserPositionStart).normalized;
        Vector2 otherRoadUserDirection = (otherRoadUserPositionEnd - otherRoadUserPositionStart).normalized;

        // Увеличиваем длину векторов на заданный отступ
        Vector2 roadUserPositionStartOffset = roadUserPositionStart - roadUserDirection * offset;
        Vector2 roadUserPositionEndOffset = roadUserPositionEnd + roadUserDirection * offset;
        Vector2 otherRoadUserPositionStartOffset = otherRoadUserPositionStart - otherRoadUserDirection * offset;
        Vector2 otherRoadUserPositionEndOffset = otherRoadUserPositionEnd + otherRoadUserDirection * offset;

        // Проверяем пересечение
        if (CheckIntersectionVectorType(roadUserPositionStartOffset, roadUserPositionEndOffset, otherRoadUserPositionStartOffset, otherRoadUserPositionEndOffset))
        {
            return true;
        }

        return false;
    }

    public static bool ByPointChecker(CarMovement firstRoadUser, CarMovement secondRoadUser)
    {
        Vector2 startPositionFirst = firstRoadUser.Route[firstRoadUser.CurrentPoint].transform.position;
        Vector2 endPositionFirst = firstRoadUser.Route[firstRoadUser.CurrentPoint + 1].transform.position;
        Vector2 startPositionSecond = secondRoadUser.Route[secondRoadUser.CurrentPoint].transform.position;
        Vector2 endPositionSecond = secondRoadUser.Route[secondRoadUser.CurrentPoint + 1].transform.position;

        // Добавляем отступ к начальным и конечным точкам
        Vector2 roadUserDirection = (endPositionFirst - startPositionFirst).normalized;
        Vector2 otherRoadUserDirection = (endPositionSecond - startPositionSecond).normalized;

        // Увеличиваем длину векторов на заданный отступ
        Vector2 roadUserPositionStartOffset = startPositionFirst - roadUserDirection;
        Vector2 roadUserPositionEndOffset = endPositionFirst + roadUserDirection;
        Vector2 otherRoadUserPositionStartOffset = startPositionSecond - otherRoadUserDirection * byPointOffset;
        Vector2 otherRoadUserPositionEndOffset = endPositionSecond + otherRoadUserDirection * byPointOffset;

        return CheckIntersectionVectorType(roadUserPositionStartOffset, roadUserPositionEndOffset, otherRoadUserPositionStartOffset, otherRoadUserPositionEndOffset);
    }

    public static bool CheckingIntersectionLastCoordinates(GameObject roadUser, GameObject otherRoadUser, CarMovement roadUserMovement, CarMovement otherRoadUserMovement)
    {

        // Получаем индексы текущих и следующих точек
        int roadUserNextPointIndex = roadUserMovement.CurrentPoint + 1;

        if (roadUserNextPointIndex >= roadUserMovement.Route.Length)
        {
            roadUserNextPointIndex = roadUserMovement.Route.Length - 1; // Берем последнюю точку
        }



        // Получаем текущие и будущие точки
        Vector2 roadUserCurrentPosition = roadUser.transform.position; // Текущая позиция
        Vector2 roadUserFuturePosition = roadUserMovement.Route[roadUserNextPointIndex].transform.position; // Будущая позиция

        Vector2 otherRoadUserCurrentPosition = otherRoadUserMovement.Route[otherRoadUserMovement.Route.Length - 1].transform.position; // Текущая позиция
        Vector2 otherRoadUserFuturePosition = otherRoadUserMovement.Route[otherRoadUserMovement.Route.Length - 2].transform.position; // Будущая позиция

        // Проверяем пересечение траекторий
        bool isIntersecting = TrafficRuleChecker.CheckIntersectionVectorType(
            roadUserCurrentPosition,
            roadUserFuturePosition,
            otherRoadUserCurrentPosition,
            otherRoadUserFuturePosition
        );
        return isIntersecting;
    }


    // Метод для проверки параллельности двух векторов
    public static bool AreVectorsParallel(Vector2 vectorA, Vector2 vectorB)
    {
        // Вычисляем определитель (векторное произведение в 2D)
        float determinant = vectorA.x * vectorB.y - vectorA.y * vectorB.x;

        // Если детерминант равен нулю, векторы параллельны
        return Mathf.Abs(determinant) < Mathf.Epsilon;
    }


    public static bool CheckIntersectionVectorType(Vector2 P1_0, Vector2 P1_1, Vector2 P2_0, Vector2 P2_1)
    {
        Vector2 d1 = P1_1 - P1_0; // Вектор направления первого участника
        Vector2 d2 = P2_1 - P2_0; // Вектор направления второго участника

        float det = d1.x * d2.y - d1.y * d2.x; // Определитель системы уравнений

        // Визуализация векторов
        Debug.DrawLine(P1_0, P1_1, Color.red); // Первый отрезок красным
        Debug.DrawLine(P2_0, P2_1, Color.blue); // Второй отрезок синим

        if (Mathf.Abs(det) < Mathf.Epsilon) // Использование epsilon для проверки на ноль
        {
            // Векторы параллельны, проверяем на коллинеарность
            if (IsCollinear(P1_0, P1_1, P2_0))
            {
                // Проверка на наложение отрезков
                return IsOverlapping(P1_0, P1_1, P2_0, P2_1);
            }
            return false; // Параллельные и не коллинеарные
        }

        Vector2 diff = P2_0 - P1_0;
        float t1 = (diff.x * d2.y - diff.y * d2.x) / det;
        float t2 = (diff.x * d1.y - diff.y * d1.x) / det;

        // Проверка, находятся ли точки пересечения на отрезках траекторий машин
        if (t1 >= 0f && t1 <= 1f && t2 >= 0f && t2 <= 1f)
        {
            // Визуализируем точку пересечения
            Vector2 intersectionPoint = P1_0 + t1 * d1;
            Debug.DrawLine(intersectionPoint - Vector2.one * 0.1f, intersectionPoint + Vector2.one * 0.1f, Color.green);
            return true; // Пересечение найдено
        }

        return false;
    }

    private static bool IsCollinear(Vector2 A, Vector2 B, Vector2 C)
    {
        // Проверка коллинеарности с использованием площади треугольника
        return Mathf.Abs((B.y - A.y) * (C.x - A.x) - (B.x - A.x) * (C.y - A.y)) < Mathf.Epsilon;
    }

    private static bool IsOverlapping(Vector2 A1, Vector2 A2, Vector2 B1, Vector2 B2)
    {
        // Проверка наложения отрезков
        return (Mathf.Max(A1.x, A2.x) >= Mathf.Min(B1.x, B2.x) &&
                Mathf.Min(A1.x, A2.x) <= Mathf.Max(B1.x, B2.x) &&
                Mathf.Max(A1.y, A2.y) >= Mathf.Min(B1.y, B2.y) &&
                Mathf.Min(A1.y, A2.y) <= Mathf.Max(B1.y, B2.y));
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

    public static bool CheckObstacleWithRays(GameObject gameObject, RoadUserMovement userMovement, Vector2 rayPosition, Vector2 direction)
    {
        RaycastHit2D firstRay = RaycastingUtils.LetOutRay(rayPosition, (direction - rayPosition).normalized);
        RaycastHit2D secondRay = RaycastingUtils.LetOutRay(rayPosition, Quaternion.Euler(0f, 0f, 6f) * (direction - rayPosition).normalized);
        return CheckRoadUserOnRight(gameObject, userMovement, firstRay) ? true : CheckRoadUserOnRight(gameObject, userMovement, secondRay);
    }
}
