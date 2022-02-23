using Newtonsoft.Json;

namespace VereTemereBackend {

/// <summary>
/// What area types are allowed to exist within the game, usually defined within a <see cref="Dictionary"/>.
/// </summary>
public class DictionaryArea
{
    /// <summary>
    /// A unique identifier for the occupation that also gets displayed in game.
    /// </summary>
    [JsonProperty]
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// If this <see cref="Area"/> can hold any residents at all.
    /// </summary>
    [JsonProperty]
    public bool CanBeHome { get; set; }
    
    /// <summary>
    /// How many residents are able to exist within this area.
    /// </summary>
    [JsonProperty]
    public int ResidentLimit { get; set; }
}
};