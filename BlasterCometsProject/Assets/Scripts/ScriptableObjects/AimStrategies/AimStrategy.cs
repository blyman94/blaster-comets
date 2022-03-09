using UnityEngine;

/// <summary>
/// Dictates how a weapon should aim its projectile.
/// </summary>
public abstract class AimStrategy : ScriptableObject
{
    /// <summary>
    /// Aims the projectile.
    /// </summary>
    /// <param name="weapon">Weapon with which to aim projectile.</param>
    /// <param name="projectileTransform">Transform of the projectile to 
    /// aim.</param>
    public abstract void AimProjectile(Weapon weapon,
        Transform projectileTransform);
}