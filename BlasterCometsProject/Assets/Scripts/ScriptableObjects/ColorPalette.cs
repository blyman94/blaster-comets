using UnityEngine;

/// <summary>
/// Dictates the color of each GameObject.
/// </summary>
[CreateAssetMenu]
public class ColorPalette : ScriptableObject
{
    /// <summary>
    /// The name of the color palette.
    /// </summary>
    [Tooltip("The name of the color palette.")]
    public string Name;

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
    [ColorUsage(true,true)]
    public Color BogeyExplosionColor = Color.white;

    /// <summary>
    /// Color of the meteoroid explosion.
    /// </summary>
    [Tooltip("Color of the meteoroid explosion.")]
    [ColorUsage(true, true)]
    public Color MeteoroidExplosionColor = Color.white;

    /// <summary>
    /// Color of the ship explosion.
    /// </summary>
    [Tooltip("Color of the ship explosion.")]
    [ColorUsage(true, true)]
    public Color ShipExplosionColor = Color.white;

    /// <summary>
    /// Color of the large bogey.
    /// </summary>
    [Header("Bogey Colors")]
    [Tooltip("Color of the large bogey.")]
    [ColorUsage(true, true)]
    public Color LargeBogeyColor = Color.white;

    /// <summary>
    /// Color of the Small bogey.
    /// </summary>
    [Tooltip("Color of the small bogey.")]
    [ColorUsage(true, true)]
    public Color SmallBogeyColor = Color.white;

    /// <summary>
    /// Color of the large meteoroid.
    /// </summary>
    [Header("Meteoroid Colors")]
    [Tooltip("Color of the large meteoroid.")]
    [ColorUsage(true, true)]
    public Color LargeMeteoroidColor = Color.white;

    /// <summary>
    /// Color of the medium meteoroid.
    /// </summary>
    [Tooltip("Color of the medium meteoroid.")]
    [ColorUsage(true, true)]
    public Color MediumMeteoroidColor = Color.white;

    /// <summary>
    /// Color of the small meteoroid.
    /// </summary>
    [Tooltip("Color of the small meteoroid.")]
    [ColorUsage(true, true)]
    public Color SmallMeteoroidColor = Color.white;

    /// <summary>
    /// Color of the bogey projectile.
    /// </summary>
    [Header("Projectile Colors")]
    [Tooltip("Color of the bogey projectile.")]
    [ColorUsage(true, true)]
    public Color ProjectileBogeyColor = Color.white;

    /// <summary>
    /// Color of the friendly projectile.
    /// </summary>
    [Tooltip("Color of the friendly projectile.")]
    [ColorUsage(true, true)]
    public Color ProjectileFriendlyColor = Color.white;

    /// <summary>
    /// Color of the player ship
    /// </summary>
    [Header("Ship Colors")]
    [Tooltip("Color of the player ship.")]
    [ColorUsage(true, true)]
    public Color ShipColor = Color.white;

    /// <summary>
    /// Color of the thruster.
    /// </summary>
    [Tooltip("Color of the thruster.")]
    [ColorUsage(true, true)]
    public Color ThrusterColor = Color.white;

    /// <summary>
    /// Color of menu headers.
    /// </summary>
    [Header("UI Colors")]
    [Tooltip("Color of menu headers.")]
    [ColorUsage(true, true)]
    public Color HeaderColor = Color.white;

    /// <summary>
    /// Color of menu button text.
    /// </summary>
    [Tooltip("Color of menu button text.")]
    [ColorUsage(true, true)]
    public Color MenuButtonTextColor = Color.white;

    /// <summary>
    /// Color of menu content text.
    /// </summary>
    [Tooltip("Color of menu content text.")]
    [ColorUsage(true, true)]
    public Color MenuContentTextColor = Color.white;
}
