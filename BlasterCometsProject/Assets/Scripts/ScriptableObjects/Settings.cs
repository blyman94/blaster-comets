using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// Contains the game's current settings.
/// </summary>
[CreateAssetMenu]
public class Settings : ScriptableObject
{
    /// <summary>
    /// Current parameters of the game.
    /// </summary>
    [Tooltip("Current parameters of the game.")]
    public GameParameters GameParameters;

    /// <summary>
    /// Naive Restart Implementation.
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
