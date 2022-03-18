using UnityEngine;

/// <summary>
/// Determines which end game screen to display based on the player's 
/// final score.
/// </summary>

public class GameEnder : MonoBehaviour
{
    /// <summary>
    /// Current high score data.
    /// </summary>
    [Tooltip("Current high score data.")]
    [SerializeField] private LocalHighScores localHighScores;

    /// <summary>
    /// IntVariable representing the player's current score.
    /// </summary>
    [Header("Player Info")]
    [Tooltip("IntVariable representing the player's current score.")]
    [SerializeField] private IntVariable playerScore;

    /// <summary>
    /// IntVariable representing the player's current score.
    /// </summary>
    [Tooltip("IntVariable representing the player's current score.")]
    [SerializeField] private StringVariable playerInitials;

    /// <summary>
    /// Group to display when a new high score is not achieved.
    /// </summary>
    [Header("End Game Screens")]
    [Tooltip("Group to display when a new high score is not achieved.")]
    [SerializeField] private CanvasGroupRevealer gameOverGroup;

    /// <summary>
    /// Group to display when a new high score is achieved.
    /// </summary>
    [Tooltip("Group to display when a new high score is achieved.")]
    [SerializeField] private CanvasGroupRevealer newHighScoreGroup;

    /// <summary>
    /// Determines which end game screen to display based on the player's 
    /// final score.
    /// </summary>
    public void DisplayEndGameScreen()
    {
        playerInitials.Value = "";
        int lowestScoreIndex = localHighScores.HighScores.Count - 1;
        if (playerScore.Value >= 
            localHighScores.HighScores[lowestScoreIndex].Value)
        {
            newHighScoreGroup.ShowGroup();
            return;
        }
        gameOverGroup.ShowGroup();
    }
}
