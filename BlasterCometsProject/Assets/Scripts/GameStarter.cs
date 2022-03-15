using UnityEngine;

/// <summary>
/// Sets the game to an initial state and begins gameplay.
/// </summary>
public class GameStarter : MonoBehaviour
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
    /// IntVariable representing the player's current score.
    /// </summary>
    [Tooltip("IntVariable representing the player's current score.")]
    [SerializeField] private IntVariable playerScore;

    /// <summary>
    /// Meteoroid spawner used to spawn meteoroids.
    /// </summary>
    [Header("Spawners")]
    [Tooltip("Meteoroid spawner used to spawn meteoroids.")]
    [SerializeField] private MeteoroidSpawner meteoroidSpawner;

    /// <summary>
    /// Ship spawner used to spawn the player ship.
    /// </summary>
    [Tooltip("Ship spawner used to spawn the player ship.")]
    [SerializeField] private ShipSpawner shipSpawner;

    /// <summary>
    /// Event raised when the game starts.
    /// </summary>
    [Header("Events")]
    [Tooltip("Event raised when the game starts.")]
    [SerializeField] private GameEvent gameStartEvent;

    /// <summary>
    /// Begins gameplay.
    /// </summary>
    public void StartGame()
    {
        shipSpawner.SpawnNewShip();
        playerScore.Value = 0;
        playerLives.Value = settings.GameParameters.ShipStartingLives;
        gameStartEvent.Raise();
        meteoroidSpawner.SpawnLargeMeteoroids();
    }
}