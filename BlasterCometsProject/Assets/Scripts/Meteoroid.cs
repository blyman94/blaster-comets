using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Objects that act as a hazard to the player, and grants the player points
/// when they are destroyed.
/// </summary>
public class Meteoroid : MonoBehaviour, IPoolObject
{
    /// <summary>
    /// Rigidbody2D component of the projectile.
    /// </summary>
    [Header("General")]
    [Tooltip("Rigidbody2D component of the projectile.")]
    [SerializeField] private new Rigidbody2D rigidbody2D;

    /// <summary>
    /// Bounds of the main camera.
    /// </summary>
    [Header("General")]
    [Tooltip("Bounds of the main camera.")]
    [SerializeField] private CameraBounds cameraBounds;

    /// <summary>
    /// How far from the center of the screen can this meteoroid be before it's
    /// teleported back to the camera boundary?
    /// </summary>
    [Tooltip("How far from the center of the screen can this meteoroid " +
        "before it's teleported back to the camera boundary?")]
    [SerializeField] private float failsafeThreshold = 20;

    /// <summary>
    /// SpriteRenderer used to represent the meteoroid.
    /// </summary>
    [Header("Graphics")]
    [Tooltip("SpriteRenderer used to represent this meteoroid.")]
    [SerializeField] private SpriteRenderer meteoroidGraphicsRenderer;

    /// <summary>
    /// Array of potential sprites used to represent this meteoroid.
    /// </summary>
    [Tooltip("Array of potential sprites used to represent this meteoroid.")]
    [SerializeField] private Sprite[] meteoroidSprites;

    /// <summary>
    /// Runtime set containing active meteoroid GameObjects.
    /// </summary>
    private RuntimeSet activeMeteoroidSet;

    #region Properties
    /// <summary>
    /// Array of meteoroid objects to be activated when this meteoroid is 
    /// destroyed.
    /// </summary>
    public List<GameObject> ChildMeteoroidObjects { get; set; }

    /// <summary>
    /// ObjectPool containing references to explode particle systems. Should be
    /// assigned by the object spawning this meteoroid.
    /// </summary>
    public ObjectPool ExplosionPool { get; set; }

    /// <summary>
    /// Runtime set containing active meteoroid GameObjects.
    /// </summary>
    public RuntimeSet ActiveMeteoroidSet
    {
        get
        {
            return activeMeteoroidSet;
        }
        set
        {
            activeMeteoroidSet = value;
            if (activeMeteoroidSet != null)
            {
                activeMeteoroidSet.Add(gameObject);
            }
        }
    }

    /// <summary>
    /// What range of speeds can a meteoroid travel?
    /// </summary>
    public Vector2 TravelSpeedRange { get; set; }
    #endregion

    #region IPoolObject Methods
    public ObjectPool OriginPool { get; set; }
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        ChildMeteoroidObjects = new List<GameObject>();
    }
    private void OnEnable()
    {
        ChooseRandomSprite();
        ChooseRandomVelocity();
    }
    private void Update()
    {
        if (transform.position.magnitude >= failsafeThreshold)
        {
            transform.position = cameraBounds.GetRandomPositionOn();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Meteoroid"))
        {
            CombatTarget target = other.GetComponent<CombatTarget>();
            if (target != null)
            {
                target.TakeHit();
                if (other.CompareTag("Player"))
                {
                    CombatTarget meteoroidTarget = GetComponent<CombatTarget>();
                    meteoroidTarget.AwardPoints();
                }

                Destroy();
            }
        }
    }
    #endregion

    /// <summary>
    /// Destroys the meteoroid.
    /// </summary>
    public void Destroy()
    {
        SpawnExplosionParticleSystem();
        SpawnChildMeteoroids();
        activeMeteoroidSet.Remove(gameObject);
        OriginPool.Release(gameObject);
    }

    /// <summary>
    /// Randomly chooses a sprite from the list of potential sprites to
    /// represent this meteoroid.
    /// </summary>
    private void ChooseRandomSprite()
    {
        if (meteoroidSprites.Length > 1)
        {
            int randomIndex = Random.Range(0, meteoroidSprites.Length);
            meteoroidGraphicsRenderer.sprite = meteoroidSprites[randomIndex];
        }
    }

    /// <summary>
    /// Begins moving the meteoroid in a random direction at a random speed.
    /// </summary>
    private void ChooseRandomVelocity()
    {
        float xDir = Random.Range(-1f, 1f);
        float yDir = Random.Range(-1f, 1f);
        Vector3 direction = new Vector3(xDir, yDir, 0);

        rigidbody2D.velocity = direction.normalized *
            Random.Range(TravelSpeedRange.x, TravelSpeedRange.y);
    }

    /// <summary>
    /// Spawns child meteoroids if this meteoroid has any.
    /// </summary>
    private void SpawnChildMeteoroids()
    {
        if (ChildMeteoroidObjects.Count > 0)
        {
            foreach (GameObject childMeteoroidObject in ChildMeteoroidObjects)
            {
                childMeteoroidObject.transform.position = transform.position;
                childMeteoroidObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Spawns an explosion particle system.
    /// </summary>
    private void SpawnExplosionParticleSystem()
    {
        GameObject explosionObject = ExplosionPool.Get();
        explosionObject.transform.SetParent(null);
        explosionObject.transform.position = transform.position;
        explosionObject.SetActive(true);
    }
}
