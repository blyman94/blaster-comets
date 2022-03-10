using UnityEngine;

/// <summary>
/// Spawns bogeys with AI to actively attack the player.
/// </summary>
public class BogeySpawner : MonoBehaviour
{   
    /// <summary>
    /// Delegate to signal that this spawner's bogey has been destroyed.
    /// </summary>
    public SimpleDelegate BogeyDown;

    /// <summary>
    /// The game's settings.
    /// </summary>
    [Tooltip("The game's settings.")]
    [SerializeField] private Settings settings;

    /// <summary>
    /// Prefab representing a bogey.
    /// </summary>
    [Header("General")]
    [Tooltip("Prefab representing a bogey.")]
    [SerializeField] private GameObject bogeyPrefab;

    /// <summary>
    /// Bounds of the main camera.
    /// </summary>
    [Tooltip("Bounds of the main camera.")]
    [SerializeField] private CameraBounds cameraBounds;

    /// <summary>
    /// Is the bogey being spawned a large bogey?
    /// </summary>
    [Tooltip("Is the bogey being spawned a large bogey?")]
    [SerializeField] private bool isLargeBogey;

    /// <summary>
    /// Pool from which destroyed ship's explosions will be spawned from.
    /// </summary>
    [Header("Object Pools")]
    [Tooltip("Pool from which destroyed ship's explosions will be " +
        "spawned from.")]
    [SerializeField] private ObjectPool explosionPool;

    /// <summary>
    /// Pool from which bogey projectiles will be spawned from.
    /// </summary>
    [Tooltip("Pool from which bogey projectiles will be spawned from.")]
    [SerializeField] private ObjectPool projectilePool;

    /// <summary>
    /// Combat Target for the bogey object.
    /// </summary>
    private CombatTarget bogeyCombatTarget;

    /// <summary>
    /// Exploder module for the bogey object.
    /// </summary>
    private Exploder bogeyExploder;

    /// <summary>
    /// GameObject representing the instance of the bogey.
    /// </summary>
    private GameObject bogeyObject;

    /// <summary>
    /// CommandRelay of the bogey.
    /// </summary>
    private CommandRelay bogeyRelay;

    #region MonoBehaviour Methods
    private void Awake()
    {
        bogeyObject = Instantiate(bogeyPrefab, Vector3.zero,
                    Quaternion.identity);
        bogeyObject.SetActive(false);

        bogeyExploder = bogeyObject.GetComponent<Exploder>();
        bogeyExploder.ExplosionPool = explosionPool;

        bogeyCombatTarget = bogeyObject.GetComponent<CombatTarget>();
        bogeyCombatTarget.PointValue = isLargeBogey ?
            settings.GameParameters.BogeyLargePointsAwarded :
            settings.GameParameters.BogeySmallPointsAwarded;

        ConfigureBogeyRelay();
    }
    private void OnEnable()
    {
        if (bogeyExploder != null)
        {
            bogeyExploder.EntityExploded += OnBogeyExplode;
        }
    }
    private void OnDisable()
    {
        if (bogeyExploder != null)
        {
            bogeyExploder.EntityExploded += OnBogeyExplode;
        }
    }
    #endregion

    /// <summary>
    /// Spawns a bogey.
    /// </summary>
    public CommandRelay SpawnBogey()
    {
        bogeyObject.transform.position =
                cameraBounds.GetRandomPositionOn();
        bogeyObject.SetActive(true);
        bogeyExploder.Unexplode();
        return bogeyRelay;
    }

    /// <summary>
    /// Deactivates the bogey and signals the bogey is down.
    /// </summary>
    private void OnBogeyExplode()
    {
        BogeyDown?.Invoke();
    }

    /// <summary>
    /// Configures the weapon of the bogey with parameters from GameSettings.
    /// </summary>
    private void ConfigureBogeyRelay()
    {
        bogeyRelay = bogeyObject.GetComponent<CommandRelay>();

        if (bogeyRelay != null)
        {
            bogeyRelay.Weapon.Cooldown =
                settings.GameParameters.BogeyFireRate;
            bogeyRelay.Weapon.ProjectilePool = projectilePool;
            bogeyRelay.Weapon.ProjectileLifetime =
                settings.GameParameters.BogeyProjectileLifeTime;
            bogeyRelay.Weapon.ProjectileTravelSpeed =
                settings.GameParameters.BogeyProjectileTravelSpeed;
        }
    }
}
