using UnityEngine;

/// <summary>
/// Exposes the Debug.Log method so it may be called from a Unity Event.
/// </summary>
public class DebugLogger : MonoBehaviour
{
    /// <summary>
    /// Calls Debug.Log and displays the passed message.
    /// </summary>
    /// <param name="message">Message to be logged.</param>
    public void Log(string message)
    {
        Debug.Log(message);
    }
}
