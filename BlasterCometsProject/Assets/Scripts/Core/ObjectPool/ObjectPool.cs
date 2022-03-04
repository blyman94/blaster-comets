using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Instantiates and holds reference to game objects for recyclying purposes.
/// </summary>
public class ObjectPool
{
    /// <summary>
    /// Determines whether this ObjectPool can grow if a pool object is
    /// requested and none are available.
    /// </summary>
    private bool canGrow;

    /// <summary>
    /// Initial number of instances of the pool object this pool will contain.
    /// </summary>
    private int initialSize;

    /// <summary>
    /// Prefab object to pool.
    /// </summary>
    private GameObject objectToPool;

    /// <summary>
    /// Stack representing the objects in the pool.
    /// </summary>
    private Stack<GameObject> pool;

    /// <summary>
    /// Transform underwhich to store instantiated pool objects.
    /// </summary>
    private Transform poolParent;

    /// <summary>
    /// Initializes an instance of the ObjectPool class with the given 
    /// parameters.
    /// </summary>
    /// <param name="objectToPool">Prefab object to pool..</param>
    /// <param name="initialSize">Initial number of instances of the pool object
    /// this pool will contain.</param>
    /// <param name="canGrow">Determines whether this ObjectPool can grow if a 
    /// pool object is requested and none are available.</param>
    public ObjectPool(GameObject objectToPool, int initialSize, bool canGrow)
    {
        this.canGrow = canGrow;
        this.initialSize = initialSize;
        this.objectToPool = objectToPool;
        pool = new Stack<GameObject>();

        InitializePool();
    }

    /// <summary>
    /// Initializes an instance of the ObjectPool class with the given 
    /// parameters. Assigns the parent of instantiated objects to a pool parent,
    /// so as to keep the Hierarchy view in the Unity Editor clean.
    /// </summary>
    /// <param name="objectToPool">Prefab object to pool..</param>
    /// <param name="initialSize">Initial number of instances of the pool object
    /// this pool will contain.</param>
    /// <param name="canGrow">Determines whether this ObjectPool can grow if a 
    /// pool object is requested and none are available.</param>
    /// <param name="poolParent">Transform to which instantiated objects will be
    /// parented.</param>
    public ObjectPool(GameObject objectToPool, int initialSize, bool canGrow,
        Transform poolParent)
    {
        this.canGrow = canGrow;
        this.initialSize = initialSize;
        this.objectToPool = objectToPool;
        pool = new Stack<GameObject>();
        this.poolParent = poolParent;

        InitializePool();
    }

    /// <summary>
    /// Returns a free object from the pool. If there are no free objects, 
    /// returns null. If there are no free objects and the pool can grow, 
    /// instantiates a new object and returns the free object from the pool.
    /// </summary>
    /// <returns>Returns a free object from the pool.</returns>
    public GameObject Get()
    {
        if (pool.Count > 0)
        {
            return pool.Pop();
        }

        if (canGrow)
        {
            GameObject poolGameObject;
            if (poolParent != null)
            {
                poolGameObject = GameObject.Instantiate(objectToPool, poolParent);
            }
            else
            {
                poolGameObject = GameObject.Instantiate(objectToPool, Vector3.zero,
                    Quaternion.identity);
            }

            IPoolObject poolObject =
                (IPoolObject)poolGameObject.GetComponent(typeof(IPoolObject));
            if (poolObject != null)
            {
                poolObject.OriginPool = this;
            }

            poolGameObject.SetActive(false);
            return poolGameObject;
        }

        return null;
    }

    /// <summary>
    /// Releases the passed GameObject back to the pool.
    /// </summary>
    /// <param name="gameObject">GameObject to be released back to the 
    /// pool.</param>
    public void Release(GameObject gameObject)
    {
        if (poolParent != null)
        {
            gameObject.transform.SetParent(poolParent);
        }
        gameObject.SetActive(false);
        pool.Push(gameObject);
    }

    /// <summary>
    /// Creates a new object pool.
    /// </summary>
    private void InitializePool()
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject poolGameObject;
            if (poolParent != null)
            {
                poolGameObject = GameObject.Instantiate(objectToPool, poolParent);
            }
            else
            {
                poolGameObject = GameObject.Instantiate(objectToPool, Vector3.zero,
                    Quaternion.identity);
            }

            IPoolObject poolObject =
                (IPoolObject)poolGameObject.GetComponent(typeof(IPoolObject));
            if (poolObject != null)
            {
                poolObject.OriginPool = this;
            }

            poolGameObject.SetActive(false);
            pool.Push(poolGameObject);
        }
    }
}
