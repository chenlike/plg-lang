using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgCompiler.Tests.ParserTests
{
    public class ParserForTests
    {


        [Test]
        public void ParseFor()
        {
            {
                var scope = Scope.CreateScope();

                Parser parser = new Parser(@" 
for(let i:number=0;i<10;i++)
{
    print(i);
}

");
                var forCmd = parser.ParseFor(scope);


                Assert.IsTrue(forCmd.Define.Variable.Name == "i");
                Assert.IsTrue(forCmd.Define.Variable.Type == VariableType.Number);
                Assert.IsTrue(forCmd.Define.Variable.Expression.Items[0].Value == "0");


                Assert.IsTrue(forCmd.Condition.Items[0].Value == "i");
                Assert.IsTrue(forCmd.Condition.Items[1].Value == "10");
                Assert.IsTrue(forCmd.Condition.Items[2].Value == "<");

                var incrementCmd = forCmd.Increment as VariableAssignCommand;
                Assert.IsTrue(incrementCmd.LeftVariableName == "i");

                Assert.IsTrue(incrementCmd.Operator == TokenKind.Increase);


            }

        }
    }
}
