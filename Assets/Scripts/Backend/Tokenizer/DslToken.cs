namespace VereTemereBackend {

/// <summary>
/// Consists of a token type and a corresponding value and will be created when calling
/// <see cref="Tokenizer.Tokenize"/>.
/// </summary>
public class DslToken
{
    /// <summary>
    /// Constructs the token object within the <see cref="Tokenizer.Tokenize"/> function. 
    /// </summary>
    /// <param name="tokenType">One of <see cref="TokenTypes"/> to assign.</param>
    /// <param name="value">A string value that will later be processed in
    /// <see cref="Tokenizer.MakeString"/>.</param>
    public DslToken(TokenTypes tokenType, string? value)
    {
        TokenType = tokenType;
        Value = value;
    }

    private TokenTypes TokenType { get; set; }
    
    /// <summary>
    /// A string value that will later be processed in <see cref="Tokenizer.MakeString"/>.
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// Simply returns the <see cref="TokenTypes"/> property.
    /// </summary>
    /// <returns><see cref="TokenTypes"/></returns>
    public TokenTypes GetTokenType()
    {
        return TokenType;
    }

    /// <summary>
    /// For simplicity in usage, returns the value of the token.
    /// </summary>
    /// <returns><see cref="Value"/></returns>
    public override string? ToString()
    {
        return Value;
    }
}
};