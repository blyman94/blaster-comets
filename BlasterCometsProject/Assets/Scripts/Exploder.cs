using UnityEngine;

/// <summary>
/// Module that explodes a bogey or a ship.
/// </summary>
public class Exploder : MonoBehaviour
{
    /// <summary>
    /// Delegate to signal that the exploding entity has exploded.
    /// </summary>
    public SimpleDelegate EntityExploded;

    /// <summary>
    /// Collider of the exploding entity.
    /// </summary>
    [Header("General")]
    [Tooltip("Collider of the exploding entity.")]
    [SerializeField] private Collider2D entityCollider;

    /// <summary>
    /// SpriteRenderer that represents the exploding entity.
    /// </summary>
    [Tooltip("SpriteRenderer that represents the exploding entity.")]
    [SerializeField] private SpriteRenderer entityRenderer;

    /// <summary>
    /// CommandRelay of the exploding entity.
    /// </summary>
    [Tooltip("CommandRelay of the exploding entity.")]
    [SerializeField] private CommandRelay entityRelay;

    /// <summary>
    /// Color of the ship's sprite renderer.
    /// </summary>
    private Color originalColor;

    #region Properties
    /// <summary>
    /// Audio source used to play explosions when this object is destroyed.
    /// </summary>
    public AudioSource ExplosionAudioSource { get; set; }

    /// <summary>
    /// ObjectPool containing references to explode particle systems. Should be
    /// assigned by the object spawning this meteoroid.
    /// </summary>    
    public ObjectPool ExplosionPool { get; set; }

    /// <summary>
    /// Controller controlling the ship.
    /// </summary>
    public IController EntityController { get; set; }
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        originalColor = entityRenderer.color;
    }
    #endregion

    /// <summary>
    /// Plays the death sequence of a ship.
    /// </summary>
    public void Explode()
    {
        if (EntityController != null)
        {
            EntityController.RelayToControl = null;
        }

        if (entityCollider != null)
        {
            entityCollider.enabled = false;
        }

        if (entityRenderer != null)
        {
            entityRenderer.color = Color.clear;
        }

        if (entityRelay != null)
        {
            entityRelay.ResetRelay();
        }

        if (ExplosionPool != null)
        {
            GameObject explosionObject = ExplosionPool.Get();
            explosionObject.transform.position = transform.position;
            explosionObject.SetActive(true);
        }

        EntityExploded?.Invoke();
    }

    /// <summary>
    /// Plays explosion audio at the passed pitch.
    /// </summary>
    /// <param name="pitch">Pitch at which to play explosion.</param>
    public void PlayExplosionAudio(float pitch)
    {
        ExplosionAudioSource.pitch = pitch;
        ExplosionAudioSource.Play();
    }

    /// <summary>
    /// Reverts the ship back to its unexploded state.
    /// </summary>
    public void Unexplode()
    {
        if (entityRenderer != null)
        {
            entityRenderer.color = originalColor;
        }
        if (entityCollider != null)
        {
            entityCollider.enabled = true;
        }
    }
}
