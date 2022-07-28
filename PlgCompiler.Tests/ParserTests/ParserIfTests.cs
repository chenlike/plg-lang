using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgCompiler.Tests.ParserTests
{
    public class ParserIfTests
    {

        [Test]
        public void ParseIf()
        {
            {
                var scope = Scope.CreateTopScope();

                Parser parser = new Parser(@"if ((1 == 2) && ab) {  let a:string = 2; } ");
                var ifCmd = parser.ParseIf(scope);


                // 判断语句
                Assert.IsTrue(ifCmd.IfExpression.Expression.Items[0].Value == "1");
                Assert.IsTrue(ifCmd.IfExpression.Expression.Items[1].Value == "2");
                Assert.IsTrue(ifCmd.IfExpression.Expression.Items[2].Value == "==");


                Assert.IsTrue(ifCmd.IfExpression.Scope.Commands[0].Type == CommandType.VariableAssignment);
            }


        }

        [Test]
        public void ParseIfElse()
        {
            {
                var scope = Scope.CreateTopScope();

                Parser parser = new Parser(@"
if(1 == 2) { 
let a:string = asd;

}else{
    let b:number = 3;
} ");
                var ifCmd = parser.ParseIf(scope);


                // 判断语句
                Assert.IsTrue(ifCmd.IfExpression.Expression.Items[0].Value == "1");
                Assert.IsTrue(ifCmd.IfExpression.Expression.Items[1].Value == "2");
                Assert.IsTrue(ifCmd.IfExpression.Expression.Items[2].Value == "==");


                Assert.IsTrue(ifCmd.IfExpression.Scope.Commands[0].Type == CommandType.VariableAssignment);
                Assert.IsTrue(ifCmd.ElseScope != null);
                Assert.IsTrue(ifCmd.ElseScope.Commands[0].Type == CommandType.VariableAssignment);
                Assert.IsTrue(ifCmd.ElseScope.Commands.Count == 1);
            }

        }


        [Test]
        public void ParseElIf()
        {
            {
                var scope = Scope.CreateTopScope();

                Parser parser = new Parser(@"
if(1 == 2) { 
let a:string = asd;

}elif(2 == 3){
    let b:number = 3;
}elif(5 == 6){  } ");
                var ifCmd = parser.ParseIf(scope);


                // 判断语句
                Assert.IsTrue(ifCmd.IfExpression.Expression.Items[0].Value == "1");
                Assert.IsTrue(ifCmd.IfExpression.Expression.Items[1].Value == "2");
                Assert.IsTrue(ifCmd.IfExpression.Expression.Items[2].Value == "==");

                Assert.IsTrue(ifCmd.ElifExpressions.Count == 2);
                Assert.IsTrue(ifCmd.ElifExpressions[0].Expression.Items[0].Value == "2");
                Assert.IsTrue(ifCmd.ElifExpressions[0].Expression.Items[1].Value == "3");
                Assert.IsTrue(ifCmd.ElifExpressions[0].Expression.Items[2].Value == "==");


                Assert.IsTrue(ifCmd.ElifExpressions[1].Expression.Items[0].Value == "5");
                Assert.IsTrue(ifCmd.ElifExpressions[1].Expression.Items[1].Value == "6");
                Assert.IsTrue(ifCmd.ElifExpressions[1].Expression.Items[2].Value == "==");

            }

        }



        [Test]
        public void ParseContainIf()
        {
            {
                var scope = Scope.CreateTopScope();

                Parser parser = new Parser(@"
if(1 == 2) { 

    if(3 == 3){
        let a:string = asd;
    }

} ");
                var ifCmd = parser.ParseIf(scope);


                // 判断语句
                Assert.IsTrue(ifCmd.IfExpression.Expression.Items[0].Value == "1");
                Assert.IsTrue(ifCmd.IfExpression.Expression.Items[1].Value == "2");
                Assert.IsTrue(ifCmd.IfExpression.Expression.Items[2].Value == "==");


                var ifScope = ifCmd.IfExpression.Scope;
                Assert.IsTrue(ifScope.Commands[0].Type == CommandType.IfStatement);
                var ifCmdContain = ifScope.Commands[0] as IfCommand;

                Assert.IsTrue(ifCmdContain.IfExpression.Expression.Items[0].Value == "3");
                Assert.IsTrue(ifCmdContain.IfExpression.Expression.Items[1].Value == "3");
                Assert.IsTrue(ifCmdContain.IfExpression.Expression.Items[2].Value == "==");
            }
        }
    }
}
