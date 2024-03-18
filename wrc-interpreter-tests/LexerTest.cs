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
            _input = @"let five = 5;
            let ten = 10;
            let add = fn(x, y) {
                x + y;
            };
            let result = add(five, ten);";
            _lexer = new Lexer(_input);

        }

        [Test]
        public void TestNextToken()
        {
            Token[] expectedTokens = { new Token() { Literal = "let", Type = TokenType.LET },
                                       new Token() { Literal = "five", Type = TokenType.IDENT },
                                       new Token() { Literal = "=", Type = TokenType.ASSIGN },
                                       new Token() { Literal = "5", Type = TokenType.INT },
                                       new Token() { Literal = ";", Type = TokenType.SEMICOLON },
                                       new Token() { Literal = "let", Type = TokenType.LET },
                                       new Token() { Literal = "ten", Type = TokenType.IDENT },
                                       new Token() { Literal = "=", Type = TokenType.ASSIGN },
                                       new Token() { Literal = "10", Type = TokenType.INT },
                                       new Token() { Literal = ";", Type = TokenType.SEMICOLON },
                                       new Token() { Literal = "let", Type = TokenType.LET },
                                       new Token() { Literal = "add", Type = TokenType.IDENT },
                                       new Token() { Literal = "=", Type = TokenType.ASSIGN },
                                       new Token() { Literal = "fn", Type = TokenType.FUNCTION },
                                       new Token() { Literal = "(", Type = TokenType.LPAREN },
                                       new Token() { Literal = "x", Type = TokenType.IDENT },
                                       new Token() { Literal = ",", Type = TokenType.COMMA },
                                       new Token() { Literal = "y", Type = TokenType.IDENT },
                                       new Token() { Literal = ")", Type = TokenType.RPAREN },
                                       new Token() { Literal = "{", Type = TokenType.LBRACE },
                                       new Token() { Literal = "x", Type = TokenType.IDENT },
                                       new Token() { Literal = "+", Type = TokenType.PLUS },
                                       new Token() { Literal = "y", Type = TokenType.IDENT },
                                       new Token() { Literal = ";", Type = TokenType.SEMICOLON },
                                       new Token() { Literal = "}", Type = TokenType.RBRACE },
                                       new Token() { Literal = ";", Type = TokenType.SEMICOLON },
                                       new Token() { Literal = "let", Type = TokenType.LET },
                                       new Token() { Literal = "result", Type = TokenType.IDENT },
                                       new Token() { Literal = "=", Type = TokenType.ASSIGN },
                                       new Token() { Literal = "add", Type = TokenType.IDENT },
                                       new Token() { Literal = "(", Type = TokenType.LPAREN },
                                       new Token() { Literal = "five", Type = TokenType.IDENT },
                                       new Token() { Literal = ",", Type = TokenType.COMMA },
                                       new Token() { Literal = "ten", Type = TokenType.IDENT },
                                       new Token() { Literal = ")", Type = TokenType.RPAREN },
                                       new Token() { Literal = ";", Type = TokenType.SEMICOLON },
                                       new Token() { Literal = "", Type = TokenType.EOF },
            };

            foreach (Token token in expectedTokens)
            {
                var tok = _lexer.Next();
                if (tok.Type != token.Type)
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