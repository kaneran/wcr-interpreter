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

            public string String()
            {
                StringBuilder sb = new StringBuilder();
                foreach (Statement s in Statements)
                {
                    sb.Append(s.String());
                }

                return sb.ToString();
            }
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

        public class ExpressionStatement : Statement
        {
            public Token Token { get; set; }
            public Expression Expression { get; set; }

            public void StatementNode()
            {
                
            }

            public string String()
            {
                if(Expression is not null)
                {
                    return Expression.String();
                }
                return "";
            }

            public string TokenLiteral() => Token.Literal;
        }

        public class LetStatement : Statement {
            public Token Token { get; set; }
            public Identifier Name { get; set; }
            public Expression Value { get; set; }

            public void StatementNode()
            {

            }

            public string String()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"{TokenLiteral()} ");
                sb.Append($"{Name.String()}");
                sb.Append(" = ");
                if(Name.Value is not null)
                {
                    sb.Append($"{Value.String()}");
                }
                sb.Append(";");
                return sb.ToString();
            }

            public string TokenLiteral() => Token.Literal;
        }

        public class ReturnStatement : Statement
        {
            public Token Token { get; set; }
            public Expression Value { get; }

            public string String()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"{TokenLiteral()} ");
                if(Value is not null)
                {
                    sb.Append($"{Value.String()} ");
                }
                sb.Append(";");
                return sb.ToString();
            }

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

            public string String()
            {
                return Value;
            }

            public string TokenLiteral() => Token.Literal;
        }

        public struct IntegerLiteral : Expression
        {
            public Token Token { get; set; }
            public Int64 Value { get; set;  }

            public void ExpressionNode()
            {
            }

            public string String() => Token.Literal;

            public string TokenLiteral() => Token.Literal;
        }

        public struct PrefixExpression : Expression
        {
            public Token Token { get; set; }
            public string Operator { get; set; }
            public Expression Right { get; set; }

            public void ExpressionNode()
            {
                
            }

            public string String()
            {
                var sb = new StringBuilder();
                sb.Append("(");
                sb.Append(Operator);
                sb.Append(Right.String());
                sb.Append(")");
                return sb.ToString();
            }

            public string TokenLiteral() => Token.Literal;
        }

    }
}
