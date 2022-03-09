using UnityEngine;

/// <summary>
/// Uses a Rigidbody2D component to move a GameObject in it's upward
/// direction.
/// </summary>
public class Thruster : MonoBehaviour
{
    /// <summary>
    /// Rigidbody2D component of the GameObject to be moved.
    /// </summary>
    [Header("General")]
    [Tooltip("Rigidbody2D component of the GameObject to be moved.")]
    [SerializeField] private new Rigidbody2D rigidbody2D;

    /// <summary>
    /// Transform from which the thruster will derive its upward direction.
    /// </summary>
    [Tooltip("Transform from which the thruster will derive its upward " +
        "direction.")]
    [SerializeField] private Transform rotationTransform;

    /// <summary>
    /// Particle system representing the thruster visually.
    /// </summary>
    [Tooltip("Particle system representing the thruster visually.")]
    [SerializeField] private ParticleSystem thrusterParticleSystem;

    #region Properties
    /// <summary>
    /// Is the thruster currently active?
    /// </summary>
    public bool Active { get; set; } = false;

    /// <summary>
    /// Max speed at which the GameObject can travel.
    /// </summary>
    public float MaxSpeed { get; set; }

    /// <summary>
    /// Force applied per tick when thruster is active.
    /// </summary>
    public float ThrustForce { get; set; }
    #endregion

    #region MonoBehaviour Methods
    private void FixedUpdate()
    {
        if (rigidbody2D != null)
        {
            bool atMaxSpeed = rigidbody2D.velocity.magnitude >= MaxSpeed;
            if (Active && !atMaxSpeed)
            {
                rigidbody2D.AddForce(rotationTransform.up * ThrustForce * 
                    Time.fixedDeltaTime);
            }
        }
    }
    private void Update()
    {
        if (Active && !thrusterParticleSystem.isPlaying)
        {
            thrusterParticleSystem.Play();
        }

        if (!Active && thrusterParticleSystem.isPlaying)
        {
            thrusterParticleSystem.Stop();
        }
    }
    #endregion
}
