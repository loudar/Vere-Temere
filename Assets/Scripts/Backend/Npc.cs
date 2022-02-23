using System;
using System.Collections.Generic;
using System.Linq;

namespace VereTemereBackend {

/// <summary>
/// Inheriting the <see cref="GameEntity"/> class, an NPC expands with many different functionalities.
/// It contains info about its current quest, wealth, social class, etc.
/// </summary>
public class Npc : GameEntity
{
    private readonly System.Random _random = new System.Random();

    /// <summary>
    /// Does not initialize the NPC. Only sets the reference for game objects so they can be used in methods without
    /// having to be passed through.
    /// </summary>
    /// <param name="gameState">The currently active <see cref="GameState"/> object.</param>
    /// <param name="dictionary">The <see cref="Dictionary"/> object that contains all data about building types and
    /// possible properties (as well as all other game objects).</param>
    /// <param name="areas">The <see cref="Area"/> object list to be referenced.</param>
    public Npc(GameState gameState, Dictionary dictionary, Dictionary<string, List<Area>> areas)
    {
        _gameState = gameState;
        _dictionary = dictionary;
        _areas = areas;
    }

    /// <summary>
    /// Sets up initial(!) data for the NPC, such as age, occupation, etc.
    /// ALSO CREATES AREAS based on <see cref="Occupation"/>. Only use when a new NPC is created.
    /// </summary>
    /// <param name="ageGenerator">The <see cref="AgeGenerator"/> to use.</param>
    public void Init(AgeGenerator ageGenerator)
    {
        // Generate some properties randomly initially
        //_questType = _dictionary.QuestTypes[_random.Next(0, _dictionary.QuestTypes.Count)];
        Name = _dictionary.Names[_random.Next(0, (int)(_dictionary.Names?.Count))];
        _surname = _dictionary.Surnames[_random.Next(0, (int)(_dictionary.Surnames?.Count))];
        
        Age = ageGenerator.NewAge(_gameState.CurrentYear);
        
        // select an occupation
        RollOccupation(_dictionary);
        while (Age < Occupation!.AgeMin || Age > Occupation.AgeMax)
        {
            RollOccupation(_dictionary);
        }

        MakeSocialClass(_dictionary);

        Quest = new Quest(_dictionary);
        GenerateQuest();
    }

    private void GenerateQuest()
    {
        if (Quest.Active) return;
        
        Quest.Generate(Wealth * .5);
        Quest.Start();
    }
    
    private void MakeSocialClass(Dictionary dictionary)
    {
        int classIndex = _random.Next(Occupation!.PossibleClasses![0], Occupation.PossibleClasses[0] + Occupation.PossibleClasses.Length);
        DictionarySocialClass tempClass = dictionary.SocialClasses[classIndex];
        SocialClass = tempClass.ClassName;
        double randomFactor = (_random.NextDouble() * .8) + .2;
        var inheritingOccupations = new List<string>
        {
            "king",
            "queen"
        };
        double ageFactor;
        if (inheritingOccupations.Contains(Occupation.Name))
        {
            ageFactor = 1;
        }
        else
        {
            ageFactor = (double)Age / 75;
        }
        const double baseWealth = 500;
        // Wealth = baseWealth;
        Wealth = Math.Round(tempClass.WealthFactor * randomFactor * ageFactor * baseWealth * 100) / 100;
    }
    
    private void RollOccupation(Dictionary dictionary)
    {
        double isMagicProb = _random.NextDouble();
        if (isMagicProb < .01)
        {
            SelectMagicOccupation(dictionary);
        }
        else
        {
            SelectOccupation(dictionary);
        }
    }
    
    private void SelectOccupation(Dictionary dictionary)
    {
        List<DictionaryOccupation?> probableOccupations = dictionary.PossibleOccupations;
        SelectOccupationByProbability(probableOccupations);
    }
    
    private void SelectMagicOccupation(Dictionary dictionary)
    {
        List<DictionaryOccupation?> nonProbableOccupations = dictionary.NonPossibleOccupations;
        if (nonProbableOccupations.Count == 0)
        {
            SelectOccupation(dictionary);            
        }
        else
        {
            SelectOccupationByProbability(nonProbableOccupations);
            if (Occupation!.Probability < 0) Occupation.Probability += 1;
            dictionary.RecalculateNonPossibleOccupations();
            _occupationProbabilitySumCache.Remove(nonProbableOccupations);
        }
    }
    
