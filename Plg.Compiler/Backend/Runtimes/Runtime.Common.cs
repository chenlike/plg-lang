using Plg.Compiler.AST;
using Plg.Compiler.AST.Expressions;
using Plg.Compiler.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plg.Compiler.Backend.Runtimes
{
    public partial class Runtime
    {
        /*
         如后缀表达式：”5 3 7 + * 4 2 / -”

规则：从左到右遍历表达式的每一个数字和符号，遇到数字就进栈，遇到是符号，就将处于栈顶的两个数字出栈，进行运算，运算结果进栈，一直到最终获得结果。

①初始化一个空栈。此栈用来对要运算的数字进出使用。
②后缀表达式中前三个都是数字，所以5,3,7进栈。
③接下来是’+’运算符，将栈顶的两个元素出栈进行加法运算，再将结果进栈。
④之后是’*’运算符，将栈顶的两个元素出栈进行运算，将运算结果再进栈。
⑤之后4,2进栈，遇’/’将2,4出栈，2作为除数，4作为被除数。
⑥之后遇’-‘，50作为被减数。48入栈，最后出栈，栈为空结果为48.

 */
        
		public string ExecuteExpression(Expression expression,VariableType exceptType)
        {
            Stack<string> stack = new Stack<string>();


            string pop()
            {
                if (stack.Count == 0)
                {
                    return null;
                }
                return stack.Pop();
            }



            foreach (ExpressionItem item in expression.Items)
            {
                if(item.Type == ExpressionItemType.Operator)
                {

                    var left = pop();
                    var right = pop();
                   
                    var res = calculate(left, right, item.Token.Kind,exceptType);
                    stack.Push(res);
                }
                else
                {
                    stack.Push(item.Value);
                }
            }
            Console.WriteLine();

            return null;
        }

        private string calculate(string left,string right, TokenKind opt,VariableType exceptType)
        {
            throw new NotImplementedException();
            switch (exceptType)
            {
                case VariableType.String:
                    break;
            }
        }
    }

	
}
