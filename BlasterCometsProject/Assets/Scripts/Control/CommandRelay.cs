using UnityEngine;

/// <summary>
/// Receives commands from a control source and delegates them to the
/// appropriate component.
/// </summary>
public class CommandRelay : MonoBehaviour
{
    /// <summary>
    /// Rigidbody2D of the attached GameObject;
    /// </summary>
    public Rigidbody2D Rigidbody2D;

    /// <summary>
    /// Weapon component allowing the GameObject to fire projectiles.
    /// </summary>
    [Header("Combat")]
    [Tooltip("Weapon component allowing the GameObject to fire projectiles.")]
    [SerializeField] private Weapon weapon;

    /// <summary>
    /// Rotator component allowing the GameObject to rotate.
    /// </summary>
    [Header("Movement")]
    [Tooltip("Rotator component allowing the GameObject to rotate.")]
    [SerializeField] private Rotator rotator;

    /// <summary>
    /// Thuster component allowing the GameObject to move forward.
    /// </summary>
    [Tooltip("Thuster component allowing the GameObject to move forward.")]
    [SerializeField] private Thruster thruster;

    #region Properties

    /// <summary>
    /// Rotator component allowing the GameObject to rotate.
    /// </summary>
    public Rotator Rotator
    {
        get
        {
            return rotator;
        }
        set
        {
            rotator = value;
        }
    }

    /// <summary>
    /// Thuster component allowing the GameObject to move forward.
    /// </summary>
    public Thruster Thruster
    {
        get
        {
            return thruster;
        }
        set
        {
            thruster = value;
        }
    }
    
    /// <summary>
    /// Weapon component allowing the GameObject to fire projectiles.
    /// </summary>
    public Weapon Weapon
    {
        get
        {
            return weapon;
        }
        set
        {
            weapon = value;
        }
    }
    #endregion

    #region General
    /// <summary>
    /// Resets the CommandRelay to its initial state.
    /// </summary>
    public void ResetRelay()
    {
        StopFire();
        StopRotationLeft();
        StopRotationRight();
        StopThrusterImmediate();
    }
    #endregion

    #region Combat
    /// <summary>
    /// Signals that the weapon should begin firing.
    /// </summary>
    public void StartFire()
    {
        if (weapon != null)
        {
            weapon.IsFiring = true;
        }
    }

    /// <summary>
    /// Signals that the weapon should cease firing.
    /// </summary>
    public void StopFire()
    {
        if (weapon != null)
        {
            weapon.IsFiring = false;
        }
    }
    #endregion

    #region Movement
    /// <summary>
    /// Starts rotating the GameObject counter-clockwise.
    /// </summary>
    public void StartRotationLeft()
    {
        if (rotator != null)
        {
            rotator.RotateLeft = true;
            rotator.RotateRight = false;
        }
    }

    /// <summary>
    /// Starts rotating the GameObject clockwise.
    /// </summary>
    public void StartRotationRight()
    {
        if (rotator != null)
        {
            rotator.RotateLeft = false;
            rotator.RotateRight = true;
        }
    }

    /// <summary>
    /// Starts moving the GameObject in its upward direction.
    /// </summary>
    public void StartThruster()
    {
        if (!thruster.gameObject.activeInHierarchy)
        {
            thruster.gameObject.SetActive(true);
        }
        if (thruster != null)
        {
            thruster.Active = true;
        }
    }

    /// <summary>
    /// Stops the GameObject from rotating counter-clockwise.
    /// </summary>
    public void StopRotationLeft()
    {
        if (rotator != null)
        {
            rotator.RotateLeft = false;
        }
    }

    /// <summary>
    /// Stops the GameObject from rotating clockwise.
    /// </summary>
    public void StopRotationRight()
    {
        if (rotator != null)
        {
            rotator.RotateRight = false;
        }
    }

    /// <summary>
    /// Stops moving the GameObject in its upward direction.
    /// </summary>
    public void StopThruster()
    {
        if (thruster != null)
        {
            thruster.Active = false;
        }
    }

    /// <summary>
    /// Immediately stops moving the GameObject in its upward direction.
    /// </summary>
    public void StopThrusterImmediate()
    {
        if (thruster != null)
        {
            thruster.gameObject.SetActive(false);
        }
    }
    #endregion
}
