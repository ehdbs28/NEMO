using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    public static PoolManager Instance = null;

    private Dictionary<string, Pool<Poolable>> _pools = new Dictionary<string, Pool<Poolable>>();

    private Transform _trmParent;

    public PoolManager(Transform trmParent)
    {
        _trmParent = trmParent;
    }

    public void CreatePool(Poolable prefab, int cnt = 10)
    {
        Pool<Poolable> pool = new Pool<Poolable>(prefab, _trmParent, cnt);
        _pools.Add(prefab.gameObject.name, pool);
    }

    public Poolable Pop(string prefabName)
    {
        if(_pools.ContainsKey(prefabName) == false)
        {
            Debug.LogError("Prefab doesnt exist on PoolList");
            return null;
        }

        Poolable item = _pools[prefabName].Pop();
        return item;
    }

    public void Push(Poolable prefab)
    {
        _pools[prefab.name].Push(prefab);
    }
}
