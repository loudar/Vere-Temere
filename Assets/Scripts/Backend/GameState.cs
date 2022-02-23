namespace VereTemereBackend {

/// <summary>
/// Contains general information about the ongoing game run.
/// </summary>
public class GameState
{
    /// <summary>
    /// The current fictional year.
    /// </summary>
    public long CurrentYear { get; set; }
    
    /// <summary>
    /// The fictional year that the game started.
    /// </summary>
    public int StartYear { get; set; }

    /// <summary>
    /// Initializes the game state based on input parameters. Does not create any objects.
    /// </summary>
    /// <param name="startYear">The fictional year the game is starting.</param>
    public GameState(int startYear)
    {
        StartYear = startYear;
        CurrentYear = StartYear;
    }
}
};