/// <summary>
/// Describes an object that can be instantiated in an ObjectPool.
/// </summary>
public interface IPoolObject
{
    /// <summary>
    /// The pool from which this PoolObject originates.
    /// </summary>
    public ObjectPool OriginPool { get; set; }
}
