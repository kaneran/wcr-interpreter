using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcr_interpreter
{
    public class Lexer
    {
        public byte Ch { get; set; }
        public string Input { get; set; }
        public int Position { get; set; }
        public int ReadPosition { get; set; }

        public Lexer(string input)
        {
            Input = input;
        }

        public Token Next()
        {
            return new Token() { Literal = "" , Type = TokenType.EOF };
        }
    }
}
