using UnityEngine;

/// <summary>
/// Randomly aims the projectile.
/// </summary>
[CreateAssetMenu(menuName = "Aim Strategy.../Aim Random")]
public class AimRandom : AimStrategy
{
    public override void AimProjectile(Weapon weapon, 
        Transform projectileTransform)
    {
        projectileTransform.Rotate(0, 0, Random.Range(0, 360));
        projectileTransform.position = weapon.transform.position +
            (projectileTransform.up * weapon.Radius);
    }
}
