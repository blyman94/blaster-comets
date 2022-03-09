using UnityEngine;

/// <summary>
/// Rotates a 2D GameObject in the counterclockwise (left) or clockwise (right)
/// directions.
/// </summary>
public class Rotator : MonoBehaviour
{
    /// <summary>
    /// Rigidbody2D component of the GameObject to be rotated.
    /// </summary>
    [Header("General")]
    [Tooltip("Rigidbody2D component of the GameObject to be rotated.")]
    [SerializeField] private new Rigidbody2D rigidbody2D;

    #region Properties
    /// <summary>
    /// Should the GameObject be rotating counter-clockwise?
    /// </summary>
    public bool RotateLeft { get; set; } = false;

    /// <summary>
    /// Should the GameObject be rotating clockwise?
    /// </summary>
    public bool RotateRight { get; set; } = false;

    /// <summary>
    /// Determines how quickly the GameObject rotates.
    /// </summary>
    public float RotationSpeed { get; set; }
    #endregion

    #region MonoBehaviour Methods
    private void FixedUpdate()
    {
        if (rigidbody2D != null)
        {
            if (RotateLeft)
            {
                rigidbody2D.rotation += RotationSpeed * Time.fixedDeltaTime;
            }
            
            if (RotateRight)
            {
                rigidbody2D.rotation -= RotationSpeed * Time.fixedDeltaTime;
            }
        }
    }
    #endregion
}
