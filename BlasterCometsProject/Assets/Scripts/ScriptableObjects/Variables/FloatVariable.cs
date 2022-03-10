using UnityEngine;

/// <summary>
/// ScriptableObject representation of an integer variable.
/// </summary>
[CreateAssetMenu]
public class FloatVariable : ScriptableObject
{
    /// <summary>
    /// Signals that the IntVariable's value has changed.
    /// </summary>
    public SimpleDelegate Updated;

    /// <summary>
    /// Value of the IntVariable.
    /// </summary>
    [Tooltip("Value of the IntVariable.")]
    [SerializeField] private float value;

    #region Properties
    /// <summary>
    /// Value of the IntVariable.
    /// </summary>
    public float Value
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

    /// <summary>
    /// Changes the value of the int variable by a passed delta.
    /// </summary>
    /// <param name="delta">Amount by which to change the value.</param>
    public void ApplyChange(float delta)
    {
        Value += delta;
    }
}