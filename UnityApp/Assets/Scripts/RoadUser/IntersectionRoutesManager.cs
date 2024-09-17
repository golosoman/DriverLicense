using UnityEngine;
using System.Collections.Generic;

public class IntersectionRoutesManager : MonoBehaviour
{
    // Используем словарь для хранения маршрутов для каждого направления и типа движения
    private Dictionary<(Direction, string), Transform[]> routes = new Dictionary<(Direction, string), Transform[]>();

    [SerializeField] private Transform[] northForwardRoute;
    [SerializeField] private Transform[] northLeftRoute;
    [SerializeField] private Transform[] northRightRoute;
    [SerializeField] private Transform[] northBackwardRoute;

    [SerializeField] private Transform[] southForwardRoute;
    [SerializeField] private Transform[] southLeftRoute;
    [SerializeField] private Transform[] southRightRoute;
    [SerializeField] private Transform[] southBackwardRoute;

    [SerializeField] private Transform[] westForwardRoute;
    [SerializeField] private Transform[] westLeftRoute;
    [SerializeField] private Transform[] westRightRoute;
    [SerializeField] private Transform[] westBackwardRoute;

    [SerializeField] private Transform[] eastForwardRoute;
    [SerializeField] private Transform[] eastLeftRoute;
    [SerializeField] private Transform[] eastRightRoute;
    [SerializeField] private Transform[] eastBackwardRoute;

    public enum Direction { NORTH, SOUTH, WEST, EAST }

    // Инициализация словаря в Awake, чтобы быть уверенным, что данные установлены до любых вызовов
    private void Awake()
    {
        // Заполняем словарь маршрутами
        routes[(Direction.NORTH, DirectionMovementTypes.FORWARD)] = northForwardRoute;
        routes[(Direction.NORTH, DirectionMovementTypes.LEFT)] = northLeftRoute;
        routes[(Direction.NORTH, DirectionMovementTypes.RIGHT)] = northRightRoute;
        routes[(Direction.NORTH, DirectionMovementTypes.BACKWARD)] = northBackwardRoute;

        routes[(Direction.SOUTH, DirectionMovementTypes.FORWARD)] = southForwardRoute;
        routes[(Direction.SOUTH, DirectionMovementTypes.LEFT)] = southLeftRoute;
        routes[(Direction.SOUTH, DirectionMovementTypes.RIGHT)] = southRightRoute;
        routes[(Direction.SOUTH, DirectionMovementTypes.BACKWARD)] = southBackwardRoute;

        routes[(Direction.WEST, DirectionMovementTypes.FORWARD)] = westForwardRoute;
        routes[(Direction.WEST, DirectionMovementTypes.LEFT)] = westLeftRoute;
        routes[(Direction.WEST, DirectionMovementTypes.RIGHT)] = westRightRoute;
        routes[(Direction.WEST, DirectionMovementTypes.BACKWARD)] = westBackwardRoute;

        routes[(Direction.EAST, DirectionMovementTypes.FORWARD)] = eastForwardRoute;
        routes[(Direction.EAST, DirectionMovementTypes.LEFT)] = eastLeftRoute;
        routes[(Direction.EAST, DirectionMovementTypes.RIGHT)] = eastRightRoute;
        routes[(Direction.EAST, DirectionMovementTypes.BACKWARD)] = eastBackwardRoute;
    }

    // Функция для получения маршрута на основе направления автомобиля
    public Transform[] GetRoute(Direction direction, string movement)
    {
        // Проверяем, существует ли такой маршрут в словаре
        if (routes.TryGetValue((direction, movement), out Transform[] route))
        {
            return route;
        }
        
        return null;
    }
}