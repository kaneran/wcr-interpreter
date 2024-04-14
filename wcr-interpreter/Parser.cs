using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static wcr_interpreter.Ast;

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
            var program = new Ast.Program() { Statements = new List<Statement>()};
            while(!CurTokenIs(TokenType.EOF))
            {
                var statement = ParseStatement();
                if(statement != null)
                {
                    program.Statements.Add(statement);
                }
                NextToken();
            }

            return program;
        }

        private Statement ParseStatement()
        {
            switch(CurToken.Type)
            {
                case TokenType.LET:
                    return ParseLetStatement();
                default:
                    return null;
            }
        }

        private LetStatement ParseLetStatement()
        {
            var statement = new LetStatement() { Token = CurToken};
            if (!ExpectPeek(TokenType.IDENT))
            {
                return statement;
            }

            statement.Name = new Identifier() { Token = CurToken, Value = CurToken.Literal };

            if (!ExpectPeek(TokenType.ASSIGN))
            {
                return statement;
            }

            while (CurTokenIs(TokenType.SEMICOLON))
            {
                NextToken();
            }
            return statement;
        }

        private bool CurTokenIs(string type) => PeekToken.Type == type;
        private bool PeekTokenIs(string type) => PeekToken.Type == type;

        private bool ExpectPeek(string type)
        {
            if (PeekTokenIs(type))
            {
                NextToken();
                return true;
            } else
            {
                return false;
            }
        }

    }
}
