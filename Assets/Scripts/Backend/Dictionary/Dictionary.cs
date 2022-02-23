using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace VereTemereBackend {

/// <summary>
/// Contains information about what object types and properties are possible to be had inside the game. Also contains
/// data about conversation, as it is strictly interlinked with the associated objects.
/// </summary>
public class Dictionary
{
    private List<DictionaryOccupation>? _possibleOccupations;
    private List<DictionaryOccupation>? _nonPossibleOccupations;

    /// <summary>
    /// Currently not used yet. TODO: Implement quests.
    /// </summary>
    [JsonProperty]
    public List<string> QuestTypes { get; set; } = new List<string>();
    
    /// <summary>
    /// A list of <see cref="DictionaryArea"/> objects, which is used to generate areas.
    /// </summary>
    [JsonProperty]
    public List<DictionaryArea> Areas { get; set; } = new List<DictionaryArea>();
    
    /// <summary>
    /// A list of possible given names. Is not grouped in any way. Contains data from many different countries, but is
    /// not balanced specifically.
    /// </summary>
    [JsonProperty]
    public List<string> Names { get; set; } = new List<string>();
    
    /// <summary>
    /// A list of possible surnames. Is not grouped in any way. Contains data from many different countries, but is
    /// not balanced specifically.
    /// </summary>
    [JsonProperty]
    public List<string> Surnames { get; set; } = new List<string>();
    
    /// <summary>
    /// A list of possible <see cref="DictionaryOccupation"/>s which can be assigned to an <see cref="Npc"/>. An NPC can only
    /// have one occupation at a time. TODO: Implement actions that are independent from occupations.
    /// </summary>
    [JsonProperty]
    public List<DictionaryOccupation> Occupations { get; set; } = new List<DictionaryOccupation>();

    /// <summary>
    /// A list of occupations that occur normally with a relative probability, rendered on first access, then cached.
    /// </summary>
    [JsonIgnore]
    public List<DictionaryOccupation?> PossibleOccupations
    {
        get { return (_possibleOccupations ??= Occupations.Where(oc => oc.Probability > 0).ToList())!; }
    }

    /// <summary>
    /// A list of occupations that can occur Math.Abs(oc.Probability) times, rendered on first access, then cached.
    /// </summary>
    [JsonIgnore]
    public List<DictionaryOccupation?> NonPossibleOccupations
    {
        get { return (_nonPossibleOccupations ??= Occupations.Where(oc => oc.Probability < 0).ToList())!; }
    }
    
    /// <summary>
    /// A list of possible <see cref="DictionarySentence"/>s. Will be used within conversations and is tokenized to process
    /// different variations, formats and variables. TODO: Abstract syntax tree?
    /// </summary>
    [JsonProperty]
    public List<DictionarySentence> Sentences { get; set; } = new List<DictionarySentence>();
    
    /// <summary>
    /// A list of possible <see cref="DictionaryItem"/>s. They can be held or placed within areas.
    /// TODO: Implement inventory + placement.
    /// </summary>
    [JsonProperty]
    public List<DictionaryItem> Items { get; set; } = new List<DictionaryItem>();
    
    /// <summary>
    /// A list of possible <see cref="DictionarySocialClass"/>s for an NPC. Will be assigned on <see cref="Npc.Init"/>.
    /// </summary>
    [JsonProperty]
    public List<DictionarySocialClass> SocialClasses { get; set; } = new List<DictionarySocialClass>();

    /// <summary>
    /// A list of possible actions for an item.
    /// </summary>
    [JsonProperty]
    public List<ItemAction> ItemActions { get; set; } = new List<ItemAction>();
    
    /// <summary>
    /// Resets the cache for <see cref="NonPossibleOccupations"/>. Gets called when a magic probability changes.
    /// </summary>
    public void RecalculateNonPossibleOccupations()
    {
        _nonPossibleOccupations = null;
    }
}

/// <summary>
/// An action that can be taken on an item or to reference it.
/// </summary>
public class ItemAction
{
    /// <summary>
    /// A unique identifier that also gets displayed in game.
    /// </summary>
    [JsonProperty]
    public string Name { get; set; } = default!;
}
};