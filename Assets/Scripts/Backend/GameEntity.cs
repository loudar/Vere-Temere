using System;

namespace VereTemereBackend {

/// <summary>
/// A parent class that lets other objects inherit very basic info about the object. Slightly decreases code complexity
/// for more complex objects. 
/// </summary>
public abstract class GameEntity
{
    /// <summary>
    /// Instantiates a new object with a (likely) unique UUID v4 within <see cref="Id"/>.
    /// </summary>
    protected GameEntity()
    {
        Id = Guid.NewGuid();
    }

    private double _health;

    /// <summary>
    /// In what shape the object is in. For NPCs, this is a physical trait, for areas how "new" or "intact" they are.
    /// </summary>
    public double Health
    {
        get => _health;
        set => _health = Math.Max(0, Math.Min(1, value));
    }

    /// <summary>
    /// NOT an identifier, only a display name, if applicable. An <see cref="Npc"/> can also use Name AND
    /// <see cref="Npc._surname"/> for display, depending on the situation.
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// A simple UUID v4 to be used as a unique identifier for a game entity.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Overrides the standard function to return a more informative representation about the area.
    /// </summary>
    /// <returns><see cref="Id"/>, <see cref="Name"/>, <see cref="Health"/></returns>
    public override string ToString()
    {
        return $"{Id} ({Name}): {Health*100}%";
    }
}
};