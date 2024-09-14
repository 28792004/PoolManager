using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable] public class Pool
{
    public GameObject Prefab => prefab;
    [SerializeField]GameObject prefab;
    Queue<GameObject> queue ;
    public int Size => size;
    public int RuntimeSize => queue.Count;
    [SerializeField]int size = 1;
    Transform parent;
    public void Initialize(Transform parent)
    {
        queue = new Queue<GameObject>();
        for (var i = 0; i < size; ++i)
        {
            queue.Enqueue(Copy());//在最末尾添加对象
            this.parent = parent;
        }
    }
    //
    GameObject Copy()
    {
       var copy = GameObject.Instantiate(prefab,parent);
        copy.SetActive(false);

        return copy;
    }
    GameObject AvailableObject()
    {
        GameObject availableobject = null;
        if(queue.Count > 0 && !queue.Peek().activeSelf)
        {
           availableobject =  queue.Dequeue();//返回队列中第一个值并将他移除
        }
        else
        {
            availableobject = Copy();
        }
        queue.Enqueue(availableobject);
        return availableobject; 
    }
    public GameObject PrepareObject()
    {
        GameObject prepareobject = AvailableObject();
        prepareobject.SetActive(true);
        return prepareobject;
    }
    public GameObject PrepareObject(Vector3 position )
    {
        GameObject prepareobject = AvailableObject();
        prepareobject.SetActive(true);
        prepareobject.transform.position = position;
        
        return prepareobject;
    }
    public GameObject PrepareObject(Vector3 position, Quaternion roation )
    {
        GameObject prepareobject = AvailableObject();
        prepareobject.SetActive(true);
        prepareobject.transform.position = position;
        prepareobject.transform.rotation = roation;
        
        return prepareobject;
    }
    public GameObject PrepareObject(Vector3 position, Quaternion roation, Vector3 localScale)
    {
        GameObject prepareobject = AvailableObject();
        prepareobject.SetActive(true);
        prepareobject.transform.position = position;
        prepareobject.transform.rotation = roation;
        prepareobject.transform.localScale = localScale;
        return prepareobject;
    }
}
