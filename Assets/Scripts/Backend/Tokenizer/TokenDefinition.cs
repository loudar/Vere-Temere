using System.Text.RegularExpressions;

namespace VereTemereBackend {

/// <summary>
/// Holds the combination of regex pattern and token type being returned if regex pattern matches.
/// </summary>
public class TokenDefinition
{
    private readonly Regex _regex;
    private readonly TokenTypes _returnsToken;
    private readonly string _regexPattern;

    /// <summary>
    /// Initialize the object based on predefined parameters.
    /// </summary>
    /// <param name="returnsToken">One of <see cref="TokenTypes"/> to return when matched.</param>
    /// <param name="regexPattern">The according regex pattern to map to <see cref="returnsToken"/></param>
    public TokenDefinition(TokenTypes returnsToken, string regexPattern)
    {
        _regexPattern = regexPattern;
        _regex = new Regex(regexPattern, RegexOptions.IgnoreCase | RegexOptions.ECMAScript | RegexOptions.Compiled);
        _returnsToken = returnsToken;
    }

    /// <summary>
    /// Matches a string against the saved regex pattern and returns the previously saved <see cref="TokenTypes"/>.
    /// </summary>
    /// <param name="inputString">The text to match against.</param>
    /// <returns>A <see cref="TokenMatch"/> object.</returns>
    public TokenMatch Match(string inputString)
    {
        Match match = _regex.Match(inputString);
        if (!match.Success) return new TokenMatch {IsMatch = false};
        
        var remainingText = string.Empty;
        if (match.Length != inputString.Length)
        {
            remainingText = inputString.Substring(match.Length);
        }
        
        return new TokenMatch
        {
            IsMatch = true,
            RemainingText = remainingText,
            TokenType = _returnsToken,
            Value = match.Value
        };
    }

    /// <summary>
    /// Overrides the standard function to return the saved regex pattern.
    /// </summary>
    /// <returns><see cref="_regexPattern"/></returns>
    public override string ToString()
    {
        return _regexPattern;
    }
}

/// <summary>
/// Holds information about the regex match to be returned when using <see cref="Match"/>.
/// </summary>
public class TokenMatch
{
    /// <summary>
    /// If the regex matches the input text.
    /// </summary>
    public bool IsMatch { get; set; }
    
    /// <summary>
    /// One of <see cref="TokenTypes"/> that was matched.
    /// </summary>
    public TokenTypes TokenType { get; set; }
    
    /// <summary>
    /// The text that matches the regex.
    /// </summary>
    public string? Value { get; set; }
    
    /// <summary>
    /// All text not matching the regex behind the match.
    /// </summary>
    public string? RemainingText { get; set; }
}
};