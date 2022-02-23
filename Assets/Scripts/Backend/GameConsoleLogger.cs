using System;
using System.Collections.Generic;
using System.Linq;

namespace VereTemereBackend {

/// <summary>
/// Dev tool to log game generation/states whatever smile :)
/// </summary>
public class GameConsoleLogger
{
    /// <summary>
    /// Prints a list of residents, including which areas they live in (if detailed flag is true).
    /// </summary>
    /// <param name="game">The <see cref="Game"/> object to reference.</param>
    /// <param name="detailed">A flag if the output should be detailed/verbose.</param>
    public void PrintResidents(Game game, bool detailed = false)
    {
        if (detailed)
        {
            const int buildingWidth = 160;
            foreach (Area area in game.Areas!.SelectMany(keyValuePair => keyValuePair.Value!))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"  ╔{new string('═', buildingWidth+4)}╗");
                Console.WriteLine($"  ║  {area, buildingWidth}  ║");
                // Find NPCs living in that area
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                foreach (Npc npc in area.Residents)
                {
                    Console.WriteLine($"  ║ {npc} ║");
                }

                Console.WriteLine($"  ╚{new string('═',buildingWidth+4)}╝");

                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
        }
        
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"NPCs            {game.Npcs!.Count}");
        
        if (detailed)
        {
            // children
            List<Npc> children = game.Npcs.Where(npc => npc.Occupation!.Name == "child").ToList();
            Console.WriteLine($"Children        {children?.Count}");
        }

        int areaCount = game.Areas!.Values.SelectMany(v => v!).Count();
        Console.WriteLine($"Areas           {areaCount}");
        Console.WriteLine($"NPCs / Area     {Math.Round((double) ((game.Npcs.Count / areaCount) * 100)) / 100}");

        double totalWealth = Math.Floor(game.Npcs.Sum(npc => npc.Wealth) * 100) / 100;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Wealth          {totalWealth,13:C}");
        Console.WriteLine($"Average         {totalWealth / game.Npcs.Count,13:C}");
        var econ = new Economics();
        List<double> wealthList = game.Npcs.Select(npc => npc.Wealth).ToList();
        Console.WriteLine($"Gini            {(int) Math.Round(econ.GetGiniCoefficient(wealthList, true) * 100),12}%");

        Console.ForegroundColor = ConsoleColor.White;
    }
}
};