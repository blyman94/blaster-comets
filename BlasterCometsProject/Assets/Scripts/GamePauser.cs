using UnityEngine;

/// <summary>
/// Allows the game to be paused.
/// </summary> 
public class GamePauser : MonoBehaviour
{
    /// <summary>
    /// Event to be raised when the game is paused.
    /// </summary>
    [Header("Events")]
    [Tooltip("Event to be raised when the game is paused.")]
    [SerializeField] private GameEvent gamePauseEvent;

    /// <summary>
    /// Event to be raised when the game is unpaused.
    /// </summary>
    [Tooltip("Event to be raised when the game is unpaused.")]
    [SerializeField] private GameEvent gameUnpauseEvent;

    #region Properties
    /// <summary>
    /// Is the GamePauser able to toggle the pause state?
    /// </summary>
    public bool CanTogglePause { get; set; } = false;

    /// <summary>
    /// Is the game currently paused?
    /// </summary>
    public bool IsPaused { get; set; } = false;
    #endregion

    /// <summary>
    /// Toggles the pause state of the game.
    /// </summary>
    public void ToggleGamePause()
    {
        if (CanTogglePause)
        {
            if (IsPaused)
            {
                IsPaused = false;
                Time.timeScale = 1.0f;
                gameUnpauseEvent.Raise();
            }
            else
            {
                IsPaused = true;
                Time.timeScale = 0.0f;
                gamePauseEvent.Raise();
            }
        }
    }
}
