using UnityEngine;

/// <summary>
/// ScriptableObject representation of a Vector2 variable.
/// </summary>
[CreateAssetMenu]
public class Vector2Variable : ScriptableObject
{
    /// <summary>
    /// Signals that the Vector2Variable's value has changed.
    /// </summary>
    public SimpleDelegate Updated;

    /// <summary>
    /// Value of the Vector2Variable.
    /// </summary>
    [Tooltip("Value of the Vector2Variable.")]
    [SerializeField] private Vector2 value;

    #region Properties
    /// <summary>
    /// Value of the Vector2Variable.
    /// </summary>
    public Vector2 Value
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
    /// Changes the value of the Vector2Variable by a passed delta.
    /// </summary>
    /// <param name="delta">Amount by which to change the value.</param>
    public void ApplyChange(Vector2 delta)
    {
        Value += delta;
    }
}
