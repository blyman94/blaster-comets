using UnityEngine;

/// <summary>
/// Receives commands from a control source and delegates them to the
/// appropriate component of a ship object..
/// </summary>
public class ShipCommandRelay : CommandRelay
{
    /// <summary>
    /// Hyperspace component allowing the ship to teleport within the 
    /// screen bounds.
    /// </summary>
    [Header("Movement")]
    [Tooltip("Hyperspace component allowing the ship to teleport " +
        "within the screen bounds.")]
    [SerializeField] private Hyperspace hyperspace;

    /// <summary>
    /// Rotator component allowing the ship to rotate.
    /// </summary>
    [Tooltip("Rotator component allowing the ship to rotate.")]
    [SerializeField] private Rotator rotator;

    /// <summary>
    /// Thuster component allowing the ship to move forward.
    /// </summary>
    [Tooltip("Thuster component allowing the ship to move forward.")]
    [SerializeField] private Thruster thruster;

    #region CommandRelay Methods
    public override void ResetRelay()
    {
        base.ResetRelay();
        StopRotationLeft();
        StopRotationRight();
        StopThrusterImmediate();
    }
    public override void StartFire()
    {
        if (!hyperspace.InHyperspace)
        {
            weapon.IsFiring = true;
        }
    }
    #endregion

    /// <summary>
    /// Sends the controlled ship into Hyperspace.
    /// </summary>
    public void EnterHyperspace()
    {
        ResetRelay();
        hyperspace.EnterHyperspace();
    }

    /// <summary>
    /// Starts rotating the ship counter-clockwise.
    /// </summary>
    public void StartRotationLeft()
    {
        if (!hyperspace.InHyperspace)
        {
            rotator.RotateLeft = true;
            rotator.RotateRight = false;
        }
    }

    /// <summary>
    /// Starts rotating the ship clockwise.
    /// </summary>
    public void StartRotationRight()
    {
        if (!hyperspace.InHyperspace)
        {
            rotator.RotateLeft = false;
            rotator.RotateRight = true;
        }
    }

    /// <summary>
    /// Starts moving the ship in its upward direction.
    /// </summary>
    public void StartThruster()
    {
        if (!hyperspace.InHyperspace)
        {
            thruster.gameObject.SetActive(true);
            thruster.Active = true;
        }
    }

    /// <summary>
    /// Stops the ship from rotating counter-clockwise.
    /// </summary>
    public void StopRotationLeft()
    {
        rotator.RotateLeft = false;
    }

    /// <summary>
    /// Stops the ship from rotating clockwise.
    /// </summary>
    public void StopRotationRight()
    {
        rotator.RotateRight = false;
    }

    /// <summary>
    /// Stops moving the ship in its upward direction.
    /// </summary>
    public void StopThruster()
    {
        thruster.Active = false;
    }

    /// <summary>
    /// Immediately stops moving the ship in its upward direction.
    /// </summary>
    public void StopThrusterImmediate()
    {
        thruster.gameObject.SetActive(false);
    }
}
