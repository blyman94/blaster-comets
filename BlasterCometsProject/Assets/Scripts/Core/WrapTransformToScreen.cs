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
    /// Bounds of the main camera.
    /// </summary>
    [Header("General")]
    [Tooltip("Bounds of the main camera.")]
    [SerializeField] private CameraBounds cameraBounds;

    /// <summary>
    /// Collection of renders that determine if the GameObject is currently 
    /// visible to the camera. Should include all 2D sprite renders that make 
    /// up the GameObject.
    /// </summary>
   
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

    #region MonoBehaviour Methods
    private void Update()
    {
        ScreenWrap();
    }
    #endregion

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
        if (!isWrappingX && (newPosition.x >= cameraBounds.MaxXBound ||
            newPosition.x <= cameraBounds.MinXBound))
        {
            newPosition.x = -newPosition.x;
            isWrappingX = true;
        }

        if (!isWrappingY && (newPosition.y >= cameraBounds.MaxYBound ||
            newPosition.y <= cameraBounds.MinYBound))
        {
            newPosition.y = -newPosition.y;
            isWrappingY = true;
        }

        transform.position = newPosition;
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
