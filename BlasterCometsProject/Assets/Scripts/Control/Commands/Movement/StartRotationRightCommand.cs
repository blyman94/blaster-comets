/// <summary>
/// Command to start rotating a GameObject clockwise.
/// </summary>
public class StartRotationRightCommand : ICommand
{
    public void Execute(CommandRelay commandRelay)
    {
        commandRelay.StartRotationRight();
    }
}
