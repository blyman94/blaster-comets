using UnityEngine;

/// <summary>
/// Module that handles the ship death sequence.
/// </summary>
public class ShipExploder : MonoBehaviour
{
    /// <summary>
    /// Delegate to signal that the ship has exploded.
    /// </summary>
    public SimpleDelegate ShipExploded;

    /// <summary>
    /// Collider of the ship.
    /// </summary>
    [Tooltip("Collider of the ship.")]
    [SerializeField] private PolygonCollider2D shipCollider;

    /// <summary>
    /// SpriteRenderer that represents the ship.
    /// </summary>
    [Tooltip("SpriteRenderer that represents the ship.")]
    [SerializeField] private SpriteRenderer shipRenderer;

    /// <summary>
    /// CommandRelay of the ship.
    /// </summary>
    [Tooltip("CommandRelay of the ship.")]
    [SerializeField] private CommandRelay shipRelay;

    /// <summary>
    /// Color of the ship's sprite renderer.
    /// </summary>
    private Color originalColor;

    #region Properties
    /// <summary>
    /// ObjectPool containing references to explode particle systems. Should be
    /// assigned by the object spawning this meteoroid.
    /// </summary>    
    public ExplosionPool ExplosionPool { get; set; }

    /// <summary>
    /// Controller controlling the ship.
    /// </summary>
    public PlayerController ShipController { get; set; }
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        originalColor = shipRenderer.color;
    }
    #endregion

    /// <summary>
    /// Plays the death sequence of a ship.
    /// </summary>
    public void Explode()
    {
        if (ShipController != null)
        {
            ShipController.RelayToControl = null;
        }

        if (shipCollider != null)
        {
            shipCollider.enabled = false;
        }

        if (shipRenderer != null)
        {
            shipRenderer.color = Color.clear;
        }

        if (shipRelay != null)
        {
            shipRelay.ResetRelay();
        }

        if (ExplosionPool != null)
        {
            GameObject explosionObject = ExplosionPool.Pool.Get();
            explosionObject.transform.position = transform.position;
            explosionObject.SetActive(true);
        }

        ShipExploded?.Invoke();
    }

    /// <summary>
    /// Reverts the ship back to its unexploded state.
    /// </summary>
    public void Unexplode()
    {
        if (shipRenderer != null)
        {
            shipRenderer.color = originalColor;
        }
        if (shipCollider != null)
        {
            shipCollider.enabled = true;
        }
    }
}
