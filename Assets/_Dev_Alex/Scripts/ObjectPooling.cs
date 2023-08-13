using SimplePool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    // The Object that will be in the Pool.
    public GameObject pooledObject;
    // The amount of Objects inside of the Pool.
    public int poolSize = 12;

    // This is the Stack with all of the PooledItems.
    private Stack<PoolItem> objectPool;

    private void Start()
    {
        objectPool = new Stack<PoolItem>(poolSize);

        //This will fill the Pool.
        Expand(poolSize);
    }


    #region ExpandSize this will also fill the pool
    private void Expand(int expenionSize)
    {
        // This ForLoop will make/spawn the correct amount of Objects for your game.
        for (int i = 0; i < expenionSize; i++)
        {
            GameObject newObject = Instantiate(pooledObject);
            PoolItem item = newObject.GetComponent<PoolItem>();
            item.Pool = this;
            ReturnPooledObject(item);
        }
    }
    #endregion

    #region Getting pooled objects and seeing how much are left inside of the Pool
    public GameObject GetPooledObject(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        // If the Pool count  is 0, then it won't give back an Object.
        if (objectPool.Count == 0)
        {
            Expand(poolSize);
            return null;
        }

        PoolItem item = objectPool.Pop();
        item.init(position, rotation, parent != null ? parent : transform);
        item.gameObject.SetActive(true);
        return item.gameObject;
        
    }
    #endregion

    #region ReturningPooledObjects
    public void ReturnPooledObject(PoolItem item)
    {
        if (!item.gameObject.activeSelf)
            return;
        // The 2 lines of code below me, have to be in this exact order, otherwise unity won't understand.
        item.transform.parent = transform;
        item.gameObject.SetActive(false);

        objectPool.Push(item);
    }
    #endregion
}
