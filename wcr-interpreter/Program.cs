enum TokenType
{
    STRING,
    NUMBER,
    WHITESPACE
}

struct Token
{
    public string Literal { get; set; }

    public TokenType Type { get; set; }
}