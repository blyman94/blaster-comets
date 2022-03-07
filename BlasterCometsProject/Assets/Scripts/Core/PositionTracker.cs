using UnityEngine;

/// <summary>
/// Stores the Vector2 position of a GameObject in the specified 
/// Vector2Variable.
/// </summary>
public class PositionTracker : MonoBehaviour
{
    /// <summary>
    /// Vector2Variable in which to store the position.
    /// </summary>
    [Tooltip("Vector2Variable in which to store the position.")]
    [SerializeField] private Vector2Variable positionVariable;

    #region MonoBehaviour Methods
    private void Update()
    {
        Vector2 currentPosition = transform.position;
        if (positionVariable.Value != currentPosition)
        {
            positionVariable.Value = currentPosition;
        }
    }
    #endregion
}
