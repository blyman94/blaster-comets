using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Responds to being hit by a projectile.
/// </summary>
public class CombatTarget : MonoBehaviour
{
    /// <summary>
    /// IntVariable representing the player's current score.
    /// </summary>
    [Tooltip("IntVariable representing the player's current score.")]
    [SerializeField] private IntVariable playerScore;

    /// <summary>
    /// CombatTarget's response to being hit by a projectile.
    /// </summary>
    [Tooltip("CombatTarget's response to being hit by a projectile.")]
    [SerializeField] private UnityEvent OnHitResponse;

    #region Properties
    /// <summary>
    /// How many points are awarded if this CombatTarget is destroyed by the 
    /// player.
    /// </summary>
    public int PointValue { get; set; } = 0;
    #endregion

    /// <summary>
    /// Invokes the CombatTarget's OnHitResponse, which is configured in the 
    /// inspector.
    /// </summary>
    public void TakeHit()
    {
        OnHitResponse.Invoke();
    }

    /// <summary>
    /// Awards PointValue points to the player.
    /// </summary>
    public void AwardPoints()
    {
        playerScore.ApplyChange(PointValue);
    }
}
