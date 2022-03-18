/// <summary>
/// A class that can control a GameObject through a CommandRelay component.
/// </summary>
public interface IController
{
    /// <summary>
    /// Clears the CommandRelay the IController is currently controlling.
    /// </summary>
    public void ClearRelayToControl();
}
