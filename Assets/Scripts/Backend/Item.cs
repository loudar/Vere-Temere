namespace VereTemereBackend {

/// <summary>
/// An item object that acts as a <see cref="GameEntity"/>. Also holds its definition. 
/// </summary>
public class Item : GameEntity
{
    public DictionaryItem Definition;

    /// <summary>
    /// Adds the definition from the <see cref="Dictionary"/>.
    /// </summary>
    /// <param name="definition">The <see cref="DictionaryItem"/> definition to reference</param>
    public Item(DictionaryItem definition)
    {
        Definition = definition;
    }
}
};