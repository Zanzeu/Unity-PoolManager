using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private Pool[] pool;

    private static Dictionary<GameObject, Pool> dictionary;

    private void Awake()
    {
        dictionary = new Dictionary<GameObject, Pool>();

        Init(pool);
    }

#if UNITY_EDITOR

    private void OnDestroy()
    {
        CheckPoolSize(pool);
    }

#endif

    private void CheckPoolSize(params Pool[][] pools)
    {
        foreach (var poolArray in pools)
        {
            foreach (var pool in poolArray)
            {
                if (pool.RuntimeSize > pool.Size)
                {
                    Debug.LogWarning(string.Format("{0}�ĳߴ�{1}���ڳ�ʼ����صĳߴ�{2}!",
                        pool.Prefab.name,
                        pool.RuntimeSize,
                        pool.Size));
                }
            }
        }
    }

    private void Init(params Pool[][] pools)
    {
        foreach (var poolArray in pools)
        {
            foreach (var pool in poolArray)
            {
#if UNITY_EDITOR
                if (dictionary.ContainsKey(pool.Prefab))
                {
                    Debug.LogError("������ͬԤ����:" + pool.Prefab.name);
                    continue;
                }
#endif

                dictionary.Add(pool.Prefab, pool);

                Transform poolParent = new GameObject("Pool:" + pool.Prefab.name).transform;

                poolParent.parent = transform;
                pool.Init(poolParent);
            }
        }
    }

    #region  =====�ͷ�Ԥ����=====

    public static GameObject Release(GameObject prefab)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("�޷��ҵ�Ԥ����:" + prefab.name);

            return null;
        }
#endif
        return dictionary[prefab].PreparedObject();
    }

    public static GameObject Release(GameObject prefab, Vector3 postion)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("�޷��ҵ�Ԥ����:" + prefab.name);

            return null;
        }
#endif
        return dictionary[prefab].PreparedObject(postion);
    }

    public static GameObject Release(GameObject prefab, Vector3 postion, Quaternion rotation) 
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("�޷��ҵ�Ԥ����:" + prefab.name);

            return null;
        }
#endif
        return dictionary[prefab].PreparedObject(postion, rotation);
    }

    public static GameObject Release(GameObject prefab, Vector3 postion,Vector3 localScale)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("�޷��ҵ�Ԥ����:" + prefab.name);

            return null;
        }
#endif
        return dictionary[prefab].PreparedObject(postion, localScale);
    }

    public static GameObject Release(GameObject prefab, Vector3 postion, Quaternion rotation, Vector3 localScale)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("�޷��ҵ�Ԥ����:" + prefab.name);

            return null;
        }
#endif
        return dictionary[prefab].PreparedObject(postion, rotation, localScale);
    }
    #endregion
}

