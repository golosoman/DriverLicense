using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IntersectionConstructor : MonoBehaviour
{
    public GameObject intersectionPrefab; // Prefab перекрестка
    public GameObject carPrefab; // Prefab машины
    public GameObject[] waypointPrefabs; // Массив Prefabs точек движения
    public Transform[] waypointSpawnPoints; // Массив точек спавна точек движения
    public Transform spawnPoint; // Точка спавна машин

    private GameObject currentIntersection; // Текущий перекресток на сцене

    void Start()
    {
        // Создаем первый перекресток
        SpawnIntersection();
    }

    // Функция для создания перекрестка
    public void SpawnIntersection()
    {
        // Удаляем предыдущий перекресток (если он есть)
        if (currentIntersection != null)
        {
            Destroy(currentIntersection);
        }

        // Создаем новый перекресток
        currentIntersection = Instantiate(intersectionPrefab);

        // ... (добавьте код для спавна точек движения)
    }

    // Обработчик перетаскивания машины
    public void OnDrag(PointerEventData eventData)
    {
        // Если машина была перетащена
        if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Car"))
        {
            // Получаем позицию мыши
            Vector2 mousePosition = eventData.position;
            // Устанавливаем позицию машины
            eventData.pointerDrag.transform.position = mousePosition;
        }
    }

    // Обработчик завершения перетаскивания
    public void OnEndDrag(PointerEventData eventData)
    {
        // Если машина была перетащена
        if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Car"))
        {
            // Получаем точку спавна машин
            Transform closestSpawnPoint = GetClosestPoint(eventData.pointerDrag.transform);

            // Устанавливаем позицию машины на ближайшую точку спавна
            eventData.pointerDrag.transform.position = closestSpawnPoint.position;

            // ... (добавьте код для установки пути машины)
        }
    }

    // Находит ближайшую точку спавна машин
    private Transform GetClosestPoint(Transform target)
    {
        Transform closest = null;
        float closestDistance = Mathf.Infinity;
        foreach (Transform point in waypointSpawnPoints)
        {
            float distance = Vector2.Distance(target.position, point.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = point;
            }
        }
        return closest;
    }
}