using UnityEngine;

/// <summary>
/// Applies a color palette to the game.
/// </summary>
public class ColorApplicator : MonoBehaviour
{
    /// <summary>
    /// Color Palette to apply.
    /// </summary>
    [Tooltip("Color Palette to apply.")]
    [SerializeField] private ColorPalette colorPalette;

    /// <summary>
    /// Camera to color background.
    /// </summary>
    [Header("Objects to Color")]
    [Tooltip("Camera to color background.")]
    [SerializeField] private Camera mainCamera;

    /// <summary>
    /// Material for the large bogey.
    /// </summary>
    [Header("Bogey Materials")]
    [Tooltip("Material for the large bogey.")]
    [SerializeField] private Material largeBogeyMaterial;

    /// <summary>
    /// Material for the small bogey.
    /// </summary>
    [Tooltip("Material for the small bogey.")]
    [SerializeField] private Material smallBogeyMaterial;

    /// <summary>
    /// Material for the bogey explosion.
    /// </summary>
    [Header("Explosion Materials")]
    [Tooltip("Material for the bogey explosion.")]
    [SerializeField] private Material bogeyExplosionMaterial;

    /// <summary>
    /// Material for the meteoroid explosion.
    /// </summary>
    [Tooltip("Material for the meteoroid explosion.")]
    [SerializeField] private Material meteoroidExplosionMaterial;

    /// <summary>
    /// Material for the ship explosion.
    /// </summary>
    [Tooltip("Material for the ship explosion.")]
    [SerializeField] private Material shipExplosionMaterial;

    /// <summary>
    /// Material for the large Meteoroid
    /// </summary>
    [Header("Meteoroid Materials")]
    [Tooltip("Material for the large meteoroid.")]
    [SerializeField] private Material largeMeteoroidMaterial;

    /// <summary>
    /// Material for the medium meteoroid.
    /// </summary>
    [Tooltip("Material for the medium meteoroid.")]
    [SerializeField] private Material mediumMeteoroidMaterial;

    /// <summary>
    /// Material for the small meteoroid.
    /// </summary>
    [Tooltip("Material for the small meteoroid.")]
    [SerializeField] private Material smallMeteoroidMaterial;

    /// <summary>
    /// Material for the bogey projectile.
    /// </summary>
    [Header("Projectile Materials")]
    [Tooltip("Material for the bogey projectile.")]
    [SerializeField] private Material projectileBogeyMaterial;

    /// <summary>
    /// Material for the friendly projectile.
    /// </summary>
    [Tooltip("Material for the friendly projectile.")]
    [SerializeField] private Material projectileFriendlyMaterial;

    /// <summary>
    /// Material for the player ship.
    /// </summary>
    [Header("Ship Materials")]
    [Tooltip("Material for the player ship.")]
    [SerializeField] private Material shipMaterial;

    /// <summary>
    /// Material for the thruster.
    /// </summary>
    [Tooltip("Material for the thruster.")]
    [SerializeField] private Material thrusterMaterial;

    #region Properties
    private ColorPalette ColorPalette
    {
        get
        {
            return colorPalette;
        }
        set
        {
            colorPalette = value;
            ApplyColors();
        }
    }
    #endregion

    #region MonoBehaviour Methods
    private void Start()
    {
        ApplyColors();
    }
    #endregion

    /// <summary>
    /// Applies color palette to available objects or materials.
    /// </summary>
    public void ApplyColors()
    {
        ApplyBogeyColors();
        ApplyCameraBackgroundColor();
        ApplyExplosionColors();
        ApplyMeteoroidColors();
        ApplyProjectileColors();
        ApplyShipColors();
    }

    /// <summary>
    /// Applies color to the camera background.
    /// </summary>
    private void ApplyCameraBackgroundColor()
    {
        if (mainCamera != null)
        {
            mainCamera.backgroundColor = ColorPalette.BackgroundColor;
        }
    }

