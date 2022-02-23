using Newtonsoft.Json;
using System.Collections.Generic;

namespace VereTemereBackend {

/// <summary>
/// A text template within a <see cref="Dictionary"/> to be tokenized and processed.
/// </summary>
public class DictionarySentence
{
    /// <summary>
    /// The text template.
    /// </summary>
    [JsonProperty]
    public string? Text { get; set; }

    /// <summary>
    /// Which of type <see cref="DictionaryOccupation"/> can say this sentence. Useful for specific conversations. Otherwise
    /// "all".
    /// </summary>
    [JsonProperty]
    public List<string> Occupations { get; set; } = new List<string>();
}
};