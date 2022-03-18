using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Updates local and global high scores with the user's name and achieved
/// score.
/// </summary>
public class HighScoreUpdater : MonoBehaviour
{
    /// <summary>
    /// URL at which the .php file interfacing with the SQL database is stored.
    /// </summary>
    private const string highScoreURL =
        "https://brandonlymangamedev.com/blaster_comets_global_scoring.php";

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
    /// Updates the local high score records with the player's new score.
    /// </summary>
    public void UpdateLocalHighScores()
    {
        GenerateScore(out Score score);
        localHighScores.InsertNewScore(score);
    }

    /// <summary>
    /// Starts the PostScore Routine with the current player's name and score.
    /// </summary>
    public void UpdateGlobalHighScores()
    {
        GenerateScore(out Score score);
        StartCoroutine(PostScoreRoutine(score.Name,
            score.Value));
    }

    /// <summary>
    /// Generates a Score object representing the player's achieved score.
    /// </summary>
    private void GenerateScore(out Score score)
    {
        score = new Score();
        if (playerInitials.Value == "")
        {
            score.Name = "N/A";
        }
        else
        {
            score.Name = playerInitials.Value;
        }
        score.Value = playerScore.Value;
    }

    /// <summary>
    /// Routine to post a score to the online leaderboard.
    /// </summary>
    /// <param name="name">Name to post to the leaderboard.</param>
    /// <param name="score">Score to post to the leaderboard.</param>
    private IEnumerator PostScoreRoutine(string name, int score)
    {
        // Remove white space from name.
        string processedName =
            String.Concat(name.Where(c => !Char.IsWhiteSpace(c)));

        // Ensure name is uppercase.
        processedName = processedName.ToUpper();

        // Do not post name if it is empty.
        if (String.IsNullOrEmpty(processedName))
        {
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("post_leaderboard", "true");
        form.AddField("name", processedName);
        form.AddField("score", score);

        using (UnityWebRequest www = UnityWebRequest.Post(highScoreURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError ||
                www.result == UnityWebRequest.Result.ProtocolError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("Successfully posted score!");
            }
        }
    }
}
