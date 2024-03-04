using wcr_interpreter;

namespace wrc_interpreter_tests
{
    public class Tests
    {
        private Lexer _lexer;
        private string _input;

        [SetUp]
        public void Setup()
        {
            _input = "+={}()";
            _lexer = new Lexer(_input);

        }

        [Test]
        public void Test1()
        {
            Token[] expectedTokens = { new Token() { Literal = "+", Type = TokenType.PLUS },
                                       new Token() { Literal = "=", Type = TokenType.ASSIGN },
                                       new Token() { Literal = "{", Type = TokenType.LBRACE },
                                       new Token() { Literal = "}", Type = TokenType.RBRACE },
                                       new Token() { Literal = "(", Type = TokenType.LPAREN },
                                       new Token() { Literal = ")", Type = TokenType.RPAREN },
            };

            foreach (Token token in expectedTokens)
            {
                var tok = _lexer.Next();
                if(tok.Type != token.Type)
                {
                    Assert.Fail("The type of the token is incorrect - Expected type: {0}, Actual type: {1}", token.Type, tok.Type);
                }
                if (tok.Literal != token.Literal)
                {
                    Assert.Fail("The value of the token is incorrect - Expected value: {0}, Actual value: {1}", token.Literal, tok.Literal);
                }
            }
        }
    }
}