    private void SelectOccupationByProbability(List<DictionaryOccupation?> occupations)
    {
        if (!_occupationProbabilitySumCache.TryGetValue(occupations, out int occProbSum))
        {
            occProbSum = occupations.Sum(occupation => occupation!.Probability);
            _occupationProbabilitySumCache[occupations] = occProbSum;
        }
        double selector = _random.NextDouble();
        double limitSum = occProbSum * selector;
        var currentSum = 0;
        foreach (DictionaryOccupation? occupation in occupations)
        {
            currentSum += occupation!.Probability;
            if (!(Math.Abs(currentSum) > Math.Abs(limitSum))) continue;
            
            Occupation = occupation;
            break;
        }
    }
    
    /// <summary>
    /// Generates a message for conversation based on the current quest and the input string.
    /// </summary>
    /// <returns>A message from the <see cref="Dictionary.Sentences"/> defined in the <see cref="Dictionary"/>.</returns>
    public DictionarySentence GenerateMessage()
    {
        List<DictionarySentence> possibleMessages = _dictionary.Sentences.Where(message =>
            message.Occupations.Contains(Occupation!.Name) || message.Occupations.Contains("all")).ToList();
        DictionarySentence response = possibleMessages[_random.Next(0, possibleMessages.Count)];

        return response;
    }

    /// <summary>
    /// Renders a <see cref="DictionarySentence"/> object to text and returns the result.
    /// </summary>
    /// <param name="template">The <see cref="DictionarySentence"/> template to work with.</param>
    /// <returns>A string of the rendered text.</returns>
    public string RenderMessage(DictionarySentence template)
    {
        // tokenize
        var tokenizer = new Tokenizer();
        List<DslToken> tokens = tokenizer.Tokenize(template.Text);
        
        // parse tokens
        var dialogueParser = new DialogueParser(tokens, _dictionary, this, _areas!);
        return dialogueParser.MakeString();
    }
    
    // private static double VicinityToMiddleOfRange(int age, int min, int max)
    // {
    //     double range = max - min;
    //     double difference = age - min;
    //     double ratio = difference / range;
    //     double probability = 1 - Math.Abs(2 * (ratio - .5));
    //     return probability;
    // }
    
    //private string? _questType;
    public DictionaryOccupation? Occupation;
    private string? _surname;
    public int Age;
    public string? SocialClass;
    public double Wealth;
    public Quest Quest;
    public Inventory? Inventory;
    private Dictionary<List<DictionaryOccupation?>, int> _occupationProbabilitySumCache =
        new Dictionary<List<DictionaryOccupation?>, int>();
    private readonly Dictionary _dictionary;
    private readonly GameState _gameState;
    private readonly Dictionary<string,List<Area>> _areas;
    /// <summary>
    /// Holds a list of messages sent to each NPC, if any.
    /// </summary>
    public readonly Dictionary<Guid, List<Message>?> SentMessages = new Dictionary<Guid, List<Message>?>();
    /// <summary>
    /// Holds a list of messages received from each NPC, if any.
    /// </summary>
    public readonly Dictionary<Guid, List<Message>?> ReceivedMessages = new Dictionary<Guid, List<Message>?>();

    /// <summary>
    /// Sends a message to an NPC. Uses the Guid as identifier and saves a list per NPC sent to.
    /// </summary>
    /// <param name="message">The <see cref="Message"/> object to add to the lists.</param>
    /// <param name="recipient">The <see cref="Npc"/> to send to.</param>
    public void SendMessage(Message message, Npc recipient)
    {
        if (!SentMessages.TryGetValue(recipient.Id, out List<Message>? _))
        {
            SentMessages[recipient.Id] = new List<Message>();
        }
        SentMessages[recipient.Id]!.Add(message);
        
        if (!recipient.ReceivedMessages.TryGetValue(Id, out List<Message>? _))
        {
            recipient.ReceivedMessages[Id] = new List<Message>();
        }
        recipient.ReceivedMessages[Id]!.Add(message);
    }

    /// <summary>
    /// Overrides the standard function to return a more informative representation about the area.
    /// </summary>
    /// <returns><see cref="GameEntity.Name"/>, <see cref="_surname"/>, <see cref="Age"/>, <see cref="Occupation"/>,
    /// <see cref="SocialClass"/>, <see cref="Wealth"/></returns>
    public override string ToString()
    {
        var special = " ";
        if (Name!.StartsWith(_surname![0].ToString()))
        {
            special = "★";
        }
        
        DictionarySentence template = GenerateMessage();
        var sentText = @$"""{RenderMessage(template)}"""; 
        var message = new Message(sentText, template);
        SendMessage(message, this);

        return $"{special} {Name,-18} {_surname,-14} ({Age,2}) {Occupation!.Name,-20} {SocialClass, 12} {Wealth,16:C} {sentText, -70}";
    }
}
};