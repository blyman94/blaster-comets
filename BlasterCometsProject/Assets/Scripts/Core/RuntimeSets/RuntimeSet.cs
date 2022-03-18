using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores a set of GameObjects at runtime to track their active state.
/// </summary>
[CreateAssetMenu]
public class RuntimeSet : ScriptableObject
{
    /// <summary>
    /// GameEvent to raise if the runtime set becomes empty.
    /// </summary>
    [Tooltip("GameEvent to raise if the runtime set becomes empty.")]
    [SerializeField] private GameEvent onEmptyEvent;

    /// <summary>
    /// List containing items added to the runtime set.
    /// </summary>
    private List<GameObject> items;

    #region Properties
    /// <summary>
    /// List containing items added to the runtime set.
    /// </summary>
    public List<GameObject> Items
    {
        get
        {
            return items;
        }
    }
    #endregion

    /// <summary>
    /// Adds an item to the runtime set.
    /// </summary>
    public void Add(GameObject item)
    {
        items.Add(item);
    }

    /// <summary>
    /// Adds an item to the runtime set if and only if it does not already exist
    /// in the set.
    /// </summary>
    public void AddUnique(GameObject item)
    {
        if (!items.Contains(item))
        {
            Add(item);
        }
    }

    /// <summary>
    /// Clears the runtime set.
    /// </summary>
    public void Clear()
    {
        items.Clear();
    }

    /// <summary>
    /// Does this runtime set contain the passed GameObject?
    /// </summary>
    /// <param name="item">GameObject Item to test.</param>
    /// <returns>Returns true if the GameObject exists in the runtime set, false
    /// otherwise.</returns>
    public bool Contains(GameObject item)
    {
        return items.Contains(item);
    }

    /// <summary>
    /// Returns the number of items in the runtime set.
    /// </summary>
    /// <returns>Int representing the number of items in the runtime 
    /// set.</returns>
    public int Count()
    {
        return items.Count;
    }

    /// <summary>
    /// Initializes the list that stores runtime set references.
    /// </summary>
    public void Initialize()
    {
        items = new List<GameObject>();
    }

    /// <summary>
    /// Removes the passed GameObject from the runtime set, if it exists in the
    /// set.
    /// </summary>
    /// <param name="item">GameObject item to be removed from the set.</param>
    public void Remove(GameObject item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);

            if (Count() == 0)
            {
                onEmptyEvent.Raise();
            }
        }
    }
}
