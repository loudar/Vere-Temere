using System;
using System.Collections.Generic;
using System.Linq;

namespace VereTemereBackend {

/// <summary>
/// Used to create and modify an area. An area is a representation of either a building or a piece of land.
/// </summary>
public class Area : GameEntity
{
    /// <summary>
    /// Creates a new Area instance. Only initializes references to Game objects. Use <see cref="Init"/> to
    /// generate actual data.
    /// </summary>
    /// <param name="areas">The list of areas the new area is to be added to.</param>
    /// <param name="gameState">The <see cref="GameState"/> object that is referenced for <see cref="BuildYear"/> and
    /// similar properties.</param>
    /// <param name="dictionary">The <see cref="Dictionary"/> object that contains all data about building types and
    /// possible properties (as well as all other game objects).</param>
    public Area(Dictionary<string, List<Area>?>? areas, GameState gameState, Dictionary dictionary)
    {
        _areas = areas;
        _gameState = gameState;
        _dictionary = dictionary;
    }

    /// <summary>
    /// Since the constructor does not initialize an area, a separate method is needed.
    /// Uses an <see cref="Npc"/> to set the building type.
    /// Generates <see cref="BuildYear"/>, <see cref="GameEntity.Health"/>, <see cref="Address"/>,
    /// <see cref="GameEntity.Name"/> and an <see cref="AreaObj"/>, which is a representation of the dictionary entry,
    /// used for easy access of the properties.
    /// </summary>
    /// <param name="npc">The NPC that is used to determine the building type. TODO: Make NPC nullable.</param>
    /// <param name="name">(optional) Will be used recursively if building type can't contain any residents
    /// to generate a home.</param>
    public void Init(Npc npc, string name = "")
    {
        GenerateArea(npc, name);
        const double maxBuildingAge = 175;
        int buildingAge = _random.Next(0, (int) maxBuildingAge);
        BuildYear = _gameState.CurrentYear - buildingAge;
        Health = 1 - (buildingAge / maxBuildingAge);
        Address = new Address();
    }

    // private bool AddResident(Npc npc)
    // {
    //     if (Residents.Count >= AreaObj!.ResidentLimit)
    //     {
    //         return false;
    //     }
    //     Residents.Add(npc);
    //     return true;
    // }

    private void GenerateArea(Npc npc, string? name = "")
    {
        if (npc.Occupation!.CanWorkIn.Count == 0 || name?.Length > 0)
        {
            // NPC can't work anywhere, thus gets just a home
            Name = "home";
        }
        else
        {
            Name = npc.Occupation.CanWorkIn.ToList()[_random.Next(0, npc.Occupation.CanWorkIn.Count)];
            if (Name == "everywhere")
            {
                // Assigns a random area as residency
                Name = _dictionary.Areas[_random.Next(0, _dictionary.Areas.Count)].Name;
            }
        }
        
        DictionaryArea? area = _dictionary.Areas.FirstOrDefault(area => area.Name == Name);
        if (area is null)
        {
            throw new Exception("Area does not exist. This is against the law.");
        }
        
        if (!area.CanBeHome)
        {
                // If workplace is not suitable as home, make a home in addition
                _areas?.Add("home", new List<Area>());
            var newArea = new Area(_areas, _gameState, _dictionary);
            newArea.Init(npc, "home");
            _areas!["home"]!.Add(newArea);
        }
        else
        {
            Residents.Add(npc);
        }
        
        AreaObj = area;
        AreaObj.ResidentLimit = (int) Math.Round(AreaObj.ResidentLimit * (_random.NextDouble() * .7 + .5));
    }

    private System.Random _random = new System.Random();
    public Address? Address;
    public long BuildYear;
    public List<Npc> Residents = new List<Npc>();
    public DictionaryArea? AreaObj;
    private readonly Dictionary<string, List<Area>?>? _areas;
    private readonly GameState _gameState;
    private readonly Dictionary _dictionary;

    /// <summary>
    /// Overrides the standard function to return a more informative representation about the area.
    /// </summary>
    /// <returns><see cref="GameEntity.Name"/>, <see cref="BuildYear"/>, <see cref="GameEntity.Health"/></returns>
    public override string ToString()
    {
        return $"{Name}, built in {BuildYear}, {Math.Floor(Health*100)}%";
    }
}
};