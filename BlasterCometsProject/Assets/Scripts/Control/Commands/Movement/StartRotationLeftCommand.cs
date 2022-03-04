/// <summary>
/// Command to start rotating a GameObject counter-clockwise.
/// </summary>
public class StartRotationLeftCommand : ICommand
{
    public void Execute(CommandRelay commandRelay)
    {
        commandRelay.StartRotationLeft();
    }
}
