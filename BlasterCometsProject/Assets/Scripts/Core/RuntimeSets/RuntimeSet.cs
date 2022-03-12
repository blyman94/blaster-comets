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
    [Tooltip("List containing items added to the runtime set.")]
    public List<GameObject> Items;

    /// <summary>
    /// Adds an item to the runtime set.
    /// </summary>
    public void Add(GameObject item)
    {
        Items.Add(item);
    }

    /// <summary>
    /// Adds an item to the runtime set if and only if it does not already exist
    /// in the set.
    /// </summary>
    public void AddUnique(GameObject item)
    {
        if (!Items.Contains(item))
        {
            Add(item);
        }
    }

    /// <summary>
    /// Clears the runtime set.
    /// </summary>
    public void Clear()
    {
        Items.Clear();
    }

    /// <summary>
    /// Does this runtime set contain the passed GameObject?
    /// </summary>
    /// <param name="item">GameObject Item to test.</param>
    /// <returns>Returns true if the GameObject exists in the runtime set, false
    /// otherwise.</returns>
    public bool Contains(GameObject item)
    {
        return Items.Contains(item);
    }

    /// <summary>
    /// Returns the number of items in the runtime set.
    /// </summary>
    /// <returns>Int representing the number of items in the runtime 
    /// set.</returns>
    public int Count()
    {
        return Items.Count;
    }

    /// <summary>
    /// Removes the passed GameObject from the runtime set, if it exists in the
    /// set.
    /// </summary>
    /// <param name="item">GameObject item to be removed from the set.</param>
    public void Remove(GameObject item)
    {
        if (Items.Contains(item))
        {
            Items.Remove(item);

            if (Count() == 0 && onEmptyEvent != null)
            {
                onEmptyEvent.Raise();
            }
        }
    }
}
