using UnityEngine;

/// <summary>
/// Aims the projectile at a target position.
/// </summary>
[CreateAssetMenu(menuName = "Aim Strategy.../Aim At Target Position")]
public class AimAtTargetPosition : AimStrategy
{
    /// <summary>
    /// When targeting the ship, the angle of the cone the bogey fires in.
    /// </summary>
    [Tooltip("When targeting the ship, the angle of the cone the bogey " +
        "fires in.")]
    [SerializeField] private FloatVariable fireAngle;

    /// <summary>
    /// Position of this weapon's target.
    /// </summary>
    [Tooltip("Position of this weapon's target.")]
    [SerializeField] private Vector2Variable targetPosition;

    public override void AimProjectile(Weapon weapon, 
        Transform projectileTransform)
    {
        Vector2 targetDir =
                    (targetPosition.Value - (Vector2)weapon.transform.position);
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) *
            Mathf.Rad2Deg;

        projectileTransform.rotation =
            Quaternion.Euler(new Vector3(0, 0, angle - 90));

        projectileTransform.Rotate(0, 0,
            Random.Range(-(fireAngle.Value * 0.5f), 
            (fireAngle.Value * 0.5f)));

        projectileTransform.position = weapon.transform.position +
            (projectileTransform.up * weapon.Radius);
    }
}
