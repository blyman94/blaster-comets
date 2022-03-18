using UnityEngine;
using TMPro;

/// <summary>
/// Displays the local high scores in the assigned TextMeshProUGUI objects.
/// </summary>
public class HighScoreDisplay : MonoBehaviour
{
    /// <summary>
    /// List of local high scores.
    /// </summary>
    [Tooltip("List of local high scores.")]
    [SerializeField] private LocalHighScores localHighScores;

    /// <summary>
    /// Array of TextMeshPro objects that will display the leaderboard names.
    /// </summary>
    [Header("High Score Display Elements")]
    [Tooltip("Array of TextMeshPro objects that will display the leaderboard" +
        "names.")]
    [SerializeField] private TextMeshProUGUI[] namesTextUI;

    /// <summary>
    /// Array of TextMeshPro objects that will display the leaderboard scores.
    /// </summary>
    [Tooltip("Array of TextMeshPro objects that will display the leaderboard" +
        "scores.")]
    [SerializeField] private TextMeshProUGUI[] scoresTextUI;

    /// <summary>
    /// Displays the local high scores in the assigned TextMeshProUGUI objects.
    /// </summary>
    public void DisplayHighScores()
    {
        for (int i = 0; i < 10; i++)
        {
            namesTextUI[i].text = localHighScores.HighScores[i].Name;
            scoresTextUI[i].text = 
                localHighScores.HighScores[i].Value.ToString();
        }
    }
}
