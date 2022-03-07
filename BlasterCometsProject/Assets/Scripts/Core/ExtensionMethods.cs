using UnityEngine;

/// <summary>
/// Collection of extension methods for use in the BlasterComets project.
/// </summary>
public static class ExtensionMethods
{
    /// <summary>
    /// Calculates the bounds of an orthographic camera based on its 
    /// orthographic sized.
    /// </summary>
    /// <param name="camera">Camera for which bounds are calculated.</param>
    /// <param name="maxXBound">Maximum X position within view of the 
    /// camera.</param>
    /// <param name="maxYBound">Maximum Y position within view of the 
    /// camera.</param>
    /// <param name="minXBound">Minimum X position within view of the 
    /// camera.</param>
    /// <param name="minYBound">minimum Y position within view of the 
    /// camera.</param>
    public static void GetBounds(this Camera camera, out float maxXBound,
        out float maxYBound, out float minXBound, out float minYBound)
    {
        maxXBound = camera.transform.position.x +
            camera.orthographicSize;

        maxYBound = camera.transform.position.y +
            camera.orthographicSize;

        minXBound = camera.transform.position.x -
            camera.orthographicSize;

        minYBound = camera.transform.position.y -
            camera.orthographicSize;
    }

    /// <summary>
    /// Calculates the bounds of an orthographic camera based on its 
    /// orthographic sized.
    /// </summary>
    /// <param name="camera">Camera for which bounds are calculated.</param>
    /// <param name="maxXBound">Maximum X position within view of the 
    /// camera.</param>
    /// <param name="maxYBound">Maximum Y position within view of the 
    /// camera.</param>
    /// <param name="minXBound">Minimum X position within view of the 
    /// camera.</param>
    /// <param name="minYBound">minimum Y position within view of the 
    /// camera.</param>
    public static CameraBounds GetBounds(this Camera camera)
    {
        CameraBounds bounds = new CameraBounds();

        bounds.MaxXBound = camera.transform.position.x +
            camera.orthographicSize;

        bounds.MaxYBound = camera.transform.position.y +
            camera.orthographicSize;

        bounds.MinXBound = camera.transform.position.x -
            camera.orthographicSize;

        bounds.MinYBound = camera.transform.position.y -
            camera.orthographicSize;

        return bounds;
    }

    /// <summary>
    /// Returns a random position along the bounds of the camera's screen.
    /// </summary>
    /// <param name="camera">Camera for which bounds are calculated.</param>
    /// <returns>Vector3 representing a random position on the camera's 
    /// bounds.</returns>
    public static Vector3 GetRandomPositionOnBounds(this Camera camera)
    {
        camera.GetBounds(out float maxXBound, out float maxYBound,
           out float minXBound, out float minYBound);

        bool spawnOnXBound = Random.Range(0.0f, 1.0f) >= 0.5f;

        if (spawnOnXBound)
        {
            bool spawnOnLeftBound = Random.Range(0.0f, 1.0f) >= 0.5f;
            if (spawnOnLeftBound)
            {
                return new Vector3(minXBound, 
                    Random.Range(minYBound, maxYBound), 0);
            }
            else
            {
                return new Vector3(maxXBound, 
                    Random.Range(minYBound, maxYBound), 0);
            }
        }
        else
        {
            bool spawnOnTopBound = Random.Range(0.0f, 1.0f) >= 0.5f;
            if (spawnOnTopBound)
            {
                return new Vector3(Random.Range(minXBound, maxXBound), 
                    maxYBound, 0);
            }
            else
            {
                return new Vector3(Random.Range(minXBound, maxXBound),
                    minYBound, 0);
            }
        }
    }
}
