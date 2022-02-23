using System;
using System.Collections.Generic;
using System.Linq;

namespace VereTemereBackend {

/// <summary>
/// Bunch o´ economics functions, have fun :)
/// </summary>
public class Economics
{
    /// <summary>
    /// Calculates the gini coefficient for a list of NPCs.
    /// </summary>
    /// <param name="balances">List of double troubles.</param>
    /// <param name="detailed">A flag if the output should be detailed/verbose.</param>
    /// <returns>A double for the gini coefficient between 0 and 1.</returns>
    public double GetGiniCoefficient(List<double> balances, bool detailed = false)
    {
        balances = balances.ToList();
        balances.Sort();
        const int rangeCount = 10;
        var ranges = new List<double>();
        double buffer = 0;
        var currentRange = 1;
        var linearChars = 2;
        int charCount;
        int counterCount;
        for (var i = 0; i < balances.Count; i++)
        {
            buffer += balances[i];
            if (i >= currentRange * (balances.Count / rangeCount))
            {
                ranges.Add(buffer);
                charCount = (int) Math.Round(rangeCount * linearChars * buffer / balances.Sum(balance => balance));
                counterCount = currentRange * linearChars - charCount;
                if (detailed)
                    Console.WriteLine(
                        $"% {currentRange * 10,3}: {new string('#', charCount)}{new string('-', counterCount)}");
                currentRange++;
            }
        }

        ranges.Add(buffer);
        charCount = (int) Math.Round(rangeCount * linearChars * buffer / balances.Sum(balance => balance));
        counterCount = currentRange * linearChars - charCount;
        if (detailed)
            Console.WriteLine($"% {currentRange * 10,3}: {new string('#', charCount)}{new string('-', counterCount)}");

        // reference: https://en.wikipedia.org/wiki/Gini_coefficient#/media/File:Economics_Gini_coefficient2.svg
        double b = ranges.Sum(range => range) / rangeCount;
        b -= ranges[0] * 1.5;
        double ab = buffer / 2;
        double a = ab - b;
        double gini = a / ab;
        return gini;
    }
}
};