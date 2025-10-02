using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager I { get; private set; }
    [System.Serializable] public class Pool { public string key; public GameObject prefab; public int prewarm = 8; }
    public Pool[] pools;

    private readonly Dictionary<string, Queue<GameObject>> q = new();

    void Awake()
    {
        if (I != null && I != this) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);
        foreach (var p in pools)
        {
            var key = p.key;
            q[key] = new Queue<GameObject>();
            for (int i = 0; i < p.prewarm; i++)
            {
                var go = Instantiate(p.prefab);
                go.SetActive(false);
                q[key].Enqueue(go);
            }
        }
    }

    public GameObject Spawn(string key, Vector3 pos, Quaternion rot)
    {
        if (!q.TryGetValue(key, out var pool)) { Debug.LogWarning($"Pool '{key}' missing"); return null; }
        GameObject go = pool.Count > 0 ? pool.Dequeue() : Instantiate(FindPool(key).prefab);
        go.transform.SetPositionAndRotation(pos, rot);
        go.SetActive(true);
        return go;
    }

    public void Despawn(string key, GameObject go)
    {
        go.SetActive(false);
        q[key].Enqueue(go);
    }

    Pool FindPool(string key)
    {
        foreach (var p in pools) if (p.key == key) return p;
        return null;
    }
}