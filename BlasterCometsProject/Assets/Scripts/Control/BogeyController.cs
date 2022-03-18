using UnityEngine;

/// <summary>
/// Uses AI to control a bogey enemy.
/// </summary>
public class BogeyController : MonoBehaviour, IController
{
    /// <summary>
    /// Delegate to signal that the active bogey has retreated off screen.
    /// </summary>
    public SimpleDelegate Retreated;

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
    /// Event to raise when a bogey enters.
    /// </summary>
    [Header("Events")]
    [Tooltip("Event to raise when a bogey enters.")]
    [SerializeField] private GameEvent bogeyEnterEvent;

    /// <summary>
    /// Event to raise when a bogey exits.
    /// </summary>
    [Tooltip("Event to raise when a bogey exits.")]
    [SerializeField] private GameEvent bogeyExitEvent;

    /// <summary>
    /// Transform of the bogey being controlled.
    /// </summary>
    private Transform bogeyTransform;

    /// <summary>
    /// CommandRelay of the GameObject being controlled.
    /// </summary>
    private CommandRelay relayToControl;

    /// <summary>
    /// Timer for how long the controlled bogey has been active.
    /// </summary>
    private float lifeTimer = -1;

    /// <summary>
    /// Timer for how long the controlled bogey has been moving in a single
    /// direction.
    /// </summary>
    private float moveTimer = 0;

    #region Properties
    /// <summary>
    /// How fast will the bogey move?
    /// </summary>
    public float MoveSpeed { get; set; } = 1;

    /// <summary>
    /// CommandRelay of the GameObject being controlled.
    /// </summary>
    public CommandRelay RelayToControl
    {
        get
        {
            return relayToControl;
        }
        set
        {
            relayToControl = value;

            if (relayToControl != null)
            {
                bogeyTransform = relayToControl.transform;
                StartBogeyControl();
            }
        }
    }
    #endregion

    #region IController Methods
    public void ClearRelayToControl()
    {
        RelayToControl = null;
    }
    #endregion

    #region MonoBehaviour Methods
    private void Update()
    {
        if (RelayToControl != null)
        {
            ControlBogey();
        }
    }
    #endregion

    /// <summary>
    /// If the bogey has life time remaining, moves it in random directions. 
    /// Otherwise, moves the bogey offscreen to retreat.
    /// </summary>
    private void ControlBogey()
    {
        if (lifeTimer == -1)
        {
            CheckRetreatStatus();
        }
        else if (lifeTimer >= 0)
        {
            lifeTimer -= Time.deltaTime;

            if (moveTimer >= 0)
            {
                moveTimer -= Time.deltaTime;
            }
            else
            {
                MoveInRandomDirection();
            }
        }
        else
        {
            lifeTimer = -1;
            MoveInRandomDirection();
        }
    }

    /// <summary>
    /// Determines if a retreating bogey has left the screen.
    /// </summary>
    private void CheckRetreatStatus()
    {
        if (bogeyTransform.position.x >= cameraBounds.MaxXBound ||
                bogeyTransform.position.x <= cameraBounds.MinXBound ||
                bogeyTransform.position.y >= cameraBounds.MaxYBound ||
                bogeyTransform.position.y <= cameraBounds.MinYBound)
        {
            bogeyExitEvent.Raise();
            Retreated?.Invoke();
        }
    }

    /// <summary>
    /// Selects a random direction and moves the bogey in it by setting its
    /// RigidBody2D's velocity.
    /// </summary>
    private void MoveInRandomDirection()
    {
        float xDir = Random.Range(-1f, 1f);
        float yDir = Random.Range(-1f, 1f);
        Vector3 direction = new Vector3(xDir, yDir, 0);

        relayToControl.Rigidbody2D.velocity = direction.normalized *
            MoveSpeed;
        moveTimer = Random.Range(settings.GameParameters.BogeyMoveTimeRange.x,
            settings.GameParameters.BogeyMoveTimeRange.y);
    }

    /// <summary>
    /// Begins controlling a bogey.
    /// </summary>
    private void StartBogeyControl()
    {
        relayToControl.StartFire();
        lifeTimer = settings.GameParameters.BogeyLifetime;
        MoveInRandomDirection();
        bogeyEnterEvent.Raise();
    }
}
