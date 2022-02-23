using System;
using System.Collections.Generic;

namespace VereTemereBackend {

//namespace VereTemereBackend;

/// <summary>
/// Is used to tokenize and process text templates.
/// TODO: Move processing into associated classes and inherit from more general functions within the TemplateParser.
/// </summary>
public class Tokenizer
{
    private List<TokenDefinition>? _tokenDefinitions;

    /// <summary>
    /// Creates a basic tokenizer.
    /// </summary>
    public Tokenizer()
    {
        Init();
    }
    
    /// <summary>
    /// Tokenizes the input string into a list of tokens defined in <see cref="TokenTypes"/> and returns them.
    /// </summary>
    /// <param name="input">The templated text string to process.</param>
    /// <returns>A list of <see cref="DslToken"/> objects which can be used for further processing.</returns>
    public List<DslToken> Tokenize(string? input)
    {
        var tokens = new List<DslToken>();

        string? remainingText = input;

        while (remainingText?.Length > 0)
        {
            TokenMatch match = FindMatch(remainingText);
            if (match.IsMatch)
            {
                tokens.Add(new DslToken(match.TokenType, match.Value));
                remainingText = match.RemainingText;
            }
            else
            {
                // don't check for invalid tokens, all non-token text will be printed
                remainingText = remainingText.Substring(1);
            }
        }

        tokens.Add(new DslToken(TokenTypes.SequenceTerminator, string.Empty));

        return tokens;
    }

    private TokenMatch FindMatch(string input)
    {
        foreach (TokenDefinition tokenDefinition in _tokenDefinitions!)
        {
            TokenMatch match = tokenDefinition.Match(input);
            
            if (!match.IsMatch) continue;
            
            return match;
        }

        Console.WriteLine($"no match for '{input}'");
        return new TokenMatch {IsMatch = false};
    }

    private void Init()
    {
        _tokenDefinitions = new List<TokenDefinition>
        {
            new TokenDefinition(TokenTypes.Or, "^\\[[^\\][]+\\]"),
            new TokenDefinition(TokenTypes.Optional, "^#"),
            new TokenDefinition(TokenTypes.Plural, "^\\$"),
            // new(TokenTypes.PastTense, "^\\-"),
            // new(TokenTypes.VariableStart, "^{"),
            // new(TokenTypes.VariableEnd, "^}"),
            // new(TokenTypes.Variable, "^[^\\{[]+(?=})"),
            new TokenDefinition(TokenTypes.Variable, "^{[^\\{[]+[^\\}](?=})}"),
            new TokenDefinition(TokenTypes.Text, "^[a-zA-Z !\\.\\?\\',\\(\\)]+")
        };
    }
}

/// <summary>
/// The possible token types for the game.
/// </summary>
public enum TokenTypes
{
    /// <summary>
    /// A list of options is available for output.
    /// </summary>
    Or,
    /// <summary>
    /// The next token is optional with a 50/50 chance.
    /// </summary>
    Optional,
    /// <summary>
    /// The token is to be replaced with a fitting value.
    /// </summary>
    Variable,
    /// <summary>
    /// Indicates that the next token is a <see cref="Variable"/>.
    /// </summary>
    VariableStart,
    /// <summary>
    /// Indicates that the previous token was a <see cref="Variable"/>.
    /// </summary>
    VariableEnd,
    /// <summary>
    /// The next token is to be pluralized.
    /// </summary>
    Plural,
    /// <summary>
    /// The next token is to be formatted to past tense.
    /// </summary>
    PastTense,
    /// <summary>
    /// Token is taken as raw text.
    /// </summary>
    Text,
    /// <summary>
    /// End of sequence/string.
    /// </summary>
    SequenceTerminator
}
};