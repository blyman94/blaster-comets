using UnityEngine;

/// <summary>
/// Allows the GameObject to enter hyperspace - Disappearing from the screen and
/// appearing randomly somewhere else on the screen.
/// </summary>
public class Hyperspace : MonoBehaviour
{
    /// <summary>
    /// The game's settings.
    /// </summary>
    [Tooltip("The game's settings.")]
    [SerializeField] private Settings settings;

    /// <summary>
    /// Bounds of the main camera.
    /// </summary>
    [Header("General")]
    [Tooltip("Bounds of the main camera.")]
    [SerializeField] private CameraBounds cameraBounds;

    /// <summary>
    /// Collider of the hyperspace object to be toggled.
    /// </summary>
    [Tooltip("Collider of the hyperspace object to be toggled.")]
    [SerializeField] private new Collider2D collider2D;

    /// <summary>
    /// Animator used to visualize hyperspace.
    /// </summary>
    [Tooltip("Animator used to visualize hyperspace.")]
    [SerializeField] private Animator hyperspaceAnimator;

    /// <summary>
    /// Transform to move with hyperspace.
    /// </summary>
    [Tooltip("Transform to move with hyperspace.")]
    [SerializeField] private Transform hyperspaceTransform;

    /// <summary>
    /// Event to raise when a ship enters hyper space.
    /// </summary>
    [Header("Events")]
    [Tooltip("Event to raise when a ship enters hyper space.")]
    [SerializeField] private GameEvent hyperspaceEnterEvent;

    /// <summary>
    /// Event to raise when a ship exits hyper space.
    /// </summary>
    [Tooltip("Event to raise when a ship exits hyper space.")]
    [SerializeField] private GameEvent hyperspaceExitEvent;

    /// <summary>
    /// Timer to track how long the GameObject has been in hyperspace.
    /// </summary>
    private float hyperspaceInTimer;

    /// <summary>
    /// Timer to track how long the GameObject has waited in between hyperspace
    /// jumps.
    /// </summary>
    private float hyperspaceCooldownTimer;

    #region Properties
    /// <summary>
    /// Is the GameObject currently in hyperspace?
    /// </summary>
    public bool InHyperspace { get; set; } = false;
    #endregion


    #region MonoBehaviour Methods
    public void Update()
    {
        if (hyperspaceInTimer > 0)
        {
            hyperspaceInTimer -= Time.deltaTime;
        }
        else
        {
            if (InHyperspace)
            {
                ExitHyperspace();
            }
        }

        if (hyperspaceCooldownTimer > 0)
        {
            hyperspaceCooldownTimer -= Time.deltaTime;
        }
    }
    #endregion

    /// <summary>
    /// Sends the attached GameObject into hyperspace.
    /// </summary>
    public void EnterHyperspace()
    {
        if (!InHyperspace && hyperspaceCooldownTimer <= 0)
        {
            InHyperspace = true;
            hyperspaceInTimer = settings.GameParameters.ShipHyperspaceInTime;
            collider2D.enabled = false;
            hyperspaceAnimator.SetTrigger("EnterHyperspace");
            hyperspaceEnterEvent.Raise();
        }
    }

    /// <summary>
    /// Returns the attached GameObject from hyperspace.
    /// </summary>
    private void ExitHyperspace()
    {
        hyperspaceTransform.position = 
            cameraBounds.GetRandomPositionWithin();

        InHyperspace = false;
        hyperspaceCooldownTimer = settings.GameParameters.ShipHyperspaceCooldown;
        collider2D.enabled = true;
        hyperspaceAnimator.SetTrigger("ExitHyperspace");
        hyperspaceExitEvent.Raise();
    }
}
