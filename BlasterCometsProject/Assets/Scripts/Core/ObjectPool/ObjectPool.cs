using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Instantiates and holds reference to game objects for recyclying purposes.
/// </summary>
public class ObjectPool : MonoBehaviour
{
    /// <summary>
    /// Initial number of instances of the pool object this pool will contain.
    /// </summary>
    [Tooltip("Initial number of instances of the pool object this pool " +
        "will contain.")]
    [SerializeField] private int initialSize;

    /// <summary>
    /// Prefab object to pool.
    /// </summary>
    [Tooltip("Prefab object to pool.")]
    [SerializeField] private GameObject objectToPool;

    /// <summary>
    /// Transform underwhich to store instantiated pool objects.
    /// </summary>
    [Tooltip("Transform underwhich to store instantiated pool objects.")]
    [SerializeField] private Transform poolParent;

    /// <summary>
    /// Stack representing the objects in the pool.
    /// </summary>
    private Stack<GameObject> pool;

    #region MonoBehaviour Methods
    public void Awake()
    {
        InitializePool();
    }
    #endregion

    /// <summary>
    /// Returns a free object from the pool. If there are no free objects, 
    /// returns null. If there are no free objects and the pool can grow, 
    /// instantiates a new object and returns a free object from the pool.
    /// </summary>
    /// <returns>GameObject representing a free object in the pool.</returns>
    public GameObject Get()
    {
        if (pool.Count > 0)
        {
            return pool.Pop();
        }

        GameObject poolGameObject;
        poolGameObject = GameObject.Instantiate(objectToPool, poolParent);

        IPoolObject poolObject =
            (IPoolObject)poolGameObject.GetComponent(typeof(IPoolObject));
        poolObject.OriginPool = this;

        poolGameObject.SetActive(false);
        return poolGameObject;
    }

    /// <summary>
    /// Releases the passed GameObject back to the pool.
    /// </summary>
    /// <param name="gameObject">GameObject to be released back to the 
    /// pool.</param>
    public void Release(GameObject gameObject)
    {
        gameObject.transform.SetParent(poolParent);
        gameObject.SetActive(false);
        pool.Push(gameObject);
    }

    /// <summary>
    /// Creates a new object pool.
    /// </summary>
    private void InitializePool()
    {
        pool = new Stack<GameObject>();

        for (int i = 0; i < initialSize; i++)
        {
            GameObject poolGameObject;
            poolGameObject = GameObject.Instantiate(objectToPool, poolParent);

            IPoolObject poolObject =
                (IPoolObject)poolGameObject.GetComponent(typeof(IPoolObject));
            poolObject.OriginPool = this;

            poolGameObject.SetActive(false);
            pool.Push(poolGameObject);
        }
    }
}
