using Newtonsoft.Json;
using System.Collections.Generic;

namespace VereTemereBackend {

/// <summary>
/// An item that is possible to be held or placed within an <see cref="Area"/>.
/// </summary>
public class DictionaryItem
{
    /// <summary>
    /// The name of the item, which is used to identify it.
    /// </summary>
    [JsonProperty]
    public string? Name { get; set; } = default!;

    /// <summary>
    /// A list of the groups an item is in, which are used to inherit properties from each group for the item.
    /// </summary>
    [JsonProperty]
    public List<string> Groups { get; set; } = new List<string>();

    /// <summary>
    /// A list of the materials an itam consists of, which are used for crafting and dialogues.
    /// </summary>
    [JsonProperty]
    public List<string> Materials { get; set; } = new List<string>();

    /// <summary>
    /// Whether the item is able to be alive, e.g. a fish. Usually false, but cares about the intersecting area between
    /// non-itemable living beings and non-living items.
    /// </summary>
    [JsonProperty]
    public bool Alive { get; set; }
    
    /// <summary>
    /// A list of the actions that can be taken on this item.
    /// </summary>
    [JsonProperty]
    public List<string> Actions { get; set; } = new List<string>();

    /// <summary>
    /// What the relative price for this item should be.
    /// </summary>
    [JsonProperty]
    public double Price { get; set; }
}
};