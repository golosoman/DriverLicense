using TMPro;
using UnityEngine;

public class SceneBuilder : MonoBehaviour
{
    public TMP_Dropdown intersectionDropdown; // Дропдаун для выбора перекрестка
    public GameObject[] intersections; // Массив префабов перекрестков
    public Transform mainArea; // Точка, где будет отображаться перекресток

    private GameObject currentIntersection;

    void Start()
    {
        intersectionDropdown.onValueChanged.AddListener(OnIntersectionChanged);
        OnIntersectionChanged(0); // Инициализация с первым элементом
    }

    public void OnIntersectionChanged(int index)
    {
        if (currentIntersection != null)
        {
            Destroy(currentIntersection);
        }

        // Инстанцируем префаб
        currentIntersection = Instantiate(intersections[index], mainArea.position, Quaternion.identity, mainArea);

        // Устанавливаем масштаб перекрестка
        float scaleValue = 28f;
        currentIntersection.transform.localScale = new Vector3(scaleValue, scaleValue, 1f);

        SetupSpawnPoints();
    }

    private void SetupSpawnPoints()
    {
        // Здесь можно установить точки спавна для автомобилей, знаков и светофоров
    }

    public void PlaceObject(GameObject prefab)
    {
        // Логика для размещения объектов на соответствующих точках
    }
}