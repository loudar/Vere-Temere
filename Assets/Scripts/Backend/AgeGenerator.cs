using System;

namespace VereTemereBackend {

/// <summary>
/// Used to generate ages within a set age distribution.
/// </summary>
public class AgeGenerator
{
    private readonly int _maxAge;
    private readonly double _dieAtFactor;
    private System.Random _random = new System.Random();

    /// <summary>
    /// Initializes the age constructor.
    /// </summary>
    /// <param name="maxAge">An int that specifies the maximum age in years. Defaults to 75.</param>
    /// <param name="dieAtFactor">The factor to apply to maxAge as to when people start dying of old age.</param>
    public AgeGenerator(int maxAge = 75, double dieAtFactor = .6)
    {
        _maxAge = maxAge;
        _dieAtFactor = dieAtFactor;
    }
    
    /// <summary>
    /// Generates a random age based on the current year.
    /// </summary>
    /// <param name="currentYear">The referenced year, if desired. Otherwise defaults to current year.</param>
    /// <returns>An int between 0 and maxAge.</returns>
    public int NewAge(long currentYear = default)
    {
        if (currentYear <= 0) throw new ArgumentOutOfRangeException(nameof(currentYear));
        
        currentYear = DateTime.UtcNow.Year;
        // age init vars
        double startToDieAt = _dieAtFactor * _maxAge;
        double reRollProbability = 0;
        _random = new Random();

        // generate random age based on age distributions
        int age = _random.Next(0, _maxAge);
        if (age > startToDieAt)
        {
            reRollProbability = 1 - ((_maxAge - age) / (_maxAge - startToDieAt));
        }
        
        // introduces generational peaks each {generationLength} in years.
        const double generationFluctuation = .2;
        const int generationLength = 30;
        double generationFactor = (Math.Sin(((currentYear - age) % generationLength) * 2 * Math.PI) + 1) *
                                  generationFluctuation * .5;
        if (reRollProbability + generationFactor <= 1)
        {
            reRollProbability += generationFactor;
        }

        if (reRollProbability > _random.NextDouble() || (age < 2 && _random.NextDouble() > .5))
        {
            age = _random.Next(0, _maxAge);
        }

        return age;
    }
}
};