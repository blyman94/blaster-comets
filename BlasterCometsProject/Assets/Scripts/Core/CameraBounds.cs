/// <summary>
/// Represents the maximum postion coordinates bounded by an orthographic 
/// camera's screen.
/// </summary>
public class CameraBounds
{
    /// <summary>
    /// Maximum X position within view of the camera.
    /// </summary>
    public float MaxXBound;

    /// <summary>
    /// Maximum Y position within view of the camera.
    /// </summary>
    public float MaxYBound;

    /// <summary>
    /// Minimum X position within view of the camera.
    /// </summary>
    public float MinXBound;

    /// <summary>
    /// Minimum Y position within view of the camera.
    /// </summary>
    public float MinYBound;

    /// <summary>
    /// Constructor for the CameraBounds object. Initializes all data members to
    /// 0.
    /// </summary>
    public CameraBounds()
    {
        MaxXBound = 0;
        MaxYBound = 0;
        MinXBound = 0;
        MinYBound = 0;
    }
}
