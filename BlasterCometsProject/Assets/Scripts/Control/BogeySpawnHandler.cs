using UnityEngine;

/// <summary>
/// Determines if a large or small bogey should be spawned, then signals 
/// referenced spawners to spawn the appropriate bogey.
/// </summary>
public class BogeySpawnHandler : MonoBehaviour
{
    /// <summary>
    /// The game's settings.
    /// </summary>
    [Tooltip("The game's settings.")]
    [SerializeField] private Settings settings;

    /// <summary>
    /// BogeyController object to which spawned ships will be assigned.
    /// </summary>
    [Header("General")]
    [Tooltip("BogeyController object to which spawned ships will be assigned.")]
    [SerializeField] private BogeyController bogeyController;

    /// <summary>
    /// IntVariable representing the player's current score.
    /// </summary>
    [Tooltip("IntVariable representing the player's current score.")]
    [SerializeField] private IntVariable playerScore;

    /// <summary>
    /// The large bogey spawner.
    /// </summary>
    [Header("Spawners")]
    [Tooltip("The large bogey spawner.")]
    [SerializeField] private BogeySpawner bogeySpawnerLarge;

    /// <summary>
    /// The large bogey spawner.
    /// </summary>
    [Tooltip("The small bogey spawner.")]
    [SerializeField] private BogeySpawner bogeySpawnerSmall;

    /// <summary>
    /// Are large bogeys spawnable?
    /// </summary>
    private bool canSpawnLargeBogeys = false;

    /// <summary>
    /// Are small bogeys spawnable?
    /// </summary>
    private bool canSpawnSmallBogeys = false;

    #region MonoBehaviour Methods
    private void OnEnable()
    {
        playerScore.Updated += UpdateBogeyAvailability;
        bogeyController.Retreated += OnBogeyLifeEnd;
        bogeySpawnerLarge.BogeyDown += OnBogeyLifeEnd;
        bogeySpawnerSmall.BogeyDown += OnBogeyLifeEnd;
    }
    private void OnDisable()
    {
        playerScore.Updated -= UpdateBogeyAvailability;
        bogeyController.Retreated -= OnBogeyLifeEnd;
        bogeySpawnerLarge.BogeyDown -= OnBogeyLifeEnd;
        bogeySpawnerSmall.BogeyDown -= OnBogeyLifeEnd;
    }
    #endregion

    /// <summary>
    /// Selects a bogey based on what is currently available to spawn.
    /// </summary>
    private void ChooseBogeyToSpawn()
    {
        if (!canSpawnLargeBogeys && !canSpawnSmallBogeys)
        {
            return;
        }
        else if (canSpawnLargeBogeys && !canSpawnSmallBogeys)
        {
            bogeyController.MoveSpeed = settings.GameParameters.BogeyMoveSpeed;
            bogeyController.RelayToControl = bogeySpawnerLarge.SpawnBogey();
        }
        else if (canSpawnLargeBogeys && canSpawnSmallBogeys)
        {
            bool spawnLarge = Random.Range(0.0f, 1.0f) >= 0.5;
            if (spawnLarge)
            {
                bogeyController.MoveSpeed = 
                    settings.GameParameters.BogeyMoveSpeed;
                bogeyController.RelayToControl = bogeySpawnerLarge.SpawnBogey();
            }
            else
            {
                bogeyController.MoveSpeed = 
                    settings.GameParameters.BogeyMoveSpeed * 1.5f;
                bogeyController.RelayToControl = bogeySpawnerSmall.SpawnBogey();
            }
        }
        else
        {
            bogeyController.MoveSpeed = 
                settings.GameParameters.BogeyMoveSpeed * 1.5f;
            bogeyController.RelayToControl = bogeySpawnerSmall.SpawnBogey();
        }
    }

    /// <summary>
    /// Releases control of active bogey. Should be called when the bogey
    /// retreats fully or is destroyed.
    /// </summary>
    private void OnBogeyLifeEnd()
    {
        bogeyController.RelayToControl.gameObject.SetActive(false);
        bogeyController.RelayToControl = null;

        float randomTime =
                Random.Range(settings.GameParameters.BogeySpawnDelayRange.x,
                settings.GameParameters.BogeySpawnDelayRange.y);
        Invoke("ChooseBogeyToSpawn", randomTime);
    }

    /// <summary>
    /// Determines which types of bogeys can be spawned based on player's
    /// current score.
    /// </summary>
    private void UpdateBogeyAvailability()
    {
        if (playerScore.Value < 
            settings.GameParameters.BogeyLargeSpawnThreshold)
        {
            canSpawnLargeBogeys = false;
            canSpawnSmallBogeys = false;
            return;
        }
        else if (!canSpawnLargeBogeys &&
            playerScore.Value >= settings.GameParameters.BogeyLargeSpawnThreshold &&
            playerScore.Value <= settings.GameParameters.BogeyOnlySmallSpawnThreshold)
        {
            canSpawnLargeBogeys = true;
            float randomTime =
                Random.Range(settings.GameParameters.BogeySpawnDelayRange.x,
                settings.GameParameters.BogeySpawnDelayRange.y);
            Invoke("ChooseBogeyToSpawn", randomTime);
        }
        else if (!canSpawnSmallBogeys &&
            playerScore.Value >= settings.GameParameters.BogeySmallSpawnThreshold)
        {
            canSpawnSmallBogeys = true;
        }
        else if (playerScore.Value > settings.GameParameters.BogeyOnlySmallSpawnThreshold &&
            canSpawnLargeBogeys)
        {
            canSpawnLargeBogeys = false;
        }
    }
}
