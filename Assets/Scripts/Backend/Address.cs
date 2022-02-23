namespace VereTemereBackend {

/// <summary>
/// An address contains information about the location of an area and is strictly bound to it.
/// Information may change as the game progresses, so the info isn't static (e.g. a street could be renamed).
/// Is used in conversations, quests and navigation.
/// </summary>
public class Address
{
    public string Region;
    public string City;
    public string District;
    public string Village;
    public string Street;
    public int Number;

    /// <summary>
    /// Initializes the address object with random values.
    /// </summary>
    public Address()
    {
        // placeholder values, TODO: Generate addresses
        Region = "region";
        City = "city";
        District = "district";
        Village = "village";
        Street = "street";
        Number = 1;
    }
}
};