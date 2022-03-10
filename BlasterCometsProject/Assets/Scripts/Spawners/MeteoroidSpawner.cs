using UnityEngine;

/// <summary>
/// Packs smaller meteoroids into large meteoroids and places them randomly
/// within the screen bounds.
/// </summary>
public class MeteoroidSpawner : MonoBehaviour
{
    /// <summary>
    /// The game's settings.
    /// </summary>
    [Tooltip("The game's settings.")]
    [SerializeField] private Settings settings;

    /// <summary>
    /// Runtime set containing active meteoroid GameObjects.
    /// </summary>
    [Header("General")]
    [Tooltip("Runtime set containing active meteoroid GameObjects.")]
    [SerializeField] private RuntimeSet activeMeteoroidSet;

    /// <summary>
    /// Bounds of the main camera.
    /// </summary>
    [Tooltip("Bounds of the main camera.")]
    [SerializeField] private CameraBounds cameraBounds;

    /// <summary>
    /// IntVariable representing the player's current score.
    /// </summary>
    [Tooltip("IntVariable representing the player's current score.")]
    [SerializeField] private IntVariable playerScore;

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
    /// Object containing an ObjectPool for explosion particle systems.
    /// </summary>
    [Header("Object Pools")]
    [Tooltip("Object containing an ObjectPool for explosion particle systems.")]
    [SerializeField] private ObjectPool explosionPool;

    /// <summary>
    /// Object pool for large meteoroids.
    /// </summary>
    [Tooltip("Object pool for large meteoroids.")]
    [SerializeField] private ObjectPool MeteoroidLargePool;

    /// <summary>
    /// Object pool for medium meteoroids.
    /// </summary>
    [Tooltip("Object pool for medium meteoroids.")]
    [SerializeField] private ObjectPool MeteoroidMediumPool;

    /// <summary>
    /// Object pool for small meteoroids.
    /// </summary>
    [Tooltip("Object pool for small meteoroids.")]
    [SerializeField] private ObjectPool MeteoroidSmallPool;

    /// <summary>
    /// Timer to track time between large meteoroid spawns.
    /// </summary>
    private float levelTimer = 0;

    #region MonoBehaviour Methods
    private void Start()
    {
        if (activeMeteoroidSet != null)
        {
            activeMeteoroidSet.Clear();
        }
        SpawnLargeMeteoroids();
    }
    private void Update()
    {
        if (levelTimer > 0)
        {
            levelTimer -= Time.deltaTime;
            if (levelTimer <= 0)
            {
                SpawnLargeMeteoroids();
            }
        }
    }
    #endregion

    public void StartNewLevelTimer()
    {
        levelTimer = settings.GameParameters.TimeBetweenLevels;
    }

    /// <summary>
    /// Packs small meteoroids into medium meteoroids, then packs medium
    /// meteoroids into large meteoroids. Spawns large meteoroids randomly on
    /// the screen.
    /// </summary>
    private void SpawnLargeMeteoroids()
    {
        int largeMeteoroidCount = LargeMeteoroidCount();
        for (int i = 0; i < largeMeteoroidCount; i++)
        {
            GameObject largeMeteoroidObject = MeteoroidLargePool.Get();
            largeMeteoroidObject.transform.SetParent(null);

            CombatTarget largeMeteoroidCombatTarget = 
                largeMeteoroidObject.GetComponent<CombatTarget>();
            largeMeteoroidCombatTarget.PointValue =
                settings.GameParameters.MeteoroidLargePointsAwarded;

            Meteoroid largeMeteoroid =
                largeMeteoroidObject.GetComponent<Meteoroid>();
            largeMeteoroid.ActiveMeteoroidSet = activeMeteoroidSet;
            largeMeteoroid.ChildMeteoroidObjects.Clear();
            largeMeteoroid.ExplosionPool = explosionPool;
            largeMeteoroid.TravelSpeedRange =
                settings.GameParameters.MeteoroidTravelSpeedRange;

            for (int j = 0; j < settings.GameParameters.MeteoroidMediumCount; j++)
            {
                GameObject mediumMeteoroidObject = MeteoroidMediumPool.Get();
                mediumMeteoroidObject.transform.SetParent(null);

                CombatTarget mediumMeteoroidCombatTarget =
                    mediumMeteoroidObject.GetComponent<CombatTarget>();
                mediumMeteoroidCombatTarget.PointValue =
                    settings.GameParameters.MeteoroidMediumPointsAwarded;

                Meteoroid mediumMeteoroid =
                    mediumMeteoroidObject.GetComponent<Meteoroid>();
                mediumMeteoroid.ActiveMeteoroidSet = activeMeteoroidSet;
                mediumMeteoroid.ChildMeteoroidObjects.Clear();
                mediumMeteoroid.ExplosionPool = explosionPool;
                mediumMeteoroid.TravelSpeedRange =
                    settings.GameParameters.MeteoroidTravelSpeedRange *
                    settings.GameParameters.MeteoroidTravelSpeedMultiplier;

                for (int k = 0; k < settings.GameParameters.MeteoroidSmallCount; k++)
                {
                    GameObject smallMeteoroidObject = MeteoroidSmallPool.Get();
                    smallMeteoroidObject.transform.SetParent(null);

                    CombatTarget smallMeteoroidCombatTarget =
                        smallMeteoroidObject.GetComponent<CombatTarget>();
                    smallMeteoroidCombatTarget.PointValue =
                        settings.GameParameters.MeteoroidSmallPointsAwarded;

                    Meteoroid smallMeteoroid =
                        smallMeteoroidObject.GetComponent<Meteoroid>();
                    smallMeteoroid.ActiveMeteoroidSet = activeMeteoroidSet;
                    smallMeteoroid.ExplosionPool = explosionPool;
                    smallMeteoroid.TravelSpeedRange =
                        settings.GameParameters.MeteoroidTravelSpeedRange *
                        settings.GameParameters.MeteoroidTravelSpeedMultiplier *
                        settings.GameParameters.MeteoroidTravelSpeedMultiplier;

                    mediumMeteoroid.ChildMeteoroidObjects.Add(smallMeteoroidObject);
                }

                largeMeteoroid.ChildMeteoroidObjects.Add(mediumMeteoroidObject);
            }

            largeMeteoroidObject.transform.position = 
                cameraBounds.GetRandomPositionOn();
            largeMeteoroidObject.SetActive(true);
        }
    }

    /// <summary>
    /// Returns the number of large meteoroids that should be spawned based on
    /// the player's score.
    /// </summary>
    /// <returns></returns>
    public int LargeMeteoroidCount()
    {
        float playerScoreRatio = (float)playerScore.Value /
            settings.GameParameters.MeteoroidMaxSpawnScore;
        float rawCount =
            ((settings.GameParameters.MeteoroidLevelStartCountRange.y -
            settings.GameParameters.MeteoroidLevelStartCountRange.x) *
            playerScoreRatio) +
            settings.GameParameters.MeteoroidLevelStartCountRange.x;
        return (Mathf.FloorToInt(rawCount));
    }
}
