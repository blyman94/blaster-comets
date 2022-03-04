using UnityEngine;

/// <summary>
/// GameObjects with this component will exhbit "screen wrapping" - if the 
/// GameObject's transform leaves the screen bounds, it will be teleported to 
/// the other side making the plane was continuous and infinite. Adapted from 
/// Damir Veapi's post on gamedevelopment.tutsplus.com.
/// 
/// Article link:
/// https://gamedevelopment.tutsplus.com/articles/create-an-asteroids-like-screen-wrapping-effect-with-unity--gamedev-15055
/// 
/// </summary>
public class WrapTransformToScreen : MonoBehaviour
{
    /// <summary>
    /// Collection of renders that determine if the GameObject is currently 
    /// visible to the camera. Should include all 2D sprite renders that make 
    /// up the GameObject.
    /// </summary>
    [Header("General")]
    [Tooltip("Collection of renders that determine if the GameObject is " +
        "currently visible to the camera. Should include all sprite " +
        "renderers that make up the GameObject.")]
    [SerializeField] private SpriteRenderer[] visibilityRenderers;

    /// <summary>
    /// Control variable for wrapping on the X axis. Ensures only one wrap
    /// occurs at a time.
    /// </summary>
    private bool isWrappingX = false;

    /// <summary>
    /// Control variable for wrapping on the Y axis. Ensures only one wrap
    /// occurs at a time.
    /// </summary>
    private bool isWrappingY = false;

    /// <summary>
    /// Camera determining the bounds for screen wrapping.
    /// </summary>
    private Camera mainCamera;

    /// <summary>
    /// Represents the rightmost camera bound.
    /// </summary>
    private float maxXBound;

    /// <summary>
    /// Represents the topmost camera bound.
    /// </summary>
    private float maxYBound;

    /// <summary>
    /// Represents the leftmost camera bound.
    /// </summary>
    private float minXBound;

    /// <summary>
    /// Represents the bottommost camera bound.
    /// </summary>
    private float minYBound;

    #region MonoBehaviour Methods
    private void Start()
    {
        mainCamera = Camera.main;
        CalculateCameraBounds();
    }
    private void Update()
    {
        ScreenWrap();
    }

    /// <summary>
    /// If the GameObject's transform leaves the screen bounds, it will be 
    /// teleported to the other side making the plane was continuous and 
    /// infinite.
    /// </summary>
    private void ScreenWrap()
    {
        bool isVisible = CheckVisibility();

        if (isVisible)
        {
            isWrappingX = false;
            isWrappingY = false;
            return;
        }

        if (isWrappingX && isWrappingY)
        {
            return;
        }

        Vector3 newPosition = transform.position;
        if (!isWrappingX && (newPosition.x >= maxXBound ||
            newPosition.x <= minXBound))
        {
            newPosition.x = -newPosition.x;
            isWrappingX = true;
        }

        if (!isWrappingY && (newPosition.y >= maxYBound ||
            newPosition.y <= minYBound))
        {
            newPosition.y = -newPosition.y;
            isWrappingY = true;
        }

        transform.position = newPosition;
    }
    #endregion

    /// <summary>
    /// Calculates camera bounds for screen wrapping functionality.
    /// </summary>
    private void CalculateCameraBounds()
    {
        maxXBound = mainCamera.transform.position.x +
            mainCamera.orthographicSize;

        maxYBound = mainCamera.transform.position.y +
            mainCamera.orthographicSize;

        minXBound = mainCamera.transform.position.x -
            mainCamera.orthographicSize;

        minYBound = mainCamera.transform.position.y -
            mainCamera.orthographicSize;
    }

    /// <summary>
    /// Determines if the GameObject is currently visible to the camera based on
    /// the visibility of the sprite renderers that compose the GameObject. 
    /// </summary>
    /// <returns>True if at least one of the GameObject's visibility renderers 
    /// are visible.</returns>
    private bool CheckVisibility()
    {
        if (visibilityRenderers.Length > 0)
        {
            foreach (SpriteRenderer renderer in visibilityRenderers)
            {
                if (renderer.isVisible)
                {
                    return true;
                }
            }
        }
        
        return false;
    }
}
