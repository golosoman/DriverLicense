using UnityEngine;
using System.Collections.Generic;

public class IntersectionManager : MonoBehaviour
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

    public enum Direction { North, South, West, East }

    // Инициализация словаря в Awake, чтобы быть уверенным, что данные установлены до любых вызовов
    private void Awake()
    {
        // Заполняем словарь маршрутами
        routes[(Direction.North, "forward")] = northForwardRoute;
        routes[(Direction.North, "left")] = northLeftRoute;
        routes[(Direction.North, "right")] = northRightRoute;
        routes[(Direction.North, "backward")] = northBackwardRoute;

        routes[(Direction.South, "forward")] = southForwardRoute;
        routes[(Direction.South, "left")] = southLeftRoute;
        routes[(Direction.South, "right")] = southRightRoute;
        routes[(Direction.South, "backward")] = southBackwardRoute;

        routes[(Direction.West, "forward")] = westForwardRoute;
        routes[(Direction.West, "left")] = westLeftRoute;
        routes[(Direction.West, "right")] = westRightRoute;
        routes[(Direction.West, "backward")] = westBackwardRoute;

        routes[(Direction.East, "forward")] = eastForwardRoute;
        routes[(Direction.East, "left")] = eastLeftRoute;
        routes[(Direction.East, "right")] = eastRightRoute;
        routes[(Direction.East, "backward")] = eastBackwardRoute;
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