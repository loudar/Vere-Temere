using System;

namespace VereTemereBackend {

/// <summary>
/// Represents the current quest for an <see cref="Npc"/>.
/// </summary>
public class Quest
{
    public bool Active;
    public DictionaryItem DictionaryItem = new DictionaryItem();
    private readonly System.Random _random = new System.Random();
    public int DesiredItemCount;
    public int DeliveredItemCount;
    private Dictionary _dictionary;

    /// <summary>
    /// Creates the base object.
    /// </summary>
    /// <param name="dictionary">The <see cref="Dictionary"/> object that contains all data about building types and
    /// possible properties (as well as all other game objects).</param>
    public Quest(Dictionary dictionary)
    {
        _dictionary = dictionary;
    }

    /// <summary>
    /// The reward in $ the player gets when completing the quest.
    /// </summary>
    public double Reward { get; set; }

    /// <summary>
    /// Attempts to complete a quest and gives back the success status.
    /// </summary>
    /// <returns>A flag whether the completion was successful.</returns>
    public bool Complete()
    {
        if (true) // if player fullfills requirements 
        {
            // Reset variables
            Active = false;
            DictionaryItem = new DictionaryItem();
            DesiredItemCount = 0;
            DeliveredItemCount = 0;
        }
        return false;
    }

    /// <summary>
    /// Generates new random data for a possible next quest.
    /// </summary>
    public void Generate(double rewardMax)
    {
        DictionaryItem = _dictionary.Items[_random.Next(0, _dictionary.Items.Count - 1)];
        DesiredItemCount = _random.Next(1, 5);
        if (DictionaryItem.Name == "coin")
        {
            Reward = 1;
        }
        else
        {
            Reward = DictionaryItem.Price * .5 + _random.NextDouble() * DictionaryItem.Price * .55;
        }
        Reward = Math.Min(rewardMax, Reward * DesiredItemCount);
        DeliveredItemCount = 0;
    }
    
    /// <summary>
    /// Activates the quest. Does not change anything about it.
    /// </summary>
    public void Start()
    {
        Active = true;
    }
}
};