using Plg.Compiler.AST.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgCompiler.Tests.ParserTests
{
    public class ParserFuncTests
    {


        [Test]
        public void ParseCallFunc()
        {
            {
                var scope = Scope.CreateScope();

                Parser parser = new Parser("print(sss,111);");

                bool res = parser.ParseVariableAssignOrCallFunc(scope);

                Assert.IsTrue(res);
                var cmd = scope.Commands[0] as CallFuncCommand;
                Assert.IsTrue(cmd.FuncName == "print");
                Assert.IsTrue(cmd.Arguments.Count == 2);
                Assert.IsTrue(cmd.Arguments[0].Items[0].Value == "sss");
                Assert.IsTrue(cmd.Arguments[1].Items[0].Value == "111");
            }

            
        }

        [Test]
        public void ParseContainCallFunc()
        {

            {
                var scope = Scope.CreateScope();

                Parser parser = new Parser("print(sss(xxxx,222),111);");

                bool res = parser.ParseVariableAssignOrCallFunc(scope);

                Assert.IsTrue(res);
                var outCmd = scope.Commands[0] as CallFuncCommand;
                Assert.IsTrue(outCmd.FuncName == "print");
                Assert.IsTrue(outCmd.Arguments.Count == 2);
                Assert.IsTrue(outCmd.Arguments[0].Items[0].Type == ExpressionItemType.Command);
                Assert.IsTrue(outCmd.Arguments[1].Items[0].Value == "111");



                var inCmd = outCmd.Arguments[0].Items[0].Command as CallFuncCommand;
                Assert.IsTrue(inCmd.FuncName == "sss");
                Assert.IsTrue(inCmd.Arguments.Count == 2);
                Assert.IsTrue(inCmd.Arguments[0].Items[0].Value == "xxxx");
                Assert.IsTrue(inCmd.Arguments[1].Items[0].Value == "222");
            }

        }







        [Test]
        public void ParseDefineFunc()
        {

            {
                var scope = Scope.CreateScope();

                Parser parser = new Parser(@"
fn test(a:string){
    print(a);
}
");

                var cmd = parser.ParseFunc(scope);

                Assert.IsTrue(cmd.FuncName == "test");
                Assert.IsTrue(cmd.Arguments.Count == 1);
                Assert.IsTrue(cmd.Arguments[0].Type == VariableType.String);
                Assert.IsTrue(cmd.ReturnValues.Count == 0);

                Assert.IsTrue(cmd.BodyScope.Commands[0].Type == CommandType.CallFunc);
            }



            {
                var scope = Scope.CreateScope();

                Parser parser = new Parser(@"
fn test(a:string,b:number,c:plg) -> (string,plg){
    print(a);
}
");

                var cmd = parser.ParseFunc(scope);

                
                Assert.IsTrue(cmd.FuncName == "test");
                Assert.IsTrue(cmd.Arguments.Count == 3);
                Assert.IsTrue(cmd.Arguments[0].Type == VariableType.String && cmd.Arguments[0].Name == "a");
                Assert.IsTrue(cmd.Arguments[1].Type == VariableType.Number && cmd.Arguments[1].Name == "b");
                Assert.IsTrue(cmd.Arguments[2].Type == VariableType.Plg && cmd.Arguments[2].Name == "c");
                Assert.IsTrue(cmd.ReturnValues.Count == 2);
                Assert.IsTrue(cmd.ReturnValues[0] == VariableType.String);
                Assert.IsTrue(cmd.ReturnValues[1] == VariableType.Plg);

                Assert.IsTrue(cmd.BodyScope.Commands[0].Type == CommandType.CallFunc);

            }
        }


        [Test]
        public void ParseDefineFunc2()
        {


            var scope = Scope.CreateScope();

            Parser parser = new Parser(@"
            fn sa(a:string, b:number) -> (number, string) {


            }
");

            var cmd = parser.ParseFunc(scope);
            Console.WriteLine();
        }

    }
}
