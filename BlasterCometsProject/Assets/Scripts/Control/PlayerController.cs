using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Allows the player to control a GameObject utilizing an Input Action asset 
/// from the Input System package.
/// </summary>
public class PlayerController : MonoBehaviour, IController
{
    /// <summary>
    /// Allows the player to pause the game.
    /// </summary>
    [Tooltip("Allows the player to pause the game.")]
    [SerializeField] private GamePauser gamePauser;

    #region Properties
    /// <summary>
    /// CommandRelay of the GameObject to be controlled.
    /// </summary>
    public ShipCommandRelay RelayToControl { get; set; }
    #endregion

    #region IController Methods
    public void ClearRelayToControl()
    {
        RelayToControl = null;
    }
    #endregion

    #region Input Action Responses
    /// <summary>
    /// Fires a projectile.
    /// </summary>
    /// <param name="context">Callback context of the controlling input 
    /// action.</param>
    public void OnFireAction(InputAction.CallbackContext context)
    {
        if (RelayToControl != null)
        {
            if (context.started)
            {
                RelayToControl.StartFire();
            }
            else if (context.canceled)
            {
                RelayToControl.StopFire();
            }
        }
    }

    /// <summary>
    /// Sends the controlled into hyperspace.
    /// </summary>
    /// <param name="context">Callback context of the controlling input 
    /// action.</param>
    public void OnHyperspaceAction(InputAction.CallbackContext context)
    {
        if (RelayToControl != null)
        {
            if (context.started)
            {
                RelayToControl.EnterHyperspace();
            }
        }
    }

    /// <summary>
    /// Pauses and unpauses the game.
    /// </summary>
    /// <param name="context">Callback context of the controlling input 
    /// action.</param>
    public void OnPauseAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            gamePauser.ToggleGamePause();
        }
    }

    /// <summary>
    /// Rotates the controlled GameObject counter-clockwise.
    /// </summary>
    /// <param name="context">Callback context of the controlling input 
    /// action.</param>
    public void OnRotateLeftAction(InputAction.CallbackContext context)
    {
        if (RelayToControl != null)
        {
            if (context.started)
            {
                RelayToControl.StartRotationLeft();
            }
            else if (context.canceled)
            {
                RelayToControl.StopRotationLeft();
            }
        }
    }

    /// <summary>
    /// Rotates the controlled GameObject clockwise.
    /// </summary>
    /// <param name="context">Callback context of the controlling input 
    /// action.</param>
    public void OnRotateRightAction(InputAction.CallbackContext context)
    {
        if (RelayToControl != null)
        {
            if (context.started)
            {
                RelayToControl.StartRotationRight();
            }
            else if (context.canceled)
            {
                RelayToControl.StopRotationRight();
            }
        }
    }

    /// <summary>
    /// Toggles the thruster on and off based on input.
    /// </summary>
    /// <param name="context">Callback context of the controlling input 
    /// action.</param>
    public void OnThrusterAction(InputAction.CallbackContext context)
    {
        if (RelayToControl != null)
        {
            if (context.started)
            {
                RelayToControl.StartThruster();
            }
            else if (context.canceled)
            {
                RelayToControl.StopThruster();
            }
        }
    }
    #endregion
}
