using UnityEngine;

/// <summary>
/// Allows the attached GameObject to fire projectiles.
/// </summary>
public class Weapon : MonoBehaviour
{
    /// <summary>
    /// Dictates how the weapon will aim its projectile.
    /// </summary>
    [Header("General")]
    [Tooltip("Dictates how the weapon will aim its projectile.")]
    [SerializeField] private AimStrategy aimStrategy;

    /// <summary>
    /// Transform whose position will dictate where the projectile is spawned 
    /// and the spawned projectile's rotation.
    /// </summary>
    [Tooltip("Transform whose position will dictate where the projectile " +
        "is spawned and the spawned projectile's rotation.")]
    public Transform ProjectileStart;

    /// <summary>
    /// Determines the start position of the projectile.
    /// </summary>
    [Tooltip("Determines the start position of the projectile.")]
    public float Radius = 1;

    /// <summary>
    /// Event raised when the weapon is fired.
    /// </summary>
    [Header("Events")]
    [Tooltip("Event raised when the weapon is fired.")]
    [SerializeField] private GameEvent fireEvent;

    /// <summary>
    /// Timer for weapon cooldown.
    /// </summary>
    private float cooldownTimer;

    #region Properties
    /// <summary>
    /// How long the weapon must wait before firing consecutive shots.
    /// </summary>
    public float Cooldown { get; set; }

    /// <summary>
    /// Should this weapon be firing?
    /// </summary>
    public bool IsFiring { get; set; } = false;

    /// <summary>
    /// Determines how long the projectile stays active for after its is fired.
    /// </summary>
    public float ProjectileLifetime { get; set; }

    /// <summary>
    /// Object pool containing friendly projectiles.
    /// </summary>
    public ObjectPool ProjectilePool { get; set; }

    /// <summary>
    /// Determines how fast the projectile moves when fired.
    /// </summary>
    public float ProjectileTravelSpeed { get; set; }
    #endregion

    #region MonoBehaviour Methods
    private void Update()
    {
        if (IsFiring && cooldownTimer <= 0)
        {
            FireProjectile();
        }
        else if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
    #endregion

    /// <summary>
    /// Fires a projectile based on the assigned aim strategy.
    /// </summary>
    private void FireProjectile()
    {
        GameObject projectileObject = ProjectilePool.Get();
        
        Transform projectileTransform = projectileObject.transform;
        projectileTransform.SetParent(null);

        aimStrategy.AimProjectile(this, projectileTransform);

        projectileObject.SetActive(true);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Fire(ProjectileTravelSpeed, ProjectileLifetime);

        fireEvent.Raise();

        cooldownTimer = Cooldown;
    }
}
