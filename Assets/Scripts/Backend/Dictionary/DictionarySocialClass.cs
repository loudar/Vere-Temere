using Newtonsoft.Json;

namespace VereTemereBackend {

/// <summary>
/// Determines wealth of the <see cref="Npc"/>.
/// </summary>
public class DictionarySocialClass
{
    /// <summary>
    /// The unique identifier.
    /// </summary>
    [JsonProperty]
    public int ClassLevel { get; set; }
    
    /// <summary>
    /// What this class will be displayed as in game.
    /// </summary>
    [JsonProperty]
    public string? ClassName { get; set; }
    
    /// <summary>
    /// Used for conversation bits. TODO: Implement class awareness into conversation.
    /// </summary>
    [JsonProperty]
    public int ClassAwareness { get; set; }
    
    /// <summary>
    /// What to scale the base Wealth by when generating a wealth for an <see cref="Npc"/>.
    /// </summary>
    [JsonProperty]
    public double WealthFactor { get; set; }
}
};