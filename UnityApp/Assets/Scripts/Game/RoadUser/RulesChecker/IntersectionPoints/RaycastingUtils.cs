using UnityEngine;

public static class RaycastingUtils
{
    public static RaycastHit2D LetOutRay(Vector3 startPostition, Vector3 nextPosition, float lengthRay = 7f)
    {
        RaycastHit2D hit = Physics2D.Raycast(startPostition, nextPosition, lengthRay);

        Debug.DrawLine(startPostition,
                       startPostition + nextPosition * lengthRay,
                       Color.red, // Цвет линии
                       2f); // Длительность отображения линии (в секундах)
        return hit;
    }

    public static GameObject CheckRoadUser(RaycastHit2D hit)
    {
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Car"))
        {
            Debug.Log("Обнаружен транспорт (машина): " + hit.collider.gameObject.name);
            return hit.collider.gameObject;
        }
        else
        {
            Debug.Log("Транспорт не найден или это не машина");
        }

        return null;
    }
}