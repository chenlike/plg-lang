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
        public void A()
        {

            Parser parser = new Parser(@"
let a:string = ""HelloWorld!"";
");
            var scope = parser.Parse();


            new Interpreter().Run(scope);
            

        }
        

    }
}
