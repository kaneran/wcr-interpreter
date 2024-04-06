using Newtonsoft.Json.Linq;
using wcr_interpreter;

namespace wrc_interpreter_tests
{
    public class ParserTests
    {
        private Lexer _lexer;
        private Parser _parser;
        private string _input;

        [SetUp]
        public void Setup()
        {
            _input = @"let x = 5;
                       let y = 10;
                       let foobar = 838383;";
            _lexer = new Lexer(_input);
            _parser = new Parser(_lexer);
        }

        [Test]
        public void TestLetStatements()
        {
            var program = _parser.ParseProgram();
            if(program == null)
            {
                Assert.Fail("ParseProgram() returned null");
            }
            if(program.Statements.Length != 3)
            {
                Assert.Fail("program.Statements does not contain 3 statement. Actual length: {0}", program.Statements.Length);
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
    }
}