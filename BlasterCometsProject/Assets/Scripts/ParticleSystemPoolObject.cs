using UnityEngine;

/// <summary>
/// PoolObject that observes a particle system and releases itself back to its 
/// origin pool once that particle system finishes playing.
/// </summary>
public class ParticleSystemPoolObject : MonoBehaviour, IPoolObject
{
    /// <summary>
    /// Particle system to observe. This pool object will release itself back
    /// to the origin pool once this particle system finishes playing.
    /// </summary>
    [Tooltip("Particle system to observe. This pool object will release " +
        "itself back to the origin pool once this particle system " + 
        "finishes playing.")]
    [SerializeField] private ParticleSystem targetSystem;

    #region MonoBehaviour Methods
    private void Start()
    {
        if (targetSystem != null)
        {
            ParticleSystem.MainModule main = targetSystem.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }
    }
    #endregion

    #region IPoolObject Methods
    public ObjectPool OriginPool { get; set; }
    #endregion

    /// <summary>
    /// Responds to the ParticleSYstemStopAction by releasing the GameObject
    /// back to its origin pool, or simply deactivating the GameObject if there
    /// is no origin pool.
    /// </summary>
    private void OnParticleSystemStopped()
    {
        if (OriginPool != null)
        {
            OriginPool.Release(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
