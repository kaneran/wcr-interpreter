using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcr_interpreter
{
    public class Ast
    {
        public struct Program {
            public Statement[] Statements { get; }
        }

        string TokenLiteral(Program p)
        {
            if (p.Statements.Length > 0)
            {
                return p.Statements[0].TokenLiteral();
            }
            else
            {
                return "";
            }
        }

        public struct LetStatement : Statement {
            public Token Token { get; }
            public Identifier Name { get; }
            public Expression Expression { get; }

            public void StatementNode()
            {

            }

            public string TokenLiteral() => Token.Literal;
        }


        public struct Identifier : Expression
        {
            public Token Token { get; }
            public string Value { get; }

            public void ExpressionNode()
            {

            }

            public string TokenLiteral() => Token.Literal;
        }

    }
}
