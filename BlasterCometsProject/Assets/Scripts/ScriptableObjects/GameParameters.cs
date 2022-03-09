using UnityEngine;

/// <summary>
/// Collection of all parameters used in the game.
/// </summary>
[CreateAssetMenu]
public class GameParameters : ScriptableObject
{
    #region Bogey Parameters
    /// <summary>
    /// When targeting the ship, the angle of the cone the bogey fires in.
    /// </summary>
    [Header("Combat")]
    [Header("Bogey Parameters")]
    [Tooltip("When targeting the ship, the angle of the cone the bogey " +
        "fires in.")]
    public float BogeyFireAngle = 60;

    /// <summary>
    /// Time between projectiles fired by bogeys.
    /// </summary>
    [Tooltip("Time between projectiles fired by bogeys.")]
    public float BogeyFireRate = 0.5f;

    /// <summary>
    /// Determines how long the bogey's projectile stays active for after it 
    /// is fired.
    /// </summary>
    [Tooltip("Determines how long the bogey's projectile stays active for " +
        "after it is fired.")]
    public float BogeyProjectileLifeTime = 0.9f;

    /// <summary>
    /// Determines how fast the bogey's projectile moves when fired.
    /// </summary>
    [Tooltip("Determines how fast the bogey's projectile moves when fired.")]
    public float BogeyProjectileTravelSpeed = 10;

    /// <summary>
    /// Speed at which the large bogey will move. The small bogey will always
    /// move at 1.5x this speed.
    /// </summary>
    [Header("Movement")]
    [Tooltip("Speed at which the bogey will move. The small bogey will " +
        "always move at 1.5x this speed.")]
    public float BogeyMoveSpeed = 1;

    /// <summary>
    /// Range of time a bogey will spend moving in a single direction.
    /// </summary>
    [Tooltip("What range of time a bogey will spend moving in a single " +
        "direction?")]
    public Vector2 BogeyMoveTimeRange = new Vector2(3, 5);

    /// <summary>
    /// How many points can the player get before large bogeys spawn?
    /// </summary>
    [Header("Spawning")]
    [Tooltip("How many points can the player get before large bogeys spawn?")]
    public float BogeyLargeSpawnThreshold = 2000;

    /// <summary>
    /// How long does the bogey last before retreating?
    /// </summary>
    [Tooltip("How long does the bogey last before retreating?")]
    public float BogeyLifetime = 10;

    /// <summary>
    /// How many points can the player get before small bogeys spawn?
    /// </summary>
    [Tooltip("How many points can the player get before small bogeys spawn?")]
    public float BogeySmallSpawnThreshold = 10000;

    /// <summary>
    /// How many points can the player get before only small bogeys spawn?
    /// </summary>
    [Tooltip("How many points can the player get before only small " + 
        "bogeys spawn?")]
    public float BogeyOnlySmallSpawnThreshold = 40000;

    /// <summary>
    /// Range of time a bogey will take to respawn.
    /// </summary>
    [Tooltip("Range of time a bogey will take to respawn.")]
    public Vector2 BogeySpawnDelayRange = new Vector2(5, 10);
    #endregion

    #region Meteoroid Parameters
    /// <summary>
    /// Number of large meteoroids to spawn.
    /// </summary>
    [Header("Counts")]
    [Header("Meteoroid Parameters")]
    [Tooltip("Number of large meteoroids to spawn.")]
    public int MeteoroidLargeCount = 1;

    /// <summary>
    /// Number of small meteoroids spawned when a medium meteoroid is destroyed.
    /// </summary>
    [Tooltip("Number of medium meteoroids spawned when a large meteoroid " +
        "is destroyed.")]
    public int MeteoroidMediumCount = 2;

    /// <summary>
    /// Number of small meteoroids spawned when a medium meteoroid is destroyed.
    /// </summary>
    [Tooltip("Number of small meteoroids spawned when a medium meteoroid " +
        "is destroyed.")]
    public int MeteoroidSmallCount = 2;

    /// <summary>
    /// What range of speeds can a meteoroid travel?
    /// </summary>
    [Header("Speed")]
    [Tooltip("What range of speeds can a meteoroid travel?")]
    public Vector2 MeteoroidTravelSpeedRange = new Vector2(0.5f, 1.5f);

    /// <summary>
    /// How much faster does the next smallest meteoroid travel?
    /// </summary>
    [Tooltip("How much faster does the next smallest meteoroid travel?")]
    public float MeteoroidTravelSpeedMultiplier = 1.5f;
    #endregion

    #region Ship Parameters
    /// <summary>
    /// Minimum time between projectiles fired by the ship.
    /// </summary>
    [Header("Combat")]
    [Header("Ship Parameters")]
    [Tooltip("Minimum time between projectiles fired by the ship.")]
    public float ShipFireCooldown = 0.5f;

    /// <summary>
    /// How long the ship must wait between hyperspace entries.
    /// </summary>
    [Tooltip("How long the ship must wait between hyperspace entries.")]
    public float ShipHyperspaceCooldown = 1.0f;

    /// <summary>
    /// How long the ship remains in hyperspace.
    /// </summary>
    [Tooltip("How long the ship remains in hyperspace.")]
    public float ShipHyperspaceInTime = 1.0f; 

    /// <summary>
    /// Determines how long the ship's projectile stays active for after it 
    /// is fired.
    /// </summary>
    [Tooltip("Determines how long the ship's projectile stays active for " +
        "after it is fired.")]
    public float ShipProjectileLifeTime = 0.9f;

    /// <summary>
    /// Determines how fast the ship's projectile moves when fired.
    /// </summary>
    [Tooltip("Determines how fast the ship's projectile moves when fired.")]
    public float ShipProjectileTravelSpeed = 10;

    /// <summary>
    /// Max speed at which the GameObject can travel.
    /// </summary>
    [Header("Movement")]
    [Tooltip("Max speed at which the GameObject can travel.")]
    public float ShipMaxSpeed = 10;

    /// <summary>
    /// Determines how quickly the ship rotates.
    /// </summary>
    [Tooltip("Determines how quickly the ship rotates.")]
    public float ShipRotationSpeed = 100;

    /// <summary>
    /// Force applied per tick when thruster is active.
    /// </summary>
    [Tooltip("Force applied per tick when thruster is active.")]
    public float ShipThrustForce = 500;

    /// <summary>
    /// How long it takes for the ship to respawn.
    /// </summary>
    [Header("Spawning")]
    [Tooltip("How long it takes for the ship to respawn.")]
    public float ShipRespawnTime = 3f;

    /// <summary>
    /// Number of lives the ship starts with.
    /// </summary>
    [Tooltip("Number of lives the ship starts with.")]
    public int ShipStartingLives = 3;
    #endregion
}
