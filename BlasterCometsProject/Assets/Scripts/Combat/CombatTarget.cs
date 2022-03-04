using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Responds to being hit by a projectile.
/// </summary>
public class CombatTarget : MonoBehaviour
{
    /// <summary>
    /// CombatTarget's response to being hit by a projectile.
    /// </summary>
    [Tooltip("CombatTarget's response to being hit by a projectile.")]
    [SerializeField] private UnityEvent OnHitResponse;

    /// <summary>
    /// Invokes the CombatTarget's OnHitResponse, which is configured in the 
    /// inspector.
    /// </summary>
    public void TakeProjectileHit()
    {
        OnHitResponse.Invoke();
    }
}
