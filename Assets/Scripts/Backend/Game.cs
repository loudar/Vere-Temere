using System.Collections.Generic;
using System.Globalization;
using System.Linq;
//using Microsoft.Extensions.Logging;

namespace VereTemereBackend {

/// <summary>
/// The overall game. Construct, then call <see cref="Start"/> to actually run the game. TODO: Game cycle
/// </summary>
public class Game
{
    private readonly Dictionary _dictionary;
    //private readonly ILogger<Game> _logger;
    public GameState GameState;
    private readonly System.Random _random = new System.Random();
    public Dictionary<string, List<Area>> Areas = new Dictionary<string, List<Area>>();
    public List<Npc> Npcs = new List<Npc>();
    public AgeGenerator AgeGenerator = new AgeGenerator();

    /// <summary>
    /// References the dictionary to generate objects and initialize the game's <see cref="GameState"/>.
    /// </summary>
    /// <param name="dictionary">The <see cref="Dictionary"/> object that contains all data about building types and
    /// possible properties (as well as all other game objects).</param>
    /// <param name="logger">loggi loggi</param>
    public Game(Dictionary dictionary)
    {
        _dictionary = dictionary;
        //_logger = logger ?? throw new ArgumentNullException(nameof(logger));
        int startYear = _random.Next(1200, 1900);
        GameState = new GameState(startYear);
        CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
    }
    
    /// <summary>
    /// Initialize the game objects. Creates NPCs and areas. To run the game, use GetNextGameFrame.
    /// repeatedly. 
    /// </summary>
    public void Start()
    {
        //var random = new Random();
        //int npcCount = random.Next(500, 200000);
        
        MakeNpcs(400);
        MakeAreas();
        
        // PrintAreaCounts(dictionary, areas);
    }

    ///// <summary>
    ///// Increments the internal frame counter based on the set time resolution.
    ///// </summary>
    // public void GetNextGameFrame()
    // {
        //// TODO: Frame counter, time resolution, progressing variables for NPCs + areas
    // }

    /// <summary>
    /// Initializes a list of NPCs randomly. Respects a working age distribution and a wealth distribution with a
    /// rough Gini coefficient of ~70%, which was typical for medieval societies.
    /// </summary>
    /// <param name="npcCount">The amount of NPCs to generate. Shouldn't be more than 200.000 at once, as it gets
    /// really slow afterwards due to checking all instances for each new NPC.</param>
    private void MakeNpcs(int npcCount)
    {
        for (var i = 0; i < npcCount; i++) // only prints update every 2^x npcs, saves some cpu time
        {
            if (i % 8 == 0)
            {
                //_logger.LogDebug("Generating NPC {Amount} of {TotalAmount}", i, npcCount);
            }

            var newNpc = new Npc(GameState, _dictionary, Areas!);
            newNpc.Init(AgeGenerator);
            Npcs.Add(newNpc);
        }
    }

    private void MakeAreas()
    {
        for (var i = 0; i < Npcs!.Count; i++)
        {
            Npc npc = Npcs[i];
            if (i % 8 == 0) // only prints update every 2^x npcs, saves some cpu time
            {
                //_logger.LogDebug("Generating building for NPC {Amount}/{TotalAmount}", i, Npcs.Count);
            }
    
            if (FillIntoSomeResidency(npc))
            {
                continue;
            }

            if (npc.Age > 16)
            {
                var newArea = new Area(Areas, GameState, _dictionary);
                newArea.Init(npc);
                AddArea(newArea);
            }
            else
            {
                ForceIntoSomeResidency(npc);
            }
        }
    }

    private void AddArea(Area newArea)
    {
        if (!newArea.AreaObj!.CanBeHome && Areas!.ContainsKey(newArea.Name!))
        {
            return;
        }
        Areas!.Add(newArea.Name!, new List<Area>());
        Areas[newArea.Name!]!.Add(newArea);
    }
    
    private void ForceIntoSomeResidency(Npc npc)
    {
        // Forcibly put the NPC into a random building
        if (Areas!.Count > 0)
        {
            List<Area> areaList = Areas.SelectMany(area => area.Value!).Where(area => area.AreaObj!.CanBeHome).ToList();
            Area area = areaList[_random.Next(0, areaList.Count - 1)];
            area.Residents.Add(npc);            
        }
        else
        {
            var newArea = new Area(Areas, GameState, _dictionary);
            newArea.Init(npc);
            AddArea(newArea);
        }
    }

    private bool FillIntoSomeResidency(Npc npc)
    {
        // only fills NPC into building if residentLimit isn't reached AND it's a building that fits their occupation
        
        foreach (string canWorkIn in npc.Occupation!.CanWorkIn)
        {
            if (!Areas!.TryGetValue(canWorkIn, out List<Area>? areas))
            {
                continue;
            }

            foreach (Area area in areas!.Where(area => area.Residents.Count < area.AreaObj!.ResidentLimit))
            {
                area.Residents.Add(npc);
                return true;
            }
        }

        if (!Areas!.ContainsKey("home"))
        {
            return false;
        }
        
        foreach (Area area in Areas["home"]!.Where(area => area.Residents.Count < area.AreaObj!.ResidentLimit))
        {
            area.Residents.Add(npc);
            return true;
        }
        
        return false;
    }

    // void PrintAreaCounts(Dictionary dictionary1, List<Area> list)
    // {
    //     var areaCounts = new List<int>();
    //     var areaNames = new List<string?>();
    //     foreach (PossibleArea area in dictionary1.Areas)
    //     {
    //         List<Area> actualAreas = list.Where(actualArea => actualArea.AreaObj.Name == area.Name).ToList();
    //         areaCounts.Add(actualAreas.Sum(_ => 1));
    //         areaNames.Add(area.Name);
    //     }
    //
    //     for (var i = 0; i < _areas.Count; i++)
    //     {
    //         Console.WriteLine($"{_areas[i].Count}    {areaNames[i]}");
    //     }
    // }
}
};