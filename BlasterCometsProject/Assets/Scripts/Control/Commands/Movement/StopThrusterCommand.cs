/// <summary>
/// Command to stop the thruster from moving the GameObject forward.
/// </summary>
public class StopThrusterCommand : ICommand
{
    public void Execute(CommandRelay commandRelay)
    {
        commandRelay.StopThruster();
    }
}
