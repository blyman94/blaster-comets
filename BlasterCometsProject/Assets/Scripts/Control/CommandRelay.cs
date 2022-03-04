using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Receives commands from a control source and delegates them to the
/// appropriate component.
/// </summary>
public class CommandRelay : MonoBehaviour
{
    /// <summary>
    /// Rotator component allowing the GameObject to rotate.
    /// </summary>
    [Header("Movement")]
    [Tooltip("Rotator component allowing the GameObject to rotate.")]
    [SerializeField] private Rotator rotator;

    /// <summary>
    /// Thuster component allowing the GameObject to move forward.
    /// </summary>
    [Tooltip("Thuster component allowing the GameObject to move forward.")]
    [SerializeField] private Thruster thruster;

    #region Properties
    public Queue<ICommand> CommandStream { get; set; }
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        CommandStream = new Queue<ICommand>();
    }
    private void Update()
    {
        if (CommandStream.Count > 0)
        {
            CommandStream.Dequeue().Execute(this);
        }
    }
    #endregion

    #region Movement
    /// <summary>
    /// Starts rotating the GameObject counter-clockwise.
    /// </summary>
    public void StartRotationLeft()
    {
        if (rotator != null)
        {
            rotator.RotateLeft = true;
            rotator.RotateRight = false;
        }
    }

    /// <summary>
    /// Starts rotating the GameObject clockwise.
    /// </summary>
    public void StartRotationRight()
    {
        if (rotator != null)
        {
            rotator.RotateLeft = false;
            rotator.RotateRight = true;
        }
    }

    /// <summary>
    /// Starts moving the GameObject in its upward direction.
    /// </summary>
    public void StartThruster()
    {
        if (thruster != null)
        {
            thruster.Active = true;
        }
    }

    /// <summary>
    /// Stops the GameObject from rotating counter-clockwise.
    /// </summary>
    public void StopRotationLeft()
    {
        if (rotator != null)
        {
            rotator.RotateLeft = false;
        }
    }

    /// <summary>
    /// Stops the GameObject from rotating clockwise.
    /// </summary>
    public void StopRotationRight()
    {
        if (rotator != null)
        {
            rotator.RotateRight = false;
        }
    }

    /// <summary>
    /// Stops moving the GameObject in its upward direction.
    /// </summary>
    public void StopThruster()
    {
        if (thruster != null)
        {
            thruster.Active = false;
        }
    }
    #endregion
}