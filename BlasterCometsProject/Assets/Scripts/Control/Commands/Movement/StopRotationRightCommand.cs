/// <summary>
/// Command to stop a GameObject from rotating clockwise.
/// </summary>
public class StopRotationRightCommand : ICommand
{
    public void Execute(CommandRelay commandRelay)
    {
        commandRelay.StopRotationRight();
    }
}
