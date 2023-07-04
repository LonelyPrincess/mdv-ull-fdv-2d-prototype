using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;

    // Contains the type of elements that will be stored in the pool
    public GameObject objectToPool;

    // Contains the desired size of the object pool
    public int amountToPool;

    // Content of the object pool
    List<GameObject> pooledObjects = new List<GameObject>();

    void Awake()
    {
        SharedInstance = this;
    }

    // Populate object pool with inactive items of the requested type
    void Start()
    {
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++) {
            tmp = Instantiate(objectToPool, Vector2.zero, Quaternion.identity, this.transform);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    // Fetch first inactive item in the list
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++) {
            if (!pooledObjects[i].activeInHierarchy) {
                return pooledObjects[i];
            }
        }
        return null;
    }

    // Get count of all active objects currently in pool
    public int GetCountOfActiveObjectsInPool()
    {
        int activeObjectCount = 0;
        for (int i = 0; i < pooledObjects.Count; i++) {
            if (pooledObjects[i].activeInHierarchy) {
                activeObjectCount += 1;
            }
        }
        return activeObjectCount;
    }
}
