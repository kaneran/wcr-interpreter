using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json.Linq;
using System.Text;
using wcr_interpreter;
using static wcr_interpreter.Ast;

namespace wrc_interpreter_tests
{
    public class ParserTests
    {
        
        [Test]
        public void TestLetStatements()
        {
            var input = @"let x = 5;
                       let y = 10;
                       let foobar = 838383;";
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            var program = parser.ParseProgram();
            var errors = parser.Errors();
            CheckParseErrors(errors);
            //if(program == null)
            //{
            //    Assert.Fail("ParseProgram() returned null");
            //}
            if(program.Statements.Count != 3)
            {
                Assert.Fail("program.Statements does not contain 3 statement. Actual length: {0}", program.Statements.Count);
            }
            var tests = new[] {
                new { ExpectedIdentifier = "x" },
                new { ExpectedIdentifier = "y" },
                new { ExpectedIdentifier = "foobar" }
            };

            for(int i = 0; i< tests.Length; i++ ) { 
                var statement = program.Statements[i];
                if(!TestLetStatement(statement, tests[i].ExpectedIdentifier))
                {
                    return;
                }
            }
        }

        private void CheckParseErrors(List<string> errors)
        {
            if(errors.Count == 0)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\"parser has {errors.Count} errors\"");

            foreach ( var error in errors)
            {
                sb.AppendLine(error);
            }
            Assert.Fail(sb.ToString());
        }

        private bool TestLetStatement(Statement statement, string name)
        {
            if(statement.TokenLiteral() != "let")
            {
                Assert.Fail("statement.TokenLiteral not 'let', got : {0}", statement.TokenLiteral());
                return false;
            }

            if(statement is not Ast.LetStatement letStatement)
            {
                Assert.Fail("statement not Ast.LetStatement, got : {0}", statement);
                return false;
            }

            if(letStatement.Name.Value != name)
            {
                Assert.Fail("letStatement.Name.value not {0}, got : {1}", name, letStatement.Name.Value);
                return false;
            }

            if (letStatement.Name.TokenLiteral() != name) {
                Assert.Fail("statement.Name not {0}, got : {1}", name, letStatement.Name.Value);
                return false;
            }
            return true;
        }

        private bool TestReturnStatement(Statement statement)
        {
            if (statement.TokenLiteral() != "return")
            {
                Assert.Fail("statement.TokenLiteral not 'return', got : {0}", statement.TokenLiteral());
                return false;
            }

            if (statement is not Ast.ReturnStatement returnStatement)
            {
                Assert.Fail("statement not Ast.ReturnStatement, got : {0}", statement);
                return false;
            }

         
            return true;
        }

        [Test]
        public void TestReturnStatements()
        {
            var input = @"return 5;
                       return 10;
                       return 743434;";
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            var program = parser.ParseProgram();
            var errors = parser.Errors();
            CheckParseErrors(errors);
            //if(program == null)
            //{
            //    Assert.Fail("ParseProgram() returned null");
            //}
            if (program.Statements.Count != 3)
            {
                Assert.Fail("program.Statements does not contain 3 statement. Actual length: {0}", program.Statements.Count);
            }

            for (int i = 0; i < program.Statements.Count; i++)
            {
                var statement = program.Statements[i];
                if (!TestReturnStatement(statement))
                {
                    return;
                }
            }
        }

        [Test]
        public void TestString()
        {
            var input = "let myVar = anotherVar;";
            var program = new Ast.Program()
            {
                Statements = new List<Statement> {
                                              new LetStatement(){
                                                  Token = new Token{ Type = TokenType.LET, Literal = "let"},
                                                  Name = new Identifier() {
                                                      Token = new Token(){ Type = TokenType.IDENT, Literal = "myVar"},
                                                      Value = "myVar"},
                                                  Value = new Identifier(){
                                                      Token = new Token(){ Type = TokenType.IDENT, Literal = "anotherVar"},
                                                      Value = "anotherVar"}
                                              }
            }
            };

            if (!program.String().Equals("let myVar = anotherVar;"))
            {
                Assert.Fail("program.String() wrong. got {0}", program.String());
            }
        }

        [Test]
        public void TestIdentifierExpression()
        {
            var input = "foobar;";
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            var program = parser.ParseProgram();
            var errors = parser.Errors();
            CheckParseErrors(errors);

            if (program.Statements.Count != 1)
            {
                Assert.Fail("program has not enough statements. Got: {0}", program.Statements.Count);
            }
            var statement = program.Statements[0];

            if (statement is not ExpressionStatement)
            {
                Assert.Fail("program.Statements[0] is not ast.ExpressionStatement. Got: {0}", statement);
            }

            var identifier = statement.Expression;

            if (statement.Expression is not Identifier)
            {
                Assert.Fail("expression not *ast.Identifier. Got: {0}", identifier);
            }

            if (identifier.Value != "foobar")
            {
                Assert.Fail("identifier.Value not {0}. Got:{1}", "foobar", identifier.Value);
            }

            if (identifier.TokenLiteral() != "foobar")
            {
                Assert.Fail("identifier.TokenLiteral not {0}. Got:{1}", "foobar", identifier.TokenLiteral());
            }
        }

    }
}