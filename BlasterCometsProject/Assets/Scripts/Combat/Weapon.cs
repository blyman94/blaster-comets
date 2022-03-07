using UnityEngine;

/// <summary>
/// Allows the attached GameObject to fire projectiles.
/// </summary>
public class Weapon : MonoBehaviour
{
    /// <summary>
    /// How long the weapon must wait before firing consecutive shots.
    /// </summary>
    [Header("General")]
    [Tooltip("How long the weapon must wait before firing consecutive shots.")]
    [SerializeField] private float cooldown = 0.5f;

    /// <summary>
    /// Transform whose position will dictate where the projectile is spawned 
    /// and the spawned projectile's rotation.
    /// </summary>

    [Tooltip("Transform whose position will dictate where the projectile " +
        "is spawned and the spawned projectile's rotation.")]
    [SerializeField] private Transform projectileStart;

    /// <summary>
    /// Angle of the firing cone, centered on the target.
    /// </summary>
    [Header("Targeting")]
    [Tooltip("Angle of the firing cone, centered on the target.")]
    [SerializeField] private float targetAngle;

    /// <summary>
    /// Position of this weapon's target.
    /// </summary>
    [Tooltip("Position of this weapon's target.")]
    [SerializeField] private Vector2Variable targetPosition;

    /// <summary>
    /// Determines how long the projectile stays active for after its is fired.
    /// </summary>
    [Header("Projectile Parameters")]
    [Tooltip("Determines how long the projectile stays active for after " +
        "it is fired.")]
    [SerializeField] private float lifeTime = 0.9f;

    /// <summary>
    /// Determines the start position of the projectile.
    /// </summary>
    [Tooltip("Determines the start position of the projectile.")]
    [SerializeField] private float radius = 1;

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

    /// <summary>
    /// Should this weapon be firing in random directions?
    /// </summary>
    public bool FireRandom { get; set; } = false;

    /// <summary>
    /// Should this weapon be firing at a target?
    /// </summary>
    public bool FireAtTarget { get; set; } = false;
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
            if (FireRandom)
            {
                FireProjectileRandom();
            }
            else if (FireAtTarget)
            {
                FireProjectileAtTarget();
            }
            else
            {
                FireProjectile();
            }
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
    private void FireProjectile()
    {
        GameObject projectileObject = projectilePool.Get();
        if (projectileObject != null)
        {
            projectileObject.transform.SetParent(null);

            if (projectileStart != null)
            {
                projectileObject.transform.position = projectileStart.position;
                projectileObject.transform.rotation = projectileStart.rotation;
            }
            else
            {
                projectileObject.transform.position = transform.position;
                projectileObject.transform.rotation = transform.rotation;
            }

            Projectile projectile = projectileObject.GetComponent<Projectile>();

            projectileObject.SetActive(true);
            projectile.Fire(travelSpeed, lifeTime);

            cooldownTimer = cooldown;
        }
    }

    /// <summary>
    /// Fires a projectile at the target.
    /// </summary>
    private void FireProjectileAtTarget()
    {
        GameObject projectileObject = projectilePool.Get();
        if (projectileObject != null)
        {
            projectileObject.transform.SetParent(null);

            Vector2 sub = (targetPosition.Value - (Vector2)transform.position);
            float angle = Mathf.Atan2(sub.y, sub.x) * Mathf.Rad2Deg;

            projectileObject.transform.rotation = 
                Quaternion.Euler(new Vector3(0, 0, angle - 90));

            projectileObject.transform.Rotate(0, 0,
                Random.Range(-(targetAngle * 0.5f),
                (targetAngle * 0.5f)));

            projectileObject.transform.position = transform.position +
                (projectileObject.transform.up * radius);

            Projectile projectile = projectileObject.GetComponent<Projectile>();

            projectileObject.SetActive(true);
            projectile.Fire(travelSpeed, lifeTime);

            cooldownTimer = cooldown;
        }
    }

    /// <summary>
    /// Fires a projectile in a random direction.
    /// </summary>
    private void FireProjectileRandom()
    {
        GameObject projectileObject = projectilePool.Get();
        if (projectileObject != null)
        {
            projectileObject.transform.SetParent(null);

            projectileObject.transform.Rotate(0, 0, Random.Range(0, 360));
            projectileObject.transform.position = transform.position + (projectileObject.transform.up * radius);

            Projectile projectile = projectileObject.GetComponent<Projectile>();

            projectileObject.SetActive(true);
            projectile.Fire(travelSpeed, lifeTime);

            cooldownTimer = cooldown;
        }
    }
}
