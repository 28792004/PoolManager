using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] Pool[] enemyPools;
    [SerializeField] Pool[] playerprojectilePools;
    [SerializeField] Pool[] enemyprojectilePools;
    [SerializeField] Pool[] vFXPools;
    [SerializeField] Pool[] lootItemPools;
     static Dictionary<GameObject, Pool> dictionary;
    void Awake()
    {
        dictionary = new Dictionary<GameObject, Pool>();

        Initialize(playerprojectilePools);
        Initialize(enemyprojectilePools);
        Initialize(vFXPools);
        Initialize(enemyPools);
        Initialize(lootItemPools);
    }
    //����������гߴ��С
#if UNITY_EDITOR
    void OnDestroy()
    {
        CheckPoolSize(playerprojectilePools);
        CheckPoolSize(enemyprojectilePools);
        CheckPoolSize(vFXPools);
        CheckPoolSize(enemyPools);
        CheckPoolSize(lootItemPools);
    }
#endif
    void CheckPoolSize(Pool[] pools )
    {
        foreach(var pool in pools)
        {
            if(pool.RuntimeSize > pool.Size)
            {
                Debug.LogWarning(string.Format(pool.Prefab.name,pool.RuntimeSize,pool.Size));       
            }
        }
    }
    void Initialize(Pool[] pools)
    {
        foreach (var pool in pools)
        {
            //��δ���ֻ����unity�༭��������
              #if UNITY_EDITOR
            if(dictionary.ContainsKey(pool.Prefab))
            {
                Debug.Log("��������ͬ��Ԥ���壺" +pool.Prefab.name);
               continue;
            }
              #endif
             
            dictionary.Add(pool.Prefab,pool);//����Ԥ����ʱ���ֵ����Ԥ�����keyֵ��valueֵ
           Transform poolParent =  new GameObject("Pool: " + pool.Prefab.name).transform;
            poolParent.parent = transform; 
            pool.Initialize(poolParent);//����Ԥ����
        }
    }
    /// <summary>
    /// ���ݴ����< paramref name = "prefab"></paramref></paramref>�������ͷŶ������Ԥ���õ���Ϸ����
    /// </summary>
    /// <param name="prefab"></param>
    /// ָ������Ϸ����Ԥ����
    /// <returns></returns>
    public static GameObject Release(GameObject prefab)
    {
         #if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.Log("����ع������Ҳ������Ԥ���壺" + prefab.name);
            return null;
        }
          #endif
        return dictionary[prefab].PrepareObject();
    }
    /// <summary>
    /// ���ݴ����prefab��������position����λ���ͷŶ������Ԥ���õ���Ϸ����
    /// </summary>
    /// <param name="prefab"></param>
    /// ָ������Ϸ����Ԥ����
    /// <param name="position"></param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab,Vector3 position)
    {
         #if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.Log("����ع������Ҳ������Ԥ���壺" + prefab.name);
            return null;
        }
          #endif
        return dictionary[prefab].PrepareObject(position);
    }

    /// <summary>
    /// ���ݴ����prefab������rotation��������position����λ���ͷŶ������Ԥ���õ���Ϸ����
    /// </summary>
    /// <param name="prefab"></param>
    /// ָ������Ϸ����Ԥ����
    /// <param name="position"></param>
    /// ָ���ͷ�λ��
    /// <param name="rotation"></param>
    /// ָ������תֵ
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position,Quaternion rotation )
    {
         #if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.Log("����ع������Ҳ������Ԥ���壺" + prefab.name);
            return null;
        }
         #endif
        return dictionary[prefab].PrepareObject(position,rotation);
    }
    /// <summary>
    /// ���ݴ����prefab������rotation������localScale��������position����λ���ͷŶ������Ԥ���õ���Ϸ����
    /// </summary>
    /// <param name="prefab"></param>
    /// ָ������Ϸ����Ԥ����
    /// <param name="position"></param>
    /// ָ���ͷ�λ��
    /// <param name="rotation"></param>
    /// ָ������תֵ
    /// <param name="localScale"></param>
    /// ָ��������ֵ
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation,Vector3 localScale)
    {
          #if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.Log("����ع������Ҳ������Ԥ���壺" + prefab.name);
            return null;
        }
         #endif
        return dictionary[prefab].PrepareObject(position, rotation,localScale);
    }
}
