using UnityEngine;

/// <summary>
/// Allows the attached GameObject to fire projectiles.
/// </summary>
public class Weapon : MonoBehaviour
{
    /// <summary>
    /// Transform whose position will dictate where the projectile is spawned 
    /// and the spawned projectile's rotation.
    /// </summary>
    [Header("General")]
    [Tooltip("Transform whose position will dictate where the projectile " +
        "is spawned and the spawned projectile's rotation.")]
    [SerializeField] private Transform projectileStart;

    /// <summary>
    /// How long the weapon must wait before firing consecutive shots.
    /// </summary>
    [Tooltip("How long the weapon must wait before firing consecutive shots.")]
    [SerializeField] private float cooldown = 0.5f;

    /// <summary>
    /// Determines how long the projectile stays active for after its is fired.
    /// </summary>
    [Header("Projectile Parameters")]
    [Tooltip("Determines how long the projectile stays active for after " +
        "it is fired.")]
    [SerializeField] private float lifeTime = 0.9f;

    /// <summary>
    /// Determines how fast the projectile moves when fired.
    /// </summary>
    [Tooltip("Determines how fast the projectile moves when fired.")]
    [SerializeField] private float travelSpeed = 10;

    /// <summary>
    /// Initial size of the projectile pool.
    /// </summary>
    [Header("Projectile Pool")]
    [Tooltip("Initial size of the projectile pool.")]
    [SerializeField] private int initialSize = 10;

    /// <summary>
    /// Transform to which pooled projectile objects are parented.
    /// </summary>
    [Tooltip("Transform to which pooled projectile objects are parented.")]
    [SerializeField] private Transform projectilePoolParent;

    /// <summary>
    /// Prefab object representing the projectile to be fired.
    /// </summary>
    [Tooltip("Prefab object representing the projectile to be fired.")]
    [SerializeField] private GameObject projectilePrefab;

    /// <summary>
    /// Object pool for projectiles.
    /// </summary>
    private ObjectPool projectilePool;

    /// <summary>
    /// Timer for weapon cooldown.
    /// </summary>
    private float cooldownTimer;

    #region Properties
    /// <summary>
    /// Should this weapon be firing?
    /// </summary>
    public bool IsFiring { get; set; } = false;
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        projectilePool =
            new ObjectPool(projectilePrefab, initialSize, true,
            projectilePoolParent);
    }
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
    /// Fires a projectile.
    /// </summary>
    public void FireProjectile()
    {
        GameObject projectileObject = projectilePool.Get();
        if (projectileObject != null)
        {
            projectileObject.transform.SetParent(null);
            projectileObject.transform.position = projectileStart.position;
            projectileObject.transform.rotation = projectileStart.rotation;

            Projectile projectile = projectileObject.GetComponent<Projectile>();

            projectileObject.SetActive(true);
            projectile.Fire(travelSpeed, lifeTime);

            cooldownTimer = cooldown;
        }
    }
}
