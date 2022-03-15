using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a button on a menu.
/// </summary>
public class MenuButton : MonoBehaviour
{
    /// <summary>
    /// Carot indicator on the left side of the button.
    /// </summary>
    [Tooltip("Carot indicator on the left side of the button.")]
    [SerializeField] private Image carotLeft;

    /// <summary>
    /// Carot indicator on the right side of the button.
    /// </summary>
    [Tooltip("Carot indicator on the right side of the button.")]
    [SerializeField] private Image carotRight;

    /// <summary>
    /// Original color of the carots.
    /// </summary>
    private Color carotColor;

    #region MonoBehaviour Methods
    private void Start()
    {
        if (carotLeft != null)
        {
            carotColor = carotLeft.color;
        }
        HideCarots();
    }
    #endregion

    /// <summary>
    /// Shows the carots on either side of the button, indicating the button is
    /// currently selected or being hovered over.
    /// </summary>
    public void ShowCarots()
    {
        carotLeft.color =
            new Color(carotColor.r, carotColor.g, carotColor.b, 1.0f);
        carotRight.color =
            new Color(carotColor.r, carotColor.g, carotColor.b, 1.0f);
    }

    /// <summary>
    /// Hide the carots on either side of the button, indicating the button is
    /// not currently selected and not being hovered over.
    /// </summary>
    public void HideCarots()
    {
        carotLeft.color =
            new Color(carotColor.r, carotColor.g, carotColor.b, 0.0f);
        carotRight.color =
            new Color(carotColor.r, carotColor.g, carotColor.b, 0.0f);
    }
}