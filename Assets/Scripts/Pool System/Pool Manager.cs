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
    //检测对象池运行尺寸大小
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
            //这段代码只会在unity编辑器里运行
              #if UNITY_EDITOR
            if(dictionary.ContainsKey(pool.Prefab))
            {
                Debug.Log("发现了相同的预制体：" +pool.Prefab.name);
               continue;
            }
              #endif
             
            dictionary.Add(pool.Prefab,pool);//生成预制体时给字典添加预制体的key值和value值
           Transform poolParent =  new GameObject("Pool: " + pool.Prefab.name).transform;
            poolParent.parent = transform; 
            pool.Initialize(poolParent);//生成预制体
        }
    }
    /// <summary>
    /// 根据传入的< paramref name = "prefab"></paramref></paramref>参数，释放对象池中预备好的游戏对象
    /// </summary>
    /// <param name="prefab"></param>
    /// 指定的游戏对象预制体
    /// <returns></returns>
    public static GameObject Release(GameObject prefab)
    {
         #if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.Log("对象池管理器找不到这个预制体：" + prefab.name);
            return null;
        }
          #endif
        return dictionary[prefab].PrepareObject();
    }
    /// <summary>
    /// 根据传入的prefab参数，在position参数位置释放对象池中预备好的游戏对象
    /// </summary>
    /// <param name="prefab"></param>
    /// 指定的游戏对象预制体
    /// <param name="position"></param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab,Vector3 position)
    {
         #if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.Log("对象池管理器找不到这个预制体：" + prefab.name);
            return null;
        }
          #endif
        return dictionary[prefab].PrepareObject(position);
    }

    /// <summary>
    /// 根据传入的prefab参数、rotation参数，在position参数位置释放对象池中预备好的游戏对象
    /// </summary>
    /// <param name="prefab"></param>
    /// 指定的游戏对象预制体
    /// <param name="position"></param>
    /// 指定释放位置
    /// <param name="rotation"></param>
    /// 指定的旋转值
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position,Quaternion rotation )
    {
         #if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.Log("对象池管理器找不到这个预制体：" + prefab.name);
            return null;
        }
         #endif
        return dictionary[prefab].PrepareObject(position,rotation);
    }
    /// <summary>
    /// 根据传入的prefab参数、rotation参数、localScale参数，在position参数位置释放对象池中预备好的游戏对象
    /// </summary>
    /// <param name="prefab"></param>
    /// 指定的游戏对象预制体
    /// <param name="position"></param>
    /// 指定释放位置
    /// <param name="rotation"></param>
    /// 指定的旋转值
    /// <param name="localScale"></param>
    /// 指定的缩放值
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation,Vector3 localScale)
    {
          #if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.Log("对象池管理器找不到这个预制体：" + prefab.name);
            return null;
        }
         #endif
        return dictionary[prefab].PrepareObject(position, rotation,localScale);
    }
}
