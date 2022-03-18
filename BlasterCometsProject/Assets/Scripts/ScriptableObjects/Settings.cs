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
    /// Current color palette of the game.
    /// </summary>
    [Tooltip("Current color palette of the game.")]
    public ColorPalette CurrentColorPalette;

    /// <summary>
    /// Naive Restart Implementation.
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
