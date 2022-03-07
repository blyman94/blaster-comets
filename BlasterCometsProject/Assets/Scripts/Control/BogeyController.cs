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
    /// Main Camera used to determine random spawn positions.
    /// </summary>
    [Header("General")]
    [Tooltip("Main Camera used to determine random spawn positions.")]
    [SerializeField] private Camera mainCamera;

    /// <summary>
    /// How long does the bogey last before retreating?
    /// </summary>
    [Header("Bogey Parameters")]
    [Tooltip("How long does the bogey last before retreating?")]
    public float Lifetime;

    /// <summary>
    /// What is the shortest amount of time a bogey will move in a single 
    /// direction?
    /// </summary>
    [Tooltip("What is the shortest amount of time a bogey will move in " +
        "a single direction?")]
    [SerializeField] private float minMoveTime;

    /// <summary>
    /// What is the longest amount of time a bogey will move in a single 
    /// direction?
    /// </summary>
    [Tooltip("What is the longest amount of time a bogey will move in " +
        "a single direction?")]
    [SerializeField] private float maxMoveTime;

    /// <summary>
    /// Bounds of the main camera.
    /// </summary>
    private CameraBounds cameraBounds;

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

    /// <summary>
    /// Is the bogey currently retreating?
    /// </summary>
    private bool isRetreating;

    #region Properties
    /// <summary>
    /// Is the bogey being controlled a large bogey? If false, the bogey being
    /// controlled will be considered a small bogey.
    /// </summary>
    public bool IsLargeBogey { get; set; }

    /// <summary>
    /// How fast will the bogey move?
    /// </summary>
    public float MoveSpeed { get; set; } = 1;
    #endregion

    #region IController Methods
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

    #region MonoBehaviour Methods
    private void Start()
    {
        if (mainCamera != null)
        {
            cameraBounds = mainCamera.GetBounds();
        }
    }
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
            Retreat();
        }
    }

    /// <summary>
    /// Determines if a retreating bogey has left the screen.
    /// </summary>
    private void CheckRetreatStatus()
    {
        if (isRetreating)
        {
            if (bogeyTransform.position.x >= cameraBounds.MaxXBound ||
                bogeyTransform.position.x <= cameraBounds.MinXBound ||
                bogeyTransform.position.y >= cameraBounds.MaxYBound ||
                bogeyTransform.position.y <= cameraBounds.MinYBound)
            {
                isRetreating = false;
                Retreated?.Invoke();
            }
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
        moveTimer = Random.Range(minMoveTime, maxMoveTime);
    }

    /// <summary>
    /// Moves the bogey to a random screen boundary and deactivates it.
    /// </summary>
    private void Retreat()
    {
        isRetreating = true;
        lifeTimer = -1;
        MoveInRandomDirection();
    }

    /// <summary>
    /// Begins controlling a bogey.
    /// </summary>
    private void StartBogeyControl()
    {
        if (IsLargeBogey)
        {
            relayToControl.StartFireRandom();
        }
        else
        {
            relayToControl.StartFireAtTarget();
        }

        lifeTimer = Lifetime;
        MoveInRandomDirection();
    }
}
