using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcr_interpreter
{
    public struct Parser
    {
        Lexer Lexer { get; }
        Token CurToken { get; set; }
        Token PeekToken { get; set; }

        public Parser(Lexer lexer)
        {
            Lexer = lexer;
            NextToken();
            NextToken();
        }

        private void NextToken()
        {
            CurToken = PeekToken;
            PeekToken = Lexer.Next();
        }

        public Ast.Program ParseProgram()
        {
            var program = new Ast.Program() { Statements = Array.Empty<Statement>()};
        }

    }
}
