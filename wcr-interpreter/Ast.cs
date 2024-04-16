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
            public List<Statement> Statements { get; set;  }
        }

        string TokenLiteral(Program p)
        {
            if (p.Statements.Count > 0)
            {
                return p.Statements[0].TokenLiteral();
            }
            else
            {
                return "";
            }
        }

        public class LetStatement : Statement {
            public Token Token { get; set; }
            public Identifier Name { get; set; }
            public Expression Expression { get; }

            public void StatementNode()
            {

            }

            public string TokenLiteral() => Token.Literal;
        }


        public struct Identifier : Expression
        {
            public Token Token { get; set; }
            public string Value { get; set; }

            public void ExpressionNode()
            {

            }

            public string TokenLiteral() => Token.Literal;
        }

    }
}
