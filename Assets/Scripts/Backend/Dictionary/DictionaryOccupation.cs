using Newtonsoft.Json;
using System.Collections.Generic;

namespace VereTemereBackend {

/// <summary>
/// Found within a <see cref="Dictionary"/>, an occupation defines age range, social class, likely items and buildings
/// a parent <see cref="Npc"/> can have.
/// </summary>
public class DictionaryOccupation
{
    /// <summary>
    /// A unique identifier for the occupation that also gets displayed in game.
    /// </summary>
    [JsonProperty]
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// The relative probability as integer that this occupation will occur. 1 is lowest for normal occupations.
    /// If a negative value is given, Math.Abs(Probability) is seen as the maximum amount of <see cref="Npc"/> that
    /// can take this occupation. Useful for e.g. queen, king, etc. 
    /// </summary>
    [JsonProperty]
    public int Probability { get; set; }
    
    /// <summary>
    /// The minimum age an <see cref="Npc"/> needs to have to take this occupation.
    /// </summary>
    [JsonProperty]
    public int AgeMin { get; set; }
    
    /// <summary>
    /// The maximum age an <see cref="Npc"/> is allowed to have to take this occupation.
    /// </summary>
    [JsonProperty]
    public int AgeMax { get; set; }
    
    /// <summary>
    /// What <see cref="DictionarySocialClass"/> the <see cref="Npc"/> can be in. Defines the initial wealth.
    /// </summary>
    [JsonProperty]
    public int[]? PossibleClasses { get; set; }
    
    /// <summary>
    /// What items are typical for this occupation. A random percentage of these will be given to the <see cref="Npc"/>
    /// when they are initialized. NPCs will strive to acquire a full set of these items.
    /// </summary>
    [JsonProperty]
    public List<string>? Items { get; set; }

    /// <summary>
    /// The area types that the <see cref="Npc"/> will be able to work in. If one of these is suitable as home, the NPC
    /// has a chance to move in.
    /// </summary>
    [JsonProperty] 
    public HashSet<string> CanWorkIn { get; set; } = default!;
}
};