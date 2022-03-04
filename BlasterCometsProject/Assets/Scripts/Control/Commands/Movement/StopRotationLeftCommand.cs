/// <summary>
/// Command to stop a GameObject from rotating counter-clockwise.
/// </summary>
public class StopRotationLeftCommand : ICommand
{
    public void Execute(CommandRelay commandRelay)
    {
        commandRelay.StopRotationLeft();
    }
}
