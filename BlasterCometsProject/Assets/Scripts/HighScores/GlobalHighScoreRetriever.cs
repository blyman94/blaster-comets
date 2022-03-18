using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Retrieves global high scores and updates the local high scores data.
/// Uses a .php file hosted on BrandonLymanGameDev.com to query a high score 
/// database hosted there. Special thanks to Generic Toast at 
/// https://blog.generistgames.com/creating-a-simple-unity-online-leaderboard/
/// who gives an in-depth, step-by-step tutorial on how to set this up.
/// </summary>
public class GlobalHighScoreRetriever : MonoBehaviour
{
    /// <summary>
    /// URL at which the .php file interfacing with the SQL database is stored.
    /// </summary>
    private const string highScoreURL =
        "https://brandonlymangamedev.com/blaster_comets_global_scoring.php";

    /// <summary>
    /// List of local high scores.
    /// </summary>
    [Tooltip("List of local high scores.")]
    [SerializeField] private LocalHighScores localHighScores;

    /// <summary>
    /// Signals that the local high scores have been updated from the host site.
    /// </summary>
    [Header("Events")]
    [Tooltip("Signals that the local high scores have been updated from " +
        "the host site.")]
    [SerializeField] private GameEvent highScoresUpdated;

    /// <summary>
    /// Were the global high scores updated on start?
    /// </summary>
    private bool calledOnStart = true;

    /// <summary>
    /// Current active coroutine.
    /// </summary>
    private Coroutine currentRoutine;

    /// <summary>
    /// List of score objects derived from the online leaderboard.
    /// </summary>
    private List<Score> scores;

    #region MonoBehaviour Methods
    private void Start()
    {
        LoadGlobalHighScores();

    }
    #endregion

    /// <summary>
    /// Begins the routine for loading the global high score board.
    /// </summary>
    public void LoadGlobalHighScores()
    {
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }
        currentRoutine = StartCoroutine(LoadGlobalHighScoresRoutine());
    }

    /// <summary>
    /// Routine to load the global high score board.
    /// </summary>
    private IEnumerator LoadGlobalHighScoresRoutine()
    {
        yield return RetrieveScoresRoutine();
        if (scores.Count > 0)
        {
            localHighScores.HighScores = scores;
        }

        if (calledOnStart)
        {
            calledOnStart = false;
        }
        else
        {
            highScoresUpdated.Raise();
        }
    }

    /// <summary>
    /// Routine to gather all scores from the leaderboard.
    /// </summary>
    private IEnumerator RetrieveScoresRoutine()
    {
        scores = new List<Score>();

        WWWForm form = new WWWForm();
        form.AddField("retrieve_leaderboard", "true");

        using (UnityWebRequest www = UnityWebRequest.Post(highScoreURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError ||
                www.result == UnityWebRequest.Result.ProtocolError)
            {
                // Debug.Log(www.error);
                yield break;
            }
            else
            {
                // Debug.Log("Successfully retrieved scores!");
                string contents = www.downloadHandler.text;
                using (StringReader reader = new StringReader(contents))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Score entry = new Score();
                        entry.Name = line;
                        try
                        {
                            entry.Value = Int32.Parse(reader.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Debug.Log("Invalid score: " + e);
                            continue;
                        }

                        scores.Add(entry);
                    }
                }
            }
        }
    }
}
