using UnityEngine;

/// <summary>
/// Stores the maximum and minimum postion coordinates bounded by an 
/// orthographic camera's screen.
/// </summary>
[CreateAssetMenu]
public class CameraBounds : ScriptableObject
{
    /// <summary>
    /// Maximum X position within view of the camera.
    /// </summary>
    [Tooltip("Maximum X position within view of the camera.")]
    public float MaxXBound;

    /// <summary>
    /// Maximum Y position within view of the camera.
    /// </summary>
    [Tooltip("Maximum Y position within view of the camera.")]
    public float MaxYBound;

    /// <summary>
    /// Minimum X position within view of the camera.
    /// </summary>
    [Tooltip("Minimum X position within view of the camera.")]
    public float MinXBound;

    /// <summary>
    /// Minimum Y position within view of the camera.
    /// </summary>
    [Tooltip("Minimum Y position within view of the camera.")]
    public float MinYBound;

    /// <summary>
    /// Returns a random position along the bounds of the camera's screen.
    /// </summary>
    /// <returns>Vector3 representing a random position on the camera's 
    /// bounds.</returns>
    public Vector3 GetRandomPositionOn()
    {
        bool spawnOnXBound = Random.Range(0.0f, 1.0f) >= 0.5f;

        if (spawnOnXBound)
        {
            bool spawnOnLeftBound = Random.Range(0.0f, 1.0f) >= 0.5f;
            if (spawnOnLeftBound)
            {
                return new Vector3(MinXBound,
                    Random.Range(MinYBound, MaxYBound), 0);
            }
            else
            {
                return new Vector3(MaxXBound,
                    Random.Range(MinYBound, MaxYBound), 0);
            }
        }
        else
        {
            bool spawnOnTopBound = Random.Range(0.0f, 1.0f) >= 0.5f;
            if (spawnOnTopBound)
            {
                return new Vector3(Random.Range(MinXBound, MaxXBound),
                    MaxYBound, 0);
            }
            else
            {
                return new Vector3(Random.Range(MinXBound, MaxXBound),
                    MinYBound, 0);
            }
        }
    }

    /// <summary>
    /// Returns a random position within the bounds of the camera's screen.
    /// </summary>
    /// <returns>Vector3 representing a random position within the camera's 
    /// bounds.</returns>
    public Vector3 GetRandomPositionWithin()
    {
        return new Vector3(Random.Range(MinXBound, MaxXBound), 
            Random.Range(MinYBound, MaxYBound), 0);
    }
}
