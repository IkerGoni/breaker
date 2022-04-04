using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ObjectPool
{
    public GameObject prefab;
    public int Size;
}

public class PoolManager : MonoBehaviour
{
    private static PoolManager _instance;
    public List<ObjectPool> ObjectPoolList;
    private static Dictionary<int, Queue<GameObject>> objectPoolDictionary;
    
    private void Start()
    {

        if (_instance != null)
        {
            Destroy(this);
            return;
        }

        _instance = this;

        objectPoolDictionary = new Dictionary<int, Queue<GameObject>>();
        foreach(ObjectPool pool in ObjectPoolList)
        {
            Queue<GameObject> poolQueue = new Queue<GameObject>();
            for(int i=0; i<pool.Size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, this.transform);
                obj.SetActive(false);
                poolQueue.Enqueue(obj);
            }
            objectPoolDictionary.Add(pool.prefab.GetInstanceID(), poolQueue);
        }
    }


    public static GameObject GetObjectFromPool(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        int instanceID = prefab.GetInstanceID();

        if(objectPoolDictionary.ContainsKey(instanceID))
        {
            if (objectPoolDictionary[instanceID].Count > 0)
            {
                GameObject obj = objectPoolDictionary[instanceID].Dequeue();
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                if (parent != null)
                    obj.transform.parent = parent;
                obj.SetActive(true);

                return obj;
            }
            else
            {
                //Instantiate and return, no more left in Pool
                GameObject obj = Instantiate(prefab);
                if (parent != null)
                    obj.transform.parent = parent;                
                return obj;
            }
        }
        else
        {
            Debug.Log(prefab.name + " object pool is not available");
        }

        return null;
    }
    
    public static void ReturnObjectToPool(int instanceID, GameObject poolObject)
    {
        if (objectPoolDictionary.ContainsKey(instanceID))
        {
            objectPoolDictionary[instanceID].Enqueue(poolObject);
            poolObject.SetActive(false);
        }
        else
        {
            Debug.Log(instanceID + " object pool is not available");
        }
    }
}