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
    /// Combat Target for the controlled object.
    /// </summary>
    [Header("Combat")]
    [Tooltip("Combat Target for the controlled object.")]
    [SerializeField] private CombatTarget combatTarget;

    /// <summary>
    /// Exploder module for the controlled object.
    /// </summary>
    [Tooltip("Exploder module for the controlled object.")]
    [SerializeField] private Exploder exploder;

    /// <summary>
    /// Weapon component allowing the GameObject to fire projectiles.
    /// </summary>
    [Tooltip("Weapon component allowing the GameObject to fire projectiles.")]
    [SerializeField] protected Weapon weapon;

    #region Properties
    /// <summary>
    /// Combat Target for the controlled object.
    /// </summary>
    public CombatTarget CombatTarget
    {
        get
        {
            return combatTarget;
        }
        set
        {
            combatTarget = value;
        }
    }

    /// <summary>
    /// Exploder module for the controlled object.
    /// </summary>
    public Exploder Exploder
    {
        get
        {
            return exploder;
        }
        set
        {
            exploder = value;
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
    public virtual void ResetRelay()
    {
        StopFire();
        Rigidbody2D.velocity = Vector2.zero;
    }
    #endregion

    /// <summary>
    /// Signals that the weapon should begin firing.
    /// </summary>
    public virtual void StartFire()
    {
        weapon.IsFiring = true;
    }

    /// <summary>
    /// Signals that the weapon should cease firing.
    /// </summary>
    public void StopFire()
    {
        weapon.IsFiring = false;
    }
    
}
