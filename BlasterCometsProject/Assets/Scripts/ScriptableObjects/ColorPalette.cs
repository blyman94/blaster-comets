using UnityEngine;

/// <summary>
/// Dictates the color of each GameObject.
/// </summary>
[CreateAssetMenu]
public class ColorPalette : ScriptableObject
{
    /// <summary>
    /// Name of the color Palette.
    /// </summary>
    [Tooltip("Name of the color Palette.")]
    public string Name = "New Color Palette";
    
    /// <summary>
    /// Color of the background.
    /// </summary>
    [Header("General Colors")]
    [Tooltip("Color of the background.")]
    public Color BackgroundColor = Color.black;

    /// <summary>
    /// Color of the bogey explosion.
    /// </summary>
    [Header("Explosion Colors")]
    [Tooltip("Color of the bogey explosion.")]
    public Color BogeyExplosionColor = Color.white;

    /// <summary>
    /// Color of the meteoroid explosion.
    /// </summary>
    [Tooltip("Color of the meteoroid explosion.")]
    public Color MeteoroidExplosionColor = Color.white;

    /// <summary>
    /// Color of the ship explosion.
    /// </summary>
    [Tooltip("Color of the ship explosion.")]
    public Color ShipExplosionColor = Color.white;

    /// <summary>
    /// Color of the large bogey.
    /// </summary>
    [Header("Bogey Colors")]
    [Tooltip("Color of the large bogey.")]
    public Color LargeBogeyColor = Color.white;

    /// <summary>
    /// Color of the Small bogey.
    /// </summary>
    [Tooltip("Color of the small bogey.")]
    public Color SmallBogeyColor = Color.white;

    /// <summary>
    /// Color of the large meteoroid.
    /// </summary>
    [Header("Meteoroid Colors")]
    [Tooltip("Color of the large meteoroid.")]
    public Color LargeMeteoroidColor = Color.white;

    /// <summary>
    /// Color of the medium meteoroid.
    /// </summary>
    [Tooltip("Color of the medium meteoroid.")]
    public Color MediumMeteoroidColor = Color.white;

    /// <summary>
    /// Color of the small meteoroid.
    /// </summary>
    [Tooltip("Color of the small meteoroid.")]
    public Color SmallMeteoroidColor = Color.white;

    /// <summary>
    /// Color of the bogey projectile.
    /// </summary>
    [Header("Projectile Colors")]
    [Tooltip("Color of the bogey projectile.")]
    public Color ProjectileBogeyColor = Color.white;

    /// <summary>
    /// Color of the friendly projectile.
    /// </summary>
    [Tooltip("Color of the friendly projectile.")]
    public Color ProjectileFriendlyColor = Color.white;

    /// <summary>
    /// Color of the player ship
    /// </summary>
    [Header("Ship Colors")]
    [Tooltip("Color of the player ship.")]
    public Color ShipColor = Color.white;

    /// <summary>
    /// Color of the thruster.
    /// </summary>
    [Tooltip("Color of the thruster.")]
    public Color ThrusterColor = Color.white;

    /// <summary>
    /// Color of the header UI.
    /// </summary>
    [Header("UI Colors")]
    [Tooltip("Color of the header UI.")]
    public Color HeaderColor = Color.white;

    /// <summary>
    /// Color of the label UI.
    /// </summary>
    [Tooltip("Color of the label UI.")]
    public Color LabelColor = Color.white;
}
