using UnityEngine;

/// <summary>
/// Packs smaller meteoroids into large meteoroids and places them randomly
/// within the screen bounds.
/// </summary>
public class MeteoroidSpawner : MonoBehaviour
{
    /// <summary>
    /// Main Camera used to determine random spawn positions.
    /// </summary>
    [Header("General")]
    [Tooltip("Main Camera used to determine random spawn positions.")]
    [SerializeField] private Camera mainCamera;

    /// <summary>
    /// Object containing an ObjectPool for explosion particle systems.
    /// </summary>
    [Tooltip("Object containing an ObjectPool for explosion particle systems.")]
    [SerializeField] private ExplosionPool explosionPool;

    /// <summary>
    /// Number of large meteoroids to spawn.
    /// </summary>
    [Header("Meteoroid Counts")]
    [Tooltip("Number of large meteoroids to spawn.")]
    [SerializeField] private int largeMeteoroidCount = 1;

    /// <summary>
    /// Number of small meteoroids spawned when a medium meteoroid is destroyed.
    /// </summary>
    [Tooltip("Number of medium meteoroids spawned when a large meteoroid " +
        "is destroyed.")]
    [SerializeField] private int mediumMeteoroidsPer = 2;

    /// <summary>
    /// Number of small meteoroids spawned when a medium meteoroid is destroyed.
    /// </summary>
    [Tooltip("Number of small meteoroids spawned when a medium meteoroid " +
        "is destroyed.")]
    [SerializeField] private int smallMeteoroidsPer = 2;

    /// <summary>
    /// GameObject prefab representing the large meteoroid.
    /// </summary>
    [Header("Meteoroid Prefabs")]
    [Tooltip("GameObject prefab representing the large meteoroid.")]
    [SerializeField] private GameObject meteoroidLargePrefab;

    /// <summary>
    /// GameObject prefab representing the medium meteoroid.
    /// </summary>
    [Tooltip("GameObject prefab representing the medium meteoroid.")]
    [SerializeField] private GameObject meteoroidMediumPrefab;

    /// <summary>
    /// GameObject prefab representing the small meteoroid.
    /// </summary>
    [Tooltip("GameObject prefab representing the small meteoroid.")]
    [SerializeField] private GameObject meteoroidSmallPrefab;

    /// <summary>
    /// Initial size of the large meteoroid pool. Will be doubled for the medium 
    /// meteoroid pool size, and doubled again for the small meteoroid pool 
    /// size.
    /// </summary>
    [Header("Meteoroid Pool")]
    [Tooltip("Initial size of the large meteoroid pool. Sizes for the " +
        "medium and small meteoroid pools will be calculated based on this " +
        "size and the respective per counts.")]
    [SerializeField] private int initialSize = 12;

    /// <summary>
    /// Transform to which pooled meteoroid objects are parented.
    /// </summary>
    [Tooltip("Transform to which pooled meteoroid objects are parented.")]
    [SerializeField] private Transform meteoroidPoolParent;

    /// <summary>
    /// Object pool for large meteoroids.
    /// </summary>
    private ObjectPool meteoroidPoolLarge;

    /// <summary>
    /// Object pool for medium meteoroids.
    /// </summary>
    private ObjectPool meteoroidPoolMedium;

    /// <summary>
    /// Object pool for small meteoroids.
    /// </summary>
    private ObjectPool meteoroidPoolsmall;

    #region MonoBehaviour Methods
    private void Awake()
    {
        InitializeMeteoroidPools();
    }

    private void Start()
    {
        SpawnLargeMeteoroids();
    }
    #endregion

    /// <summary>
    /// Calculates a random position to spawn large meteoroids based on main
    /// camera bounds.
    /// </summary>
    /// <returns>Vector3 representing a random spawn position.</returns>
    private Vector3 GetRandomSpawnPosition()
    {
        mainCamera.GetBounds(out float maxXBound, out float maxYBound,
            out float minXBound, out float minYBound);

        return new Vector3(Random.Range(minXBound, maxXBound),
            Random.Range(minYBound, maxYBound), 0);
    }

    /// <summary>
    /// Initializes ObjectPools for each of the 3 meteoroid sizes.
    /// </summary>
    private void InitializeMeteoroidPools()
    {
        meteoroidPoolLarge = new ObjectPool(meteoroidLargePrefab, initialSize,
                    false, meteoroidPoolParent);

        int meteoroidPoolMediumSize = initialSize * mediumMeteoroidsPer;
        meteoroidPoolMedium = new ObjectPool(meteoroidMediumPrefab,
            meteoroidPoolMediumSize, false, meteoroidPoolParent);

        int meteoroidPoolSmallSize = initialSize * mediumMeteoroidsPer *
            smallMeteoroidsPer;
        meteoroidPoolsmall = new ObjectPool(meteoroidSmallPrefab,
            meteoroidPoolSmallSize, false, meteoroidPoolParent);
    }

    /// <summary>
    /// Packs small meteoroids into medium meteoroids, then packs medium
    /// meteoroids into large meteoroids. Spawns large meteoroids randomly on
    /// the screen.
    /// </summary>
    private void SpawnLargeMeteoroids()
    {
        for (int i = 0; i < largeMeteoroidCount; i++)
        {
            GameObject largeMeteoroidObject = meteoroidPoolLarge.Get();
            largeMeteoroidObject.transform.SetParent(null);
            Meteoroid largeMeteoroid =
                largeMeteoroidObject.GetComponent<Meteoroid>();
            largeMeteoroid.ExplosionPool = explosionPool.Pool;

            for (int j = 0; j < mediumMeteoroidsPer; j++)
            {
                GameObject mediumMeteoroidObject = meteoroidPoolMedium.Get();
                mediumMeteoroidObject.transform.SetParent(null);
                Meteoroid mediumMeteoroid =
                    mediumMeteoroidObject.GetComponent<Meteoroid>();
                mediumMeteoroid.ExplosionPool = explosionPool.Pool;

                for (int k = 0; k < smallMeteoroidsPer; k++)
                {
                    GameObject smallMeteoroidObject = meteoroidPoolsmall.Get();
                    smallMeteoroidObject.transform.SetParent(null);
                    Meteoroid smallMeteoroid =
                        smallMeteoroidObject.GetComponent<Meteoroid>();
                    smallMeteoroid.ExplosionPool = explosionPool.Pool;

                    mediumMeteoroid.ChildMeteoroidObjects.Add(smallMeteoroidObject);
                }

                largeMeteoroid.ChildMeteoroidObjects.Add(mediumMeteoroidObject);
            }

            largeMeteoroidObject.transform.position = GetRandomSpawnPosition();
            largeMeteoroidObject.SetActive(true);
        }
    }
}
