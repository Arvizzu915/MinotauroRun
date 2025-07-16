using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool; // Required for ObjectPool<T>

public class ObstaclePoolManager : MonoBehaviour
{
    public static ObstaclePoolManager Instance;

    [SerializeField] public GameObject[] obstaclePrefabs;
    [SerializeField] private int poolSize = 10;

    private Dictionary<string, ObjectPool<GameObject>> poolDict = new();

    private void Awake()
    {
        Instance = this;

        foreach (var prefab in obstaclePrefabs)
        {
            var pool = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(prefab),
                actionOnGet: obj => obj.SetActive(true),
                actionOnRelease: obj => obj.SetActive(false),
                actionOnDestroy: obj => Destroy(obj),
                collectionCheck: false,
                defaultCapacity: poolSize
            );

            poolDict[prefab.name] = pool;
        }
    }

    public GameObject Get(string prefabName, Vector3 position, Transform parent = null)
    {
        if (!poolDict.ContainsKey(prefabName))
        {
            Debug.LogWarning($"No pool found for: {prefabName}");
            return null;
        }

        GameObject obj = poolDict[prefabName].Get();
        obj.transform.SetParent(parent);
        obj.transform.position = position;
        return obj;
    }

    public void Return(string prefabName, GameObject obj)
    {
        if (!poolDict.ContainsKey(prefabName))
        {
            Debug.LogWarning($"No pool to return to: {prefabName}");
            Destroy(obj);
            return;
        }

        poolDict[prefabName].Release(obj);
    }
}
