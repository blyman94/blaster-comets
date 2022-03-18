using UnityEngine;

/// <summary>
/// Adjust the small bogey's fire angle based on the player's score.
/// </summary>
public class FireAngleAdjuster : MonoBehaviour
{
    /// <summary>
    /// The game's settings.
    /// </summary>
    [Tooltip("The game's settings.")]
    [SerializeField] private Settings settings;

    /// <summary>
    /// When targeting the ship, the angle of the cone the bogey fires in.
    /// </summary>
    [Tooltip("When targeting the ship, the angle of the cone the bogey " +
        "fires in.")]
    [SerializeField] private FloatVariable bogeyFireAngle;

    /// <summary>
    /// IntVariable representing the player's current score.
    /// </summary>
    [Tooltip("IntVariable representing the player's current score.")]
    [SerializeField] private IntVariable playerScore;

    /// <summary>
    /// When targeting the ship, the initial angle of the cone the bogey fires 
    /// in.
    /// </summary>
    private float bogeyFireAngleStart;

    /// <summary>
    /// When targeting the ship, the initial angle of the cone the bogey fires 
    /// in.
    /// </summary>
    private float bogeyFireAngleMinimum;

    #region MonoBehaviour Methods
    private void Awake()
    {
        bogeyFireAngleMinimum = settings.GameParameters.BogeyFireAngleMinimum;
        bogeyFireAngleStart = settings.GameParameters.BogeyFireAngleStart;
        bogeyFireAngle.Value = bogeyFireAngleStart;
    }
    private void OnEnable()
    {
        playerScore.Updated += UpdateBogeyFireAngle;
    }
    private void OnDisable()
    {
        playerScore.Updated -= UpdateBogeyFireAngle;
    }
    #endregion

    /// <summary>
    /// Increases the accuracy of the bogey's fire angle as the player's score 
    /// increases.
    /// </summary>
    private void UpdateBogeyFireAngle()
    {
        if (playerScore.Value >=
            settings.GameParameters.BogeySmallSpawnThreshold)
        {
            float accuracyIncrease = bogeyFireAngleStart *
                ((float)playerScore.Value /
                settings.GameParameters.BogeyFireAngleMinimumScore);

            bogeyFireAngle.Value = bogeyFireAngleStart - accuracyIncrease +
                bogeyFireAngleMinimum;
        }
    }
}
