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
    [SerializeField] private CommandRelay playerRelay;

    #region Input Action Responses
    /// <summary>
    /// Fires a projectile.
    /// </summary>
    /// <param name="context">Callback context of the controlling input 
    /// action.</param>
    public void OnFireAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerRelay.StartFire();
        }
        else if (context.canceled)
        {
            playerRelay.StopFire();
        }
    }

    /// <summary>
    /// Rotates the controlled GameObject counter-clockwise.
    /// </summary>
    /// <param name="context">Callback context of the controlling input 
    /// action.</param>
    public void OnRotateLeftAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerRelay.StartRotationLeft();
        }
        else if (context.canceled)
        {
            playerRelay.StopRotationLeft();
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
            playerRelay.StartRotationRight();
        }
        else if (context.canceled)
        {
            playerRelay.StopRotationRight();
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
            playerRelay.StartThruster();
        }
        else if (context.canceled)
        {
            playerRelay.StopThruster();
        }
    }
    #endregion
}
