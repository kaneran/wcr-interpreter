using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcr_interpreter
{
    public class Lexer
    {
        public char Ch { get; set; }
        public string Input { get; set; }
        public int Position { get; set; }
        public int ReadPosition { get; set; }

        public Lexer(string input)
        {
            Input = input;
            ReadChar();
        }

        private void ReadChar()
        {
            if (ReadPosition >= Input.Length)
            {
                Ch = '\0';
            }
            else
            {
                Ch = Input[ReadPosition];
            }
            Position = ReadPosition;
            ReadPosition++;
        }

        public Token Next()
        {
            Token token;

            switch (Ch)
            {
                case '=':
                    token = new Token() { Literal = Ch.ToString(), Type = TokenType.ASSIGN };
                    break;
                case ';':
                    token = new Token() { Literal = Ch.ToString(), Type = TokenType.SEMICOLON };
                    break;
                case '(':
                    token = new Token() { Literal = Ch.ToString(), Type = TokenType.LPAREN };
                    break;
                case ')':
                    token = new Token() { Literal = Ch.ToString(), Type = TokenType.RPAREN };
                    break;
                case ',':
                    token = new Token() { Literal = Ch.ToString(), Type = TokenType.COMMA };
                    break;
                case '+':
                    token = new Token() { Literal = Ch.ToString(), Type = TokenType.PLUS };
                    break;
                case '{':
                    token = new Token() { Literal = Ch.ToString(), Type = TokenType.LBRACE };
                    break;
                case '}':
                    token = new Token() { Literal = Ch.ToString(), Type = TokenType.RBRACE };
                    break;
                case '\0':
                    token = new Token() { Literal = "", Type = TokenType.EOF };
                    break;
                default:
                    if (IsLetter(Ch))
                    {
                        token = new Token() { Literal = ReadIdentifier() };
                        token.Type = LookupIdent(token.Literal);
                    }
                    else
                    {
                        token = new Token() { Literal = Ch.ToString(), Type = TokenType.ILLEGAL };
                    }
                    break;
            }
            ReadChar();
            return token;
        }

        private bool IsLetter(char ch) => ch == '_' || Char.IsLetter(ch);
        
        private string ReadIdentifier()
        {
            var startPosition = Position;
            while(IsLetter(Ch))
            {
                ReadChar();
            }
            return Input[startPosition..Position];
        }

        Dictionary<string, string> keywords = new Dictionary<string, string>() { { "fn", TokenType.FUNCTION },
                                                                                 { "let", TokenType.LET} };

        private string LookupIdent(string ident)
        {
            if (keywords.ContainsKey(ident))
            {
                return keywords[ident];
            }
            return TokenType.IDENT;
        }
    }
}
