using UnityEngine;

/// <summary>
/// Collection of extension methods for use in the BlasterComets project.
/// </summary>
public static class ExtensionMethods
{
    #region Camera Extension Methods
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
    #endregion
}
