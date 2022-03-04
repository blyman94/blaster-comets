using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Allows the player to control a GameObject utilizing an Input Action asset 
/// from the Input System package.
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// CommandRelay of the player character GameObject.
    /// </summary>
    [Tooltip("CommandRelay of the player character GameObject.")]
    [SerializeField] private CommandRelay commandRelay;

    #region Commands

    // Movement Commands

    /// <summary>
    /// Command to make the controlled GameObject start rotating 
    /// counter-clockwise.
    /// </summary>
    private StartRotationLeftCommand startRotationLeft = 
        new StartRotationLeftCommand();

    /// <summary>
    /// Command to make the controlled GameObject start rotating clockwise.
    /// </summary>
    private StartRotationRightCommand startRotationRight = 
        new StartRotationRightCommand();

    /// <summary>
    /// Command to start the thruster, moving the GameObject in its upward 
    /// direction.
    /// </summary>
    private StartThrusterCommand startThruster = new StartThrusterCommand();

    /// <summary>
    /// Command to make the controlled GameObject stop rotating 
    /// counter-clockwise.
    /// </summary>
    private StopRotationLeftCommand stopRotationLeft =
        new StopRotationLeftCommand();

    /// <summary>
    /// Command to make the controlled GameObject stop rotating clockwise.
    /// </summary>
    private StopRotationRightCommand stopRotationRight =
        new StopRotationRightCommand();

    /// <summary>
    /// Command to stop the thruster, which will beginning slowing the 
    /// GameObject down.
    /// </summary>
    private StopThrusterCommand stopThruster = new StopThrusterCommand();
    #endregion

    #region Input Action Responses
    /// <summary>
    /// Rotates the controlled GameObject counter-clockwise.
    /// </summary>
    /// <param name="context">Callback context of the controlling input 
    /// action.</param>
    public void OnRotateLeftAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            commandRelay.CommandStream.Enqueue(startRotationLeft);
        }
        else if (context.canceled)
        {
            commandRelay.CommandStream.Enqueue(stopRotationLeft);
        }
    }

    /// <summary>
    /// Rotates the controlled GameObject clockwise.
    /// </summary>
    /// <param name="context">Callback context of the controlling input 
    /// action.</param>
    public void OnRotateRightAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            commandRelay.CommandStream.Enqueue(startRotationRight);
        }
        else if (context.canceled)
        {
            commandRelay.CommandStream.Enqueue(stopRotationRight);
        }
    }

    /// <summary>
    /// Toggles the thruster on and off based on input.
    /// </summary>
    /// <param name="context">Callback context of the controlling input 
    /// action.</param>
    public void OnThrusterAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            commandRelay.CommandStream.Enqueue(startThruster);
        }
        else if (context.canceled)
        {
            commandRelay.CommandStream.Enqueue(stopThruster);
        }
    }
    #endregion
}
