
namespace wcr_interpreter
{
    public struct Token
    {
        public string Literal { get; set; }

        public string Type { get; set; }
    }

    public class TokenType
    {
        public const string ILLEGAL = "ILLEGAL";
        public const string EOF = "EOF";

        //Identifiers + literals
        public const string IDENT = "IDENT";
        public const string INT = "INT";

        //Operators
        public const string ASSIGN = "=";
        public const string PLUS = "+";

        //Delimiters
        public const string COMMA = ",";
        public const string SEMICOLON = ";";

        public const string LPAREN = "(";
        public const string RPAREN = ")";
        public const string LBRACE = "{";
        public const string RBRACE = "}";

        //Keywords
        public const string FUNCTION = "FUNCTION";
        public const string LET = "LET";
    }
}
