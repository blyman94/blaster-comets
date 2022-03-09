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
    /// Bounds of the main camera.
    /// </summary>
    [Header("General")]
    [Tooltip("Bounds of the main camera.")]
    [SerializeField] private CameraBounds cameraBounds;

    /// <summary>
    /// Audio source used for explosions.
    /// </summary>
    [Tooltip("Audio source used for explosions.")]
    [SerializeField] private AudioSource explosionAudioSource;

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

    #region MonoBehaviour Methods
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
        return new Vector3(Random.Range(cameraBounds.MinXBound, 
            cameraBounds.MaxXBound), Random.Range(cameraBounds.MinYBound, 
            cameraBounds.MaxYBound), 0);
    }

    /// <summary>
    /// Packs small meteoroids into medium meteoroids, then packs medium
    /// meteoroids into large meteoroids. Spawns large meteoroids randomly on
    /// the screen.
    /// </summary>
    private void SpawnLargeMeteoroids()
    {
        for (int i = 0; i < settings.GameParameters.MeteoroidLargeCount; i++)
        {
            GameObject largeMeteoroidObject = MeteoroidLargePool.Get();
            largeMeteoroidObject.transform.SetParent(null);
            Meteoroid largeMeteoroid =
                largeMeteoroidObject.GetComponent<Meteoroid>();
            largeMeteoroid.ExplosionAudioSource = explosionAudioSource;
            largeMeteoroid.ExplosionPool = explosionPool;
            largeMeteoroid.TravelSpeedRange = 
                settings.GameParameters.MeteoroidTravelSpeedRange;

            for (int j = 0; j < settings.GameParameters.MeteoroidMediumCount; j++)
            {
                GameObject mediumMeteoroidObject = MeteoroidMediumPool.Get();
                mediumMeteoroidObject.transform.SetParent(null);
                Meteoroid mediumMeteoroid =
                    mediumMeteoroidObject.GetComponent<Meteoroid>();
                mediumMeteoroid.ExplosionAudioSource = explosionAudioSource;
                mediumMeteoroid.ExplosionPool = explosionPool;
                mediumMeteoroid.TravelSpeedRange = 
                    settings.GameParameters.MeteoroidTravelSpeedRange * 
                    settings.GameParameters.MeteoroidTravelSpeedMultiplier;

                for (int k = 0; k < settings.GameParameters.MeteoroidSmallCount; k++)
                {
                    GameObject smallMeteoroidObject = MeteoroidSmallPool.Get();
                    smallMeteoroidObject.transform.SetParent(null);
                    Meteoroid smallMeteoroid =
                        smallMeteoroidObject.GetComponent<Meteoroid>();
                    smallMeteoroid.ExplosionAudioSource = explosionAudioSource;
                    smallMeteoroid.ExplosionPool = explosionPool;
                    smallMeteoroid.TravelSpeedRange =
                        settings.GameParameters.MeteoroidTravelSpeedRange *
                        settings.GameParameters.MeteoroidTravelSpeedMultiplier *
                        settings.GameParameters.MeteoroidTravelSpeedMultiplier;

                    mediumMeteoroid.ChildMeteoroidObjects.Add(smallMeteoroidObject);
                }

                largeMeteoroid.ChildMeteoroidObjects.Add(mediumMeteoroidObject);
            }

            largeMeteoroidObject.transform.position = GetRandomSpawnPosition();
            largeMeteoroidObject.SetActive(true);
        }
    }
}
