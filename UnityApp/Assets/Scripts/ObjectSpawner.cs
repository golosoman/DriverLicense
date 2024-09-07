using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public GameObject signPrefab;

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        // Найти все точки спавна для знаков
        GameObject[] signSpawnPoints = GameObject.FindGameObjectsWithTag("SignSpawn");
        foreach (GameObject spawnPoint in signSpawnPoints)
        {
            // GameObject sign = Instantiate(signPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            Instantiate(signPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            // Дополнительные настройки для знака, если необходимо
        }

        // Найти все точки спавна для машин
        GameObject[] carSpawnPoints = GameObject.FindGameObjectsWithTag("CarSpawn");
        foreach (GameObject spawnPoint in carSpawnPoints)
        {
            Debug.Log("+1");
            // GameObject car = Instantiate(carPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            Instantiate(carPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            // Дополнительные настройки для машины, если необходимо
        }
    }
}
