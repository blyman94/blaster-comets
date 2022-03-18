using UnityEngine;

/// <summary>
/// ScriptableObject representation of an string variable.
/// </summary>
[CreateAssetMenu]
public class StringVariable : ScriptableObject
{
    /// <summary>
    /// Signals that the StringVariable's value has changed.
    /// </summary>
    public SimpleDelegate Updated;

    /// <summary>
    /// Value of the StringVariable.
    /// </summary>
    [Tooltip("Value of the StringVariable.")]
    [SerializeField] private string value;

    #region Properties
    /// <summary>
    /// Value of the StringVariable.
    /// </summary>
    public string Value
    {
        get
        {
            return value;
        }
        set
        {
            this.value = value;
            Updated?.Invoke();
        }
    }
    #endregion
}
