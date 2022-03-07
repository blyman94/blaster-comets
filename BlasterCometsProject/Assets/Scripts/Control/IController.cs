/// <summary>
/// A class that can control a GameObject through a CommandRelay component.
/// </summary>
public interface IController
{
    /// <summary>
    /// CommandRelay of the GameObject being controlled.
    /// </summary>
    public CommandRelay RelayToControl { get; set; }
}
