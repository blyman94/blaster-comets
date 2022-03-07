using UnityEngine;

/// <summary>
/// Spawns bogeys with AI to actively attack the player.
/// </summary>
public class BogeySpawner : MonoBehaviour
{
    /// <summary>
    /// BogeyController object to which spawned ships will be assigned.
    /// </summary>
    [Header("General")]
    [Tooltip("BogeyController object to which spawned ships will be assigned.")]
    [SerializeField] private BogeyController bogeyController;

    /// <summary>
    /// Pool from which destroyed ship's explosions will be spawned from.
    /// </summary>
    [Tooltip("Pool from which destroyed ship's explosions will be " +
        "spawned from.")]
    [SerializeField] private ExplosionPool explosionPool;

    /// <summary>
    /// Main Camera used to determine random spawn positions.
    /// </summary>

    [Tooltip("Main Camera used to determine random spawn positions.")]
    [SerializeField] private Camera mainCamera;

    /// <summary>
    /// IntVariable representing the player's current score.
    /// </summary>
    [Tooltip("IntVariable representing the player's current score.")]
    [SerializeField] private IntVariable playerScore;

    /// <summary>
    /// Prefab representing the large bogey.
    /// </summary>
    [Header("Bogey Prefabs")]
    [Tooltip("Prefab representing the large bogey.")]
    [SerializeField] private GameObject bogeyLargePrefab;

    /// <summary>
    /// Prefab representing the small bogey.
    /// </summary>
    [Tooltip("Prefab representing the small bogey.")]
    [SerializeField] private GameObject bogeySmallPrefab;

    /// <summary>
    /// Speed at which the large bogey will move.
    /// </summary>
    [Header("Bogey Parameters")]
    [Tooltip("Speed at which the large bogey will move.")]
    [SerializeField] private float bogeyMoveSpeed;

    /// <summary>
    /// Maximum time between bogey spawns.
    /// </summary>
    [Tooltip("Maximum time between bogey spawns.")]
    [SerializeField] private float bogeyMaxSpawnDelay;

    /// <summary>
    /// Minimum time between bogey spawns.
    /// </summary>
    [Tooltip("Minimum time between bogey spawns.")]
    [SerializeField] private float bogeyMinSpawnDelay;

    /// <summary>
    /// Exploder module for the large bogey object.
    /// </summary>
    private Exploder bogeyLargeExploder;

    /// <summary>
    /// GameObject representing the instance of a large bogey.
    /// </summary>
    private GameObject bogeyLargeObject;

    /// <summary>
    /// Explode module for the small bogey object.
    /// </summary>
    private Exploder bogeySmallExploder;

    /// <summary>
    /// GameObject representing the instance of a small bogey.
    /// </summary>
    private GameObject bogeySmallObject;

    /// <summary>
    /// Can this spawner spawn large bogeys?
    /// </summary>
    private bool canSpawnLargeBogeys = false;

    /// <summary>
    /// Can this spawner spawn small bogeys?
    /// </summary>
    private bool canSpawnSmallBogeys = false;

    #region MonoBehaviour Methods
    private void Awake()
    {
        InitializeBogeyObjects();
        ConfigureBogeyExploders();
    }
    private void OnEnable()
    {
        SubscribeToDelegates();
    }
    private void OnDisable()
    {
        UnsubscribeFromDelegates();
    }
    #endregion

    /// <summary>
    /// Selects a bogey based on what the spawner can currently spawn and 
    /// spawns a bogey.
    /// </summary>
    private void ChooseBogeyToSpawn()
    {
        if (!canSpawnLargeBogeys && !canSpawnSmallBogeys)
        {
            return;
        }
        else if (canSpawnLargeBogeys && !canSpawnSmallBogeys)
        {
            SpawnBogey(true);
        }
        else if (canSpawnLargeBogeys && canSpawnSmallBogeys)
        {
            bool spawnLarge = Random.Range(0.0f, 1.0f) >= 0.5;
            SpawnBogey(spawnLarge);
        }
        else if (!canSpawnLargeBogeys && canSpawnSmallBogeys)
        {
            SpawnBogey(false);
        }
    }

    /// <summary>
    /// Configures the exploder modules of each bogey object.
    /// </summary>
    private void ConfigureBogeyExploders()
    {
        bogeyLargeExploder = bogeyLargeObject.GetComponent<Exploder>();
        bogeyLargeExploder.ExplosionPool = explosionPool;

        bogeySmallExploder = bogeySmallObject.GetComponent<Exploder>();
        bogeySmallExploder.ExplosionPool = explosionPool;
    }

