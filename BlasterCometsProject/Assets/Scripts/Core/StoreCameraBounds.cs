using UnityEngine;

/// <summary>
/// Calculates and makes public the maximum postion coordinates bounded by an 
/// orthographic camera's screen.
/// </summary>
public class StoreCameraBounds : MonoBehaviour
{
    /// <summary>
    /// ScriptableObject for camera Bounds storage.
    /// </summary>
    [Tooltip("ScriptableObject for CameraBounds storage.")]
    [SerializeField] private CameraBounds cameraBounds;

    /// <summary>
    /// Main camera reference.
    /// </summary>
    private Camera mainCamera;

    #region MonoBehaviour Methods
    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        GetAndStoreBounds();
    }

    /// <summary>
    /// Gets the main camera's bounds and stores them in a ScriptableObject.
    /// </summary>
    private void GetAndStoreBounds()
    {
        mainCamera.GetBounds(out float maxXBound, out float maxYBound,
                    out float minXBound, out float minYBound);

        cameraBounds.MaxXBound = maxXBound;
        cameraBounds.MaxYBound = maxYBound;
        cameraBounds.MinXBound = minXBound;
        cameraBounds.MinYBound = minYBound;
    }
    #endregion
}
