using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Observes an IntVariable and displays it's value in UI text.
/// </summary>
public class IntVariableDisplay : MonoBehaviour
{
    /// <summary>
    /// IntVariable whose value will be displayed in UI text.
    /// </summary>
    [Header("General")]
    [Tooltip("IntVariable whose value will be displayed in UI text.")]
    [SerializeField] private IntVariable observedVariable;

    /// <summary>
    /// UI text object to reflect the value of the observed variable.
    /// </summary>
    [Tooltip("UI text object to reflect the value of the observed variable.")]
    [SerializeField] private Text text;

    /// <summary>
    /// Should the IntVariable be reset to 0 at the start?
    /// </summary>
    [Tooltip("Should the IntVariable be reset to 0 at the start?")]
    [SerializeField] private bool resetOnStart;

    /// <summary>
    /// What should the value of the IntVariable be when it is reset?
    /// </summary>
    [Tooltip("What should the value of the IntVariable be when it is reset?")]
    [SerializeField] private int resetValue;

    #region MonoBehaviour Methods
    private void OnEnable()
    {
        if (observedVariable != null)
        {
            observedVariable.Updated += UpdateText;
        }
    }
    private void Start()
    {
        if (resetOnStart)
        {
            ResetObservedVariable();
        }
    }
    private void OnDisable()
    {
        if (observedVariable != null)
        {
            observedVariable.Updated -= UpdateText;
        }
    }
    #endregion

    /// <summary>
    /// Resets the value of the observed variable to 0.
    /// </summary>
    private void ResetObservedVariable()
    {
        if (observedVariable != null)
        {
            observedVariable.Value = resetValue;
        }
    }

    /// <summary>
    /// Updates the UI text to reflect the current value of the observed
    /// variable.
    /// </summary>
    private void UpdateText()
    {
        text.text = observedVariable.Value.ToString();
    }
}
