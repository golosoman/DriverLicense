using UnityEngine;

public class IntersectionManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] northForwardRoute;
    [SerializeField]
    private Transform[] northLeftRoute;
    [SerializeField]
    private Transform[] northRightRoute;
    [SerializeField]
    private Transform[] nortBackwardRoute;

    [SerializeField]
    private Transform[] southForwardRoute;
    [SerializeField]
    private Transform[] southLeftRoute;
    [SerializeField]
    private Transform[] southRightRoute;
    [SerializeField]
    private Transform[] southBackwardRoute;

    [SerializeField]
    private Transform[] westForwardRoute;
    [SerializeField]
    private Transform[] westLeftRoute;
    [SerializeField]
    private Transform[] westRightRoute;
    [SerializeField]
    private Transform[] westBackwardRoute;

    [SerializeField]
    private Transform[] eastForwardRoute;
    [SerializeField]
    private Transform[] eastLeftRoute;
    [SerializeField]
    private Transform[] eastRightRoute;
    [SerializeField]
    private Transform[] eastBackwardRoute;

    public enum Direction { North, South, West, East }

    // Функция для получения маршрута на основе направления автомобиля
    public Transform[] GetRoute(Direction direction, string movement)
    {
        switch (direction)
        {
            case Direction.North:
                if (movement == "forward")
                    return northForwardRoute;
                else if (movement == "left")
                    return northLeftRoute;
                else if (movement == "right")
                    return northRightRoute;
                else if (movement == "backward")
                    return nortBackwardRoute;
                break;
            case Direction.South:
                if (movement == "forward")
                    return southForwardRoute;
                else if (movement == "left")
                    return southLeftRoute;
                else if (movement == "right")
                    return southRightRoute;
                else if (movement == "backward")
                    return southBackwardRoute;
                break;
            case Direction.West:
                if (movement == "forward")
                    return westForwardRoute;
                else if (movement == "left")
                    return westLeftRoute;
                else if (movement == "right")
                    return westRightRoute;
                else if (movement == "backward")
                    return westBackwardRoute;
                break;
            case Direction.East:
                if (movement == "forward")
                    return eastForwardRoute;
                else if (movement == "left")
                    return eastLeftRoute;
                else if (movement == "right")
                    return eastRightRoute;
                else if (movement == "backward")
                    return eastBackwardRoute;
                break;
        }
        return null;
    }
}
