using UnityEngine;

public static class RaycastingUtils
{
    public static RaycastHit2D LetOutRay(Vector2 startPostition, Vector2 nextPosition, float lengthRay = 7f)
    {
        int layerMask = LayerMask.GetMask("RaycastLayer"); // или любой другой слой
        RaycastHit2D hit = Physics2D.Raycast(startPostition, nextPosition, lengthRay, layerMask);

        Debug.DrawLine(startPostition,
                       startPostition + nextPosition * lengthRay,
                       Color.red, // Цвет линии
                       2f); // Длительность отображения линии (в секундах)
        return hit;
    }

    public static GameObject CheckRoadUser(RaycastHit2D hit)
    {
        Debug.Log(hit.collider);
        if (hit.collider != null && hit.collider.gameObject.CompareTag(TagObjectNamesTypes.CAR))
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