using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Updates the GameHUD menu to reflect the number of lives the player has 
/// remaining.
/// </summary>
public class PlayerLivesDisplay : MonoBehaviour
{
    /// <summary>
    /// Array of images representing how many lives the player has.
    /// </summary>
    [Header("General")]
    [Tooltip("Array of images representing how many lives the player has.")]
    [SerializeField] private Image[] playerLifeImages;

    /// <summary>
    /// IntVariable representing the number of lives the player has remaining.
    /// </summary>
    [Tooltip("IntVariable representing the number of lives the player has " +
        "remaining.")]
    [SerializeField] private IntVariable playerLives;

    #region MonoBehaviour Methods
    private void OnEnable()
    {
        playerLives.Updated += UpdateLivesDisplay;
    }
    private void OnDisable()
    {
        playerLives.Updated -= UpdateLivesDisplay;
    }
    #endregion

    /// <summary>
    /// Updates the GameHUD menu to reflect the number of lives the player has 
    /// remaining.
    /// </summary>
    private void UpdateLivesDisplay()
    {
        for (int i = 0; i < playerLives.Value; i++)
        {
            if (!playerLifeImages[i].gameObject.activeInHierarchy)
            {
                playerLifeImages[i].gameObject.SetActive(true);
            }
        }
        for (int j = playerLives.Value; j < playerLifeImages.Length; j++)
        {
            if (playerLifeImages[j].gameObject.activeInHierarchy)
            {
                playerLifeImages[j].gameObject.SetActive(false);
            }
        }
    }
}
