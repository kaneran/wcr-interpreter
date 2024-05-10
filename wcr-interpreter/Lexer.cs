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

        private Token _token { get; set; }

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

        private char PeekChar()
        {
            if (ReadPosition >= Input.Length)
            {
                return '\0';
            } else
            {
                return Input[ReadPosition];
            }
        }

        

        public Token Next(bool doSkip = false)
        {
            if (doSkip)
            {
                return _token;
            }
            else
            {
                Token token;
                bool isSymbol = true;
                SkipWhitespace();

                switch (Ch)
                {
                    case '=':
                        if (PeekChar() == '=')
                        {
                            var ch = Input[Position].ToString();
                            ReadChar();
                            token = new Token() { Literal = ch + Ch.ToString(), Type = TokenType.EQ };
                        }
                        else
                        {
                            token = new Token() { Literal = Ch.ToString(), Type = TokenType.ASSIGN };
                        }

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
                    case '-':
                        token = new Token() { Literal = Ch.ToString(), Type = TokenType.MINUS };
                        break;
                    case '!':

                        if (PeekChar() == '=')
                        {
                            var ch = Input[Position].ToString();
                            ReadChar();
                            token = new Token() { Literal = ch + Ch.ToString(), Type = TokenType.NOT_EQ };
                        }
                        else
                        {
                            token = new Token() { Literal = Ch.ToString(), Type = TokenType.BANG };
                        }
                        break;
                    case '/':
                        token = new Token() { Literal = Ch.ToString(), Type = TokenType.SLASH };
                        break;
                    case '*':
                        token = new Token() { Literal = Ch.ToString(), Type = TokenType.ASTERISK };
                        break;
                    case '<':
                        token = new Token() { Literal = Ch.ToString(), Type = TokenType.LT };
                        break;
                    case '>':
                        token = new Token() { Literal = Ch.ToString(), Type = TokenType.GT };
                        break;
                    case '\0':
                        token = new Token() { Literal = "", Type = TokenType.EOF };
                        break;
                    default:
                        if (IsLetter(Ch))
                        {
                            token = new Token() { Literal = ReadIdentifier() };
                            token.Type = LookupIdent(token.Literal);
                            isSymbol = !isSymbol;
                        }
                        else if (Char.IsDigit(Ch))
                        {
                            token = new Token() { Literal = ReadNumber() };
                            token.Type = TokenType.INT;
                            isSymbol = !isSymbol;
                        }
                        else
                        {
                            token = new Token() { Literal = Ch.ToString(), Type = TokenType.ILLEGAL };
                        }
                        break;
                }

                if (isSymbol)
                    ReadChar();
                _token = token;
                return token;
            }
            
        }

        private bool IsLetter(char ch) => ch == '_' || Char.IsLetter(ch);

        private string ReadIdentifier()
        {
            var startPosition = Position;
            while (IsLetter(Ch))
            {
                ReadChar();
            }
            return Input.Substring(startPosition, Position - startPosition);
        }

        private string ReadNumber()
        {
            var startPosition = Position;
            while (Char.IsDigit(Ch))
            {
                ReadChar();
            }
            var num = Input.Substring(startPosition, Position - startPosition);
            return Input.Substring(startPosition, Position - startPosition);
        }

        Dictionary<string, string> keywords = new Dictionary<string, string>() { { "fn", TokenType.FUNCTION },
                                                                                 { "let", TokenType.LET },
                                                                                 {"true", TokenType.TRUE},
                                                                                 {"false", TokenType.FALSE},
                                                                                 {"if", TokenType.IF},
                                                                                 {"else", TokenType.ELSE},
                                                                                 {"return", TokenType.RETURN},
        };

        private string LookupIdent(string ident)
        {
            if (keywords.ContainsKey(ident))
            {
                return keywords[ident];
            }
            return TokenType.IDENT;
        }

        private void SkipWhitespace()
        {
            while (Ch.Equals(' ') || Ch.Equals('\n') || Ch.Equals('\r') || Ch.Equals('\t'))
            {
                ReadChar();
            }
        }
    }
}
