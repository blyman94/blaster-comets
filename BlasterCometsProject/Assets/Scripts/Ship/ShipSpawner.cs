using UnityEngine;

/// <summary>
/// Spawns ships at the beginning of the game and whenever the player dies. A
/// new ship will only be spawned if the player has lives remaining.
/// </summary>
public class ShipSpawner : MonoBehaviour
{
    /// <summary>
    /// Pool from which destroyed ship's explosions will be spawned from.
    /// </summary>
    [Header("General")]
    [Tooltip("Pool from which destroyed ship's explosions will be " +
        "spawned from.")]
    [SerializeField] private ExplosionPool explosionPool;

    /// <summary>
    /// IntVariable representing the number of lives the player has remaining.
    /// </summary>
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
    /// Amount of time it takes for a ship to respawn after exploding.
    /// </summary>
    [Tooltip("Amount of time it takes for a ship to respawn after exploding.")]
    [SerializeField] private float shipRespawnTime = 3;

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
        AssignShipRelayToController();
        ConfigureShipExploder();
    }
    private void OnEnable()
    {
        shipExploder.EntityExploded += StartRespawnTimer;
    }
    private void Update()
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
    private void OnDisable()
    {
        shipExploder.EntityExploded -= StartRespawnTimer;
    }
    #endregion

    /// <summary>
    /// Assigns the ship's CommandRelay to the ship controller.
    /// </summary>
    private void AssignShipRelayToController()
    {
        if (shipRelay == null)
        {
            shipRelay = shipObject.GetComponent<CommandRelay>();
        }

        if (shipRelay != null)
        {
            shipController.RelayToControl = shipRelay;
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
    /// Spawns a new ship by resetting the state of the existing ship.
    /// </summary>
    private void SpawnNewShip()
    {
        shipObject.transform.position = Vector3.zero;
        shipObject.transform.rotation = Quaternion.identity;
        shipExploder.Unexplode();
        AssignShipRelayToController();
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
        respawnTimer = shipRespawnTime;
    }
}
