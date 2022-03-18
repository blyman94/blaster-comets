using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Can be fired by the player or a bogey to destroy meteroids or eachother.
/// </summary>
public class Projectile : MonoBehaviour, IPoolObject
{
    /// <summary>
    /// Rigidbody2D component of the projectile.
    /// </summary>
    [Tooltip("Rigidbody2D component of the projectile.")]
    [SerializeField] private new Rigidbody2D rigidbody2D;

    /// <summary>
    /// Array of strings representing the tags of targets this projectile can
    /// destroy.
    /// </summary>
    [Tooltip("Array of strings representing the tags of targets this " +
        "projectile can destroy.")]
    [SerializeField] private List<string> targetTags;

    /// <summary>
    /// Timer used to determine how long the projectile has been alive for.
    /// </summary>
    private float lifeTimer;

    #region IPoolObject Methods
    public ObjectPool OriginPool { get; set; }
    #endregion

    #region MonoBehaviour Methods
    private void Update()
    {
        if (lifeTimer > 0)
        {
            lifeTimer -= Time.deltaTime;
        }
        else
        {
            OriginPool.Release(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        CombatTarget hitTarget = other.GetComponent<CombatTarget>();
        if (hitTarget != null && targetTags.Count > 0 && 
            targetTags.Contains(hitTarget.tag))
        {
            hitTarget.TakeHit();
            hitTarget.AwardPoints();
        }
        OriginPool.Release(gameObject);
    }
    #endregion

    /// <summary>
    /// Sets the velocity and lifetime of the projectile based on passed
    /// parameters.
    /// </summary>
    /// <param name="travelSpeed">Speed at which the projectile will 
    /// travel.</param>
    /// <param name="lifeTime">How long the projectile will remain active 
    /// for.</param>
    public void Fire(float travelSpeed, float lifeTime)
    {
        lifeTimer = lifeTime;
        rigidbody2D.velocity = transform.up * travelSpeed;
    }
}
