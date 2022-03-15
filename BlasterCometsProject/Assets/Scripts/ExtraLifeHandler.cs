using UnityEngine;

/// <summary>
/// Observes the player's score and awards extra lives if the player scores
/// enough points.
/// </summary>
public class ExtraLifeHandler : MonoBehaviour
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
    /// Event to raise when an extra life is gained.
    /// </summary>
    [Header("Events")]
    [Tooltip("Event to raise when an extra life is gained.")]
    [SerializeField] private GameEvent extraLifeEvent;

    /// <summary>
    /// Score the player must reach to get an extra life.
    /// </summary>
    private int targetScore;

    #region MonoBehaviour Methods
    private void OnEnable()
    {
        playerScore.Updated += UpdatePlayerLives;
    }
    private void Start()
    {
        targetScore = settings.GameParameters.PointsPerExtraLife;
    }
    private void OnDisable()
    {
        playerScore.Updated -= UpdatePlayerLives;
    }
    #endregion

    /// <summary>
    /// If the player has scored enough points, add an extra life.
    /// </summary>
    private void UpdatePlayerLives()
    {
        if (playerScore.Value >= targetScore &&
            playerLives.Value < settings.GameParameters.ShipMaxLivesCount)
        {
            playerLives.ApplyChange(1);
            extraLifeEvent.Raise();
            targetScore += settings.GameParameters.PointsPerExtraLife;
        }
    }
}
