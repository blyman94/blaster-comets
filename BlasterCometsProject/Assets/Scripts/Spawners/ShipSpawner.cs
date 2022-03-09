using UnityEngine;

/// <summary>
/// Spawns ships at the beginning of the game and whenever the player dies. A
/// new ship will only be spawned if the player has lives remaining.
/// </summary>
public class ShipSpawner : MonoBehaviour
{
    /// <summary>
    /// The game's settings.
    /// </summary>
    [Tooltip("The game's settings.")]
    [SerializeField] private Settings settings;

    /// <summary>
    /// IntVariable representing the number of lives the player has remaining.
    /// </summary>
    [Header("General")]
    [Tooltip("IntVariable representing the number of lives the player has " +
        "remaining.")]
    [SerializeField] private IntVariable playerLives;

    /// <summary>
    /// PlayerController object to which spawned ships will be assigned.
    /// </summary>
    [Tooltip("PlayerController object to which spawned ships will be " +
        "assigned.")]
    [SerializeField] private PlayerController shipController;

    /// <summary>
    /// Prefab representing the ship (player).
    /// </summary>
    [Tooltip("Prefab representing the ship (player).")]
    [SerializeField] private GameObject shipPrefab;

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
    /// Reference to the spawned ship's exploder module.
    /// </summary>
    private Exploder shipExploder;

    /// <summary>
    /// Reference to the spawned ship.
    /// </summary>
    private GameObject shipObject;

    /// <summary>
    /// CommandRelay component of the ship being spawned.
    /// </summary>
    private CommandRelay shipRelay;

    /// <summary>
    /// Timer to track how long before ship respawns.
    /// </summary>
    private float respawnTimer = -1;

    #region MonoBehaviour Methods
    private void Awake()
    {
        shipObject = Instantiate(shipPrefab, Vector3.zero, Quaternion.identity);
        ConfigureShip();
        ConfigureShipExploder();
        SpawnNewShip();
    }
    private void OnEnable()
    {
        shipExploder.EntityExploded += StartRespawnTimer;
    }
    private void Update()
    {
        HandleShipRespawn();
    }

    private void OnDisable()
    {
        shipExploder.EntityExploded -= StartRespawnTimer;
    }
    #endregion

    /// <summary>
    /// Assigns the ship's CommandRelay to the ship controller.
    /// </summary>
    private void ConfigureShip()
    {
        if (shipRelay == null)
        {
            shipRelay = shipObject.GetComponent<CommandRelay>();
        }

        if (shipRelay != null)
        {
            shipRelay.Rotator.RotationSpeed = 
                settings.GameParameters.ShipRotationSpeed;

            shipRelay.Thruster.MaxSpeed = settings.GameParameters.ShipMaxSpeed;
            shipRelay.Thruster.ThrustForce = 
                settings.GameParameters.ShipThrustForce;

            shipRelay.Weapon.Cooldown = 
                settings.GameParameters.ShipFireCooldown;
            shipRelay.Weapon.ProjectilePool = projectilePool;
            shipRelay.Weapon.ProjectileLifetime = 
                settings.GameParameters.ShipProjectileLifeTime;
            shipRelay.Weapon.ProjectileTravelSpeed =
                settings.GameParameters.ShipProjectileTravelSpeed;
        }
    }

    /// <summary>
    /// Configures the exploder module of the spawned ship.
    /// </summary>
    private void ConfigureShipExploder()
    {
        shipExploder = shipObject.GetComponent<Exploder>();
        shipExploder.ExplosionPool = explosionPool;
        shipExploder.EntityController = shipController;
    }

    /// <summary>
    /// Spawns a new ship if the respawn timer is active and reaches 0. Triggers
    /// end game sequence if there are no player lives remaining.
    /// </summary>
    private void HandleShipRespawn()
    {
        if (respawnTimer == -1)
        {
            return;
        }
        else if (respawnTimer >= 0)
        {
            respawnTimer -= Time.deltaTime;
        }
        else
        {
            if (playerLives.Value > 0)
            {
                SpawnNewShip();
            }
            else
            {
                // TODO: Trigger formal end game sequence here.
                Debug.Log("Game Over!");
            }
            respawnTimer = -1;
        }
    }

    /// <summary>
    /// Spawns a new ship by resetting the state of the existing ship.
    /// </summary>
    private void SpawnNewShip()
    {
        shipObject.transform.position = Vector3.zero;
        shipObject.transform.rotation = Quaternion.identity;
        shipExploder.Unexplode();
        shipController.RelayToControl = shipRelay;
    }

    /// <summary>
    /// Starts the ship respawn timer.
    /// </summary>
    public void StartRespawnTimer()
    {
        if (playerLives.Value > 0)
        {
            playerLives.Value -= 1;
        }
        respawnTimer = settings.GameParameters.ShipRespawnTime;
    }
}
