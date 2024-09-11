using UnityEngine;
using System.Collections.Generic;

public static class PrefabManager
{
    private static Dictionary<string, GameObject> prefabCache = new Dictionary<string, GameObject>();

    public static GameObject GetPrefab(string prefabPath)
    {
        if (prefabCache.ContainsKey(prefabPath))
        {
            return prefabCache[prefabPath];
        }

        GameObject prefab = Resources.Load<GameObject>(prefabPath);
        if (prefab != null)
        {
            prefabCache.Add(prefabPath, prefab);
            return prefab;
        }
        else
        {
            Debug.LogError($"Prefab not found: {prefabPath}");
            return null;
        }
    }
}
