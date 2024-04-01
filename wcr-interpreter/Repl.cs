using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcr_interpreter
{
    public class Repl
    {
        public void Start() { 
            var line = Console.ReadLine();
            var lexer = new Lexer(line);

            var tok = lexer.Next();
            while(tok.Type != TokenType.EOF) {
                Console.WriteLine("Type - {0} , Literal - {1}", tok.Type, tok.Literal);
                tok = lexer.Next();
            }
        }
    }
}
