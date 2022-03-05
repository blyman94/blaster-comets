using UnityEngine;

/// <summary>
/// Exposes an object pool containing explosion particle systems for when 
/// various objects are destroyed.
/// </summary>
public class ExplosionPool : MonoBehaviour
{
    /// <summary>
    /// GameObject prefab representing the explosion particle system.
    /// </summary>
    [Header("Particle System Pool")]
    [Tooltip("GameObject prefab representing the explosion particle system.")]
    [SerializeField] private GameObject explosionPrefab;

    /// <summary>
    /// Initial size of the particle system pool.
    /// </summary>
    [Tooltip("Initial size of the particle system pool.")]
    [SerializeField] private int initialSize = 12;

    /// <summary>
    /// Transform to which pooled explode particle system objects are parented.
    /// </summary>
    [Tooltip("Transform to which pooled explode particle system objects " + 
        "are parented.")]
    [SerializeField] private Transform explosionPoolParent;

    #region Properties
    /// <summary>
    /// ObjectPool containing references to explosion particle systems.
    /// </summary>
    public ObjectPool Pool { get; set; }
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        Pool = new ObjectPool(explosionPrefab, initialSize, true,
            explosionPoolParent);
    }   
    #endregion
}
