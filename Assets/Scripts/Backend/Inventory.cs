using System.Collections.Generic;

namespace VereTemereBackend {

/// <summary>
/// A base class for inventories that holds basic info like size (is used for weight), slots, etc. and should not be
/// used on its own.
/// </summary>
public abstract class Inventory
{
    public double Size;
    public int Slots;
    public List<Item> Items = new List<Item>();

    /// <summary>
    /// Creates the inventory with a weight size and slots / item count.
    /// </summary>
    /// <param name="size">Defaults to 100, but accepts any numeric value.</param>
    /// <param name="slots">The amount of items this inventory can hold.</param>
    public Inventory(double size = 100, int slots = 30)
    {
        Size = size;
        Slots = slots;
    }

    /// <summary>
    /// Determines wether the item specified can go into the inventory.
    /// </summary>
    /// <param name="item">The item of type <see cref="Item"/> to check for.</param>
    /// <returns>A boolean, true if it fits, false if not.</returns>
    public bool ItemFitsIn(Item item)
    {
        return Items.Count < Slots;
    }

    /// <summary>
    /// Adds an <see cref="Item"/> to the inventory. Also checks if it fits, returns false if not.
    /// </summary>
    /// <param name="item">The <see cref="Item"/> instance to add to the inventory.</param>
    /// <returns>true if item was successfully added to the inventory.</returns>
    public bool AddItem(Item item)
    {
        if (!ItemFitsIn(item)) return false;
        
        Items.Add(item);
        return true;
    }
    
    /// <summary>
    /// Removes an <see cref="Item"/> from the inventory. Also checks if it actually exists, returns false if not.
    /// </summary>
    /// <param name="item">The <see cref="Item"/> instance to remove from the inventory.</param>
    /// <returns>true if item was successfully removed from the inventory.</returns>
    public bool RemoveItem(Item item)
    {
        if (!Items.Contains(item)) return false;
        
        Items.Remove(item);
        return true;
    }
}
};