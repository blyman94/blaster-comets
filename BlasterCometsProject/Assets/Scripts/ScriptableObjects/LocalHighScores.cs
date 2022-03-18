using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores the local high scores.
/// </summary>
[CreateAssetMenu]
public class LocalHighScores : ScriptableObject
{
    /// <summary>
    /// List of local high scores.
    /// </summary>
    [Tooltip("List of local high scores.")]
    public List<Score> HighScores;

    /// <summary>
    /// Inserts a new high score into the local scoreboard.
    /// </summary>
    /// <param name="scoreToInsert">The score to be added to the local
    /// scoreboard</param>
    public void InsertNewScore(Score scoreToInsert)
    {
        for (int i = 0; i < HighScores.Count; i++)
        {
            if (scoreToInsert.Value <= HighScores[i].Value)
            {
                continue;
            }
            else
            {
                HighScores.Insert(i, scoreToInsert);
                return;
            }
        }
    }
}
