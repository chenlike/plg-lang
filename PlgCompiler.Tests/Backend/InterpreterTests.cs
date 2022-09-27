using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgCompiler.Tests.Backend
{
    public class InterpreterTests
    {
        [Test]
        public void B()
        {

            {
                Parser parser = new Parser(@"
let a:number = 1;
let b:number = a + 3 * 2;
");
                var scope = parser.Parse();
                var interpreter = new Interpreter();
                interpreter.Run(scope);
                Console.WriteLine();

            }






        }

        [Test]
        public void A()
        {

            {
                Parser parser = new Parser(@"
let a:string = ""HelloWorld!"";
");
                var scope = parser.Parse();
                new Interpreter().Run(scope);
            }




            

        }
        

    }
}
