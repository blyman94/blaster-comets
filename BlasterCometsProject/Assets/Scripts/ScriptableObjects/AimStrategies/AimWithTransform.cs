using UnityEngine;

/// <summary>
/// Aligns the passed projectile to the ProjectileStart position of the weapon.
/// </summary>
[CreateAssetMenu(menuName = "Aim Strategy.../Aim With Transform")]
public class AimWithTransform : AimStrategy
{
    public override void AimProjectile(Weapon weapon,
        Transform projectileTransform)
    {
        if (weapon.ProjectileStart != null)
        {
            projectileTransform.position = weapon.ProjectileStart.position;
            projectileTransform.rotation = weapon.ProjectileStart.rotation;
        }
        else
        {
            projectileTransform.position = weapon.transform.position;
            projectileTransform.rotation = weapon.transform.rotation;
        }
    }
}
