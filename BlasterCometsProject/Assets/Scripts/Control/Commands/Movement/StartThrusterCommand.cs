/// <summary>
/// Command to start the thruster, moving the GameObject forward.
/// </summary>
public class StartThrusterCommand : ICommand
{
    public void Execute(CommandRelay commandRelay)
    {
        commandRelay.StartThruster();
    }
}