    /// <summary>
    /// Applies colors to bogey materials.
    /// </summary>
    private void ApplyBogeyColors()
    {
        if (largeBogeyMaterial != null)
        {
            largeBogeyMaterial.SetColor("_BaseColor", 
                ColorPalette.LargeBogeyColor);
            largeBogeyMaterial.SetColor("_EmissionColor", 
                ColorPalette.LargeBogeyColor);
        }
        if (smallBogeyMaterial != null)
        {
            smallBogeyMaterial.SetColor("_BaseColor", 
                ColorPalette.LargeBogeyColor);
            smallBogeyMaterial.SetColor("_EmissionColor", 
                ColorPalette.LargeBogeyColor);
        }
    }

    /// <summary>
    /// Applies colors to explosion materials.
    /// </summary>
    private void ApplyExplosionColors()
    {
        if (bogeyExplosionMaterial != null)
        {
            bogeyExplosionMaterial.SetColor("_BaseColor", 
                ColorPalette.BogeyExplosionColor);
            bogeyExplosionMaterial.SetColor("_EmissionColor", 
                ColorPalette.BogeyExplosionColor);
        }

        if (meteoroidExplosionMaterial != null)
        {
            meteoroidExplosionMaterial.SetColor("_BaseColor",
                ColorPalette.MeteoroidExplosionColor);
            meteoroidExplosionMaterial.SetColor("_EmissionColor", 
                ColorPalette.MeteoroidExplosionColor);
        }

        if (shipExplosionMaterial != null)
        {
            shipExplosionMaterial.SetColor("_BaseColor", 
                ColorPalette.ShipExplosionColor);
            shipExplosionMaterial.SetColor("_EmissionColor", 
                ColorPalette.ShipExplosionColor);
        }
    }

    /// <summary>
    /// Applies colors to meteoroid materials.
    /// </summary>
    private void ApplyMeteoroidColors()
    {
        if (largeMeteoroidMaterial != null)
        {
            largeMeteoroidMaterial.SetColor("_BaseColor", 
                ColorPalette.LargeMeteoroidColor);
            largeMeteoroidMaterial.SetColor("_EmissionColor", 
                ColorPalette.LargeMeteoroidColor);
        }
        if (mediumMeteoroidMaterial != null)
        {
            mediumMeteoroidMaterial.SetColor("_BaseColor", 
                ColorPalette.MediumMeteoroidColor);
            mediumMeteoroidMaterial.SetColor("_EmissionColor", 
                ColorPalette.MediumMeteoroidColor);
        }
        if (smallMeteoroidMaterial != null)
        {
            smallMeteoroidMaterial.SetColor("_BaseColor", 
                ColorPalette.SmallMeteoroidColor);
            smallMeteoroidMaterial.SetColor("_EmissionColor", 
                ColorPalette.SmallMeteoroidColor);
        }
    }

    /// <summary>
    /// Applies colors to projectile materials.
    /// </summary>
    private void ApplyProjectileColors()
    {
        if (projectileBogeyMaterial != null)
        {
            projectileBogeyMaterial.SetColor("_BaseColor", 
                ColorPalette.ProjectileBogeyColor);
            projectileBogeyMaterial.SetColor("_EmissionColor", 
                ColorPalette.ProjectileBogeyColor);
        }

        if (projectileFriendlyMaterial != null)
        {
            projectileFriendlyMaterial.SetColor("_BaseColor", 
                ColorPalette.ProjectileFriendlyColor);
            projectileFriendlyMaterial.SetColor("_EmissionColor", 
                ColorPalette.ProjectileFriendlyColor);
        }
    }

    /// <summary>
    /// Applies colors to ship materials.
    /// </summary>
    private void ApplyShipColors()
    {
        if (shipMaterial != null)
        {
            shipMaterial.SetColor("_BaseColor", ColorPalette.ShipColor);
            shipMaterial.SetColor("_EmissionColor", ColorPalette.ShipColor);
        }
        if (thrusterMaterial != null)
        {
            thrusterMaterial.SetColor("_BaseColor", 
                ColorPalette.ThrusterColor);
            thrusterMaterial.SetColor("_EmissionColor", 
                ColorPalette.ThrusterColor);
        }
    }
}
