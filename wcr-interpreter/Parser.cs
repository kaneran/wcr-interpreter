using Microsoft.VisualBasic.FileIO;
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

        private List<string> _errors;
        private Dictionary<string, Func<Expression>> _prefixParseFns;
        private Dictionary<string, Func<Expression,Expression>> _infixParseFns;


        public Parser(Lexer lexer)
        {
            Lexer = lexer;
            _errors = new List<string>();
            NextToken();
            NextToken();

            _prefixParseFns = new Dictionary<string, Func<Expression>>();
            _infixParseFns = new Dictionary<string, Func<Expression, Expression>>();
            RegisterPrefix(TokenType.IDENT, ParseIdentifier);
            RegisterPrefix(TokenType.INT, ParseIntegerLiteral);
            RegisterPrefix(TokenType.BANG, ParsePrefixExpression);
            RegisterPrefix(TokenType.MINUS, ParsePrefixExpression);
        }

        private Expression ParseIdentifier() => new Identifier() { Token = CurToken, Value = CurToken.Literal };

        private void RegisterPrefix(string tokenType, Func<Expression> fn) => _prefixParseFns[tokenType] = fn;
        private void RegisterInfix(string tokenType, Func<Expression, Expression> fn) => _infixParseFns[tokenType] = fn;

        public List<string> Errors() => _errors;

        private void PeekError(string type)
        {
            string errorMsg = $"Expected token type : {type} , actual token type : {PeekToken.Type}";
            _errors.Add(errorMsg);
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

        private Statement? ParseStatement()
        {
            switch(CurToken.Type)
            {
                case TokenType.LET:
                    return ParseLetStatement();
                case TokenType.RETURN: 
                    return ParseReturnStatement();
                case TokenType.SEMICOLON:
                    return null;
                default:
                    return ParseExpressionStatement();
            }
        }

        private ExpressionStatement ParseExpressionStatement() {
            var statement = new ExpressionStatement() { Token = CurToken};
            statement.Expression = ParseExpression(Constants.Precdence.LOWEST);
            if (PeekTokenIs(TokenType.SEMICOLON))
                NextToken();
            return statement;
        }

        private void NoPrefixParseFnError(string tokenType)
        {
            var msg = $"no prefix parse function for {tokenType} found";
            _errors.Add(msg);
        }

        private Expression ParseExpression(string precedence)
        {
            if(!_prefixParseFns.TryGetValue(CurToken.Type, out var prefix))
            {
                NoPrefixParseFnError(CurToken.Type);
                return null;
            }

            var leftExp = prefix();

            return leftExp;
        }

        private Expression ParseIntegerLiteral()
        {
            //07/05/2024: Added fix in the case where the CurToken get's reverted to the previous non-INT token before parsing.
            if(CurToken.Type != TokenType.INT) {
                NextToken();
            }
            var literal = new IntegerLiteral() { Token = CurToken };
            if(Int64.TryParse(CurToken.Literal, out Int64 value))
            {
                literal.Value = value;
            } else
            {
                var msg = $"could not parse {CurToken.Literal} as integer";
                _errors.Add(msg);
            }

            return literal;
        }

        private Expression ParsePrefixExpression()
        {
            var expression = new PrefixExpression() { Token = CurToken, Operator = CurToken.Literal };

            NextToken();
            expression.Right = ParseExpression(Constants.Precdence.PREFIX);

            return expression;
        }

        private ReturnStatement ParseReturnStatement()
        {
            var statement = new ReturnStatement() { Token = CurToken };
            NextToken();

            while (!CurTokenIs(TokenType.SEMICOLON))
            {
                NextToken();
            }
            return statement;

        }

        private LetStatement? ParseLetStatement()
        {
            var statement = new LetStatement() { Token = CurToken};
            if (!ExpectPeek(TokenType.IDENT))
            {
                return null;
            }

            statement.Name = new Identifier() { Token = CurToken, Value = CurToken.Literal };

            if (!ExpectPeek(TokenType.ASSIGN))
            {
                return null;
            }

            while (!CurTokenIs(TokenType.SEMICOLON))
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
                PeekError(type);
                return false;
            }
        }

        private Func<Expression> PrefixParseFn;
        private Func<Expression,Expression> InfixParseFn;

    }
}
