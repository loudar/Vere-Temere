namespace VereTemereBackend {

/// <summary>
/// Is used to save messages to a message history.
/// </summary>
public class Message
{
    private readonly string _sentText;
    private readonly DictionarySentence _dictionarySentence;

    /// <summary>
    /// Initializes readonly object to add to a message list.
    /// </summary>
    /// <param name="sentText">The rendered text.</param>
    /// <param name="dictionarySentence">The <see cref="DictionarySentence"/> text template.</param>
    public Message(string sentText, DictionarySentence dictionarySentence)
    {
        _sentText = sentText;
        _dictionarySentence = dictionarySentence;
    }
}
};