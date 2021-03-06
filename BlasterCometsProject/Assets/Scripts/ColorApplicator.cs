using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Applies a color palette to the game.
/// </summary>
public class ColorApplicator : MonoBehaviour
{
    /// <summary>
    /// The game's settings.
    /// </summary>
    [Tooltip("The game's settings.")]
    [SerializeField] private Settings settings;

    /// <summary>
    /// Color Palette to apply.
    /// </summary>
    [Header("General")]
    [Tooltip("Color Palette to apply.")]
    [SerializeField] private ColorPalette colorPalette;

    /// <summary>
    /// Text of the button used to select color palettes.
    /// </summary>
    [Tooltip("Text of the button used to select color palettes.")]
    [SerializeField] private TextMeshProUGUI colorSelectorText;

    /// <summary>
    /// Palettes to cycle through through UI.
    /// </summary>
    [Tooltip("Palettes to cycle through through UI.")]
    [SerializeField] private List<ColorPalette> paletteCycle;

    /// <summary>
    /// Camera to color background.
    /// </summary>
    [Header("Objects to Color")]
    [Tooltip("Camera to color background.")]
    [SerializeField] private Camera mainCamera;

    /// <summary>
    /// Menu backgrounds to color.
    /// </summary>
    [Tooltip("Menu backgrounds to color.")]
    [SerializeField] private Image[] menuBackgrounds;

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

    /// <summary>
    /// Material for menu headers.
    /// </summary>
    [Header("UI Materials")]
    [Tooltip("Material for menu headers.")]
    [SerializeField] private Material headerMaterial;

    /// <summary>
    /// Material for menu button text.
    /// </summary>
    [Tooltip("Material for menu button text.")]
    [SerializeField] private Material menuButtonTextMaterial;

    /// <summary>
    /// Material for menu content text.
    /// </summary>
    [Tooltip("Material for menu content text.")]
    [SerializeField] private Material menuContentTextMaterial;

    /// <summary>
    /// Material for the player ship when represented in the UI.
    /// </summary>
    [Tooltip("Material for the player ship when represented in the UI.")]
    [SerializeField] private Material shipUIMaterial;

    /// <summary>
    /// Index of the active color palette.
    /// </summary>
    private int currentPaletteIndex = -1;

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
        ColorPalette = settings.CurrentColorPalette;
    }
    #endregion

    /// <summary>
    /// Switch to next color palette.
    /// </summary>
    public void NextColorPalette()
    {
        if (currentPaletteIndex == -1)
        {
            currentPaletteIndex = paletteCycle.IndexOf(colorPalette);
        }

        if (currentPaletteIndex + 1 < paletteCycle.Count)
        {
            currentPaletteIndex += 1;
        }
        else
        {
            currentPaletteIndex = 0;
        }

        ColorPalette = paletteCycle[currentPaletteIndex];
    }

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
        ApplyUIColors();

        colorSelectorText.text = ColorPalette.Name;
        settings.CurrentColorPalette = ColorPalette;
    }

    /// <summary>
    /// Applies colors to bogey materials.
    /// </summary>
    private void ApplyBogeyColors()
    {
        largeBogeyMaterial.SetColor("_BaseColor",
            ColorPalette.LargeBogeyColor);
        largeBogeyMaterial.SetColor("_EmissionColor",
            ColorPalette.LargeBogeyColor);
            
        smallBogeyMaterial.SetColor("_BaseColor",
            ColorPalette.LargeBogeyColor);
        smallBogeyMaterial.SetColor("_EmissionColor",
            ColorPalette.LargeBogeyColor);
    }

    /// <summary>
    /// Applies color to the camera background.
    /// </summary>
    private void ApplyCameraBackgroundColor()
    {
        mainCamera.backgroundColor = ColorPalette.BackgroundColor;

        foreach (Image background in menuBackgrounds)
        {
            background.color = ColorPalette.BackgroundColor;
        }
    }

    /// <summary>
    /// Applies colors to explosion materials.
    /// </summary>
    private void ApplyExplosionColors()
    {
        bogeyExplosionMaterial.SetColor("_BaseColor",
            ColorPalette.BogeyExplosionColor);
        bogeyExplosionMaterial.SetColor("_EmissionColor",
            ColorPalette.BogeyExplosionColor);

        meteoroidExplosionMaterial.SetColor("_BaseColor",
            ColorPalette.MeteoroidExplosionColor);
        meteoroidExplosionMaterial.SetColor("_EmissionColor",
            ColorPalette.MeteoroidExplosionColor);

        shipExplosionMaterial.SetColor("_BaseColor",
            ColorPalette.ShipExplosionColor);
        shipExplosionMaterial.SetColor("_EmissionColor",
            ColorPalette.ShipExplosionColor);
    }

    /// <summary>
    /// Applies colors to meteoroid materials.
    /// </summary>
    private void ApplyMeteoroidColors()
    {
        largeMeteoroidMaterial.SetColor("_BaseColor",
            ColorPalette.LargeMeteoroidColor);
        largeMeteoroidMaterial.SetColor("_EmissionColor",
            ColorPalette.LargeMeteoroidColor);

        mediumMeteoroidMaterial.SetColor("_BaseColor",
            ColorPalette.MediumMeteoroidColor);
        mediumMeteoroidMaterial.SetColor("_EmissionColor",
            ColorPalette.MediumMeteoroidColor);

        smallMeteoroidMaterial.SetColor("_BaseColor",
            ColorPalette.SmallMeteoroidColor);
        smallMeteoroidMaterial.SetColor("_EmissionColor",
            ColorPalette.SmallMeteoroidColor);
    }

    /// <summary>
    /// Applies colors to projectile materials.
    /// </summary>
    private void ApplyProjectileColors()
    {
        projectileBogeyMaterial.SetColor("_BaseColor",
            ColorPalette.ProjectileBogeyColor);
        projectileBogeyMaterial.SetColor("_EmissionColor",
            ColorPalette.ProjectileBogeyColor);

        projectileFriendlyMaterial.SetColor("_BaseColor",
            ColorPalette.ProjectileFriendlyColor);
        projectileFriendlyMaterial.SetColor("_EmissionColor",
            ColorPalette.ProjectileFriendlyColor);
    }

    /// <summary>
    /// Applies colors to ship materials.
    /// </summary>
    private void ApplyShipColors()
    {
        shipMaterial.SetColor("_BaseColor", ColorPalette.ShipColor);
        shipMaterial.SetColor("_EmissionColor", ColorPalette.ShipColor);

        thrusterMaterial.SetColor("_BaseColor",
            ColorPalette.ThrusterColor);
        thrusterMaterial.SetColor("_EmissionColor",
            ColorPalette.ThrusterColor);
    }

    /// <summary>
    /// Applies colors to UI materials.
    /// </summary>
    private void ApplyUIColors()
    {
        headerMaterial.SetColor("_FaceColor", ColorPalette.HeaderColor);

        menuButtonTextMaterial.SetColor("_FaceColor", 
            ColorPalette.MenuButtonTextColor);
        menuContentTextMaterial.SetColor("_FaceColor", 
            ColorPalette.MenuContentTextColor);
        
        shipUIMaterial.SetColor("_BaseColor", ColorPalette.ShipColor);
        shipUIMaterial.SetColor("_EmissionColor", ColorPalette.ShipColor);
    }
}
