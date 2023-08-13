using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItem : MonoBehaviour
{
    //the objectBool we belong to.
    private ObjectPooling myPool;

    /// <summary>
    /// Set the objectPool we belong to.
    /// </summary>
    public ObjectPooling Pool { set { myPool = value; } }

    #region Activating and deactivating item
    /// <summary>
    /// This is called just before the Object is Activated.
    /// </summary>
    protected virtual void Activate() { }

    /// <summary>
    /// This is called just before the Object is DeActivated.
    /// </summary>
    protected virtual void DeActivate() { }
    #endregion

    #region initialization and returning to Pool
    /// <summary>
    /// Initialize this and activate it
    /// </summary>
    /// <param name="position">The position where the item will be spawned.</param>
    /// <param name="rotation">The rotation of the object at the spawn position.</param>
    /// <param name="parent">The requested parent for the Object after it is spawned.</param>
    public void init(Vector3 position, Quaternion rotation, Transform parent)
    {
        transform.position = position;
        transform.rotation = rotation;
        transform.parent = parent;

        Activate();
    }
    /// <summary>
    /// Call this to return the item to the ObjectPool.
    /// </summary>
    public void ReturnToPool()
    {
        DeActivate();

        myPool.ReturnPooledObject(this);
    }
    #endregion
}