    /// <summary>
    /// Determines which types of bogeys can be spawned based on player's
    /// current score.
    /// </summary>
    private void HandleBogeySpawn()
    {
        if (playerScore.Value < 2000)
        {
            return;
        }
        else if (!canSpawnLargeBogeys && playerScore.Value >= 2000 &&
            playerScore.Value <= 40000)
        {
            canSpawnLargeBogeys = true;
            float randomTime =
                Random.Range(bogeyMinSpawnDelay, bogeyMaxSpawnDelay);
            Invoke("ChooseBogeyToSpawn", randomTime);
        }
        else if (!canSpawnSmallBogeys && playerScore.Value >= 10000)
        {
            canSpawnSmallBogeys = true;
        }
        else if (playerScore.Value > 40000 && canSpawnLargeBogeys)
        {
            canSpawnLargeBogeys = false;
        }
    }

    /// <summary>
    /// Instantiates the large and small bogey objects.
    /// </summary>
    private void InitializeBogeyObjects()
    {
        bogeyLargeObject = Instantiate(bogeyLargePrefab, Vector3.zero,
                    Quaternion.identity);
        bogeyLargeObject.SetActive(false);

        bogeySmallObject = Instantiate(bogeySmallPrefab, Vector3.zero,
            Quaternion.identity);
        bogeySmallObject.SetActive(false);
    }

    /// <summary>
    /// Releases control of and deactivates the active bogey. Should be called
    /// when the bogey finishes a retreat or is destroyed.
    /// </summary>
    private void OnBogeyLifeEnd()
    {
        if (bogeyController != null)
        {
            bogeyController.RelayToControl = null;
        }
        if (bogeyLargeObject != null)
        {
            bogeyLargeObject.SetActive(false);
        }
        if (bogeySmallObject != null)
        {
            bogeySmallObject.SetActive(false);
        }

        float randomTime =
            Random.Range(bogeyMinSpawnDelay, bogeyMaxSpawnDelay);
        Invoke("ChooseBogeyToSpawn", randomTime);
    }

    /// <summary>
    /// Subscribes to delegates in the playerScore, bogey controller, and bogey 
    /// exploder modules.
    /// </summary>
    private void SubscribeToDelegates()
    {
        if (playerScore != null)
        {
            playerScore.Updated += HandleBogeySpawn;
        }
        if (bogeyController != null)
        {
            bogeyController.Retreated += OnBogeyLifeEnd;
        }
        if (bogeyLargeExploder != null)
        {
            bogeyLargeExploder.EntityExploded += OnBogeyLifeEnd;
        }
        if (bogeySmallExploder != null)
        {
            bogeySmallExploder.EntityExploded += OnBogeyLifeEnd;
        }
    }

    private void SpawnBogey(bool isLargeBogey)
    {
        GameObject bogeyObject =
            isLargeBogey ? bogeyLargeObject : bogeySmallObject;

        GameObject bogeyPrefab =
            isLargeBogey ? bogeyLargePrefab : bogeySmallPrefab;

        Exploder bogeyExploder =
            isLargeBogey ? bogeyLargeExploder : bogeySmallExploder;

        bogeyObject.transform.position =
                mainCamera.GetRandomPositionOnBounds();
        bogeyObject.SetActive(true);

        bogeyController.IsLargeBogey = isLargeBogey;
        bogeyController.MoveSpeed =
            isLargeBogey ? bogeyMoveSpeed : bogeyMoveSpeed * 1.5f;

        bogeyExploder.Unexplode();

        CommandRelay bogeyRelay = bogeyObject.GetComponent<CommandRelay>();
        if (bogeyRelay != null)
        {
            bogeyController.RelayToControl = bogeyRelay;
        }
    }

    /// <summary>
    /// Unsubscribes from delegates in the playerScore, bogey controller, and 
    /// bogey exploder modules.
    /// </summary>
    private void UnsubscribeFromDelegates()
    {
        if (playerScore != null)
        {
            playerScore.Updated -= HandleBogeySpawn;
        }
        if (bogeyController != null)
        {
            bogeyController.Retreated -= OnBogeyLifeEnd;
        }
        if (bogeyLargeExploder != null)
        {
            bogeyLargeExploder.EntityExploded -= OnBogeyLifeEnd;
        }
        if (bogeySmallExploder != null)
        {
            bogeySmallExploder.EntityExploded -= OnBogeyLifeEnd;
        }
    }
}
