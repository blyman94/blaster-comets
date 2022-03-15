using TMPro;
using UnityEngine;

/// <summary>
/// Observes an IntVariable and displays it's value in UI text.
/// </summary>
public class PlayerScoreDisplay : MonoBehaviour
{
    /// <summary>
    /// IntVariable representing the player's current score.
    /// </summary>
    [Header("General")]
    [Tooltip("IntVariable representing the player's current score.")]
    [SerializeField] private IntVariable playerScore;

    /// <summary>
    /// TMPRO object to reflect the value of the player's score.
    /// </summary>
    [Tooltip("TMPRO object to reflect the value of the player's score.")]
    [SerializeField] private TextMeshProUGUI text;

    #region MonoBehaviour Methods
    private void OnEnable()
    {
        if (playerScore != null)
        {
            playerScore.Updated += UpdateDisplay;
        }
    }
    private void OnDisable()
    {
        if (playerScore != null)
        {
            playerScore.Updated -= UpdateDisplay;
        }
    }
    #endregion

    /// <summary>
    /// Updates the UI text to reflect the current value of the observed
    /// variable.
    /// </summary>
    private void UpdateDisplay()
    {
        text.text = playerScore.Value.ToString();
    }
}