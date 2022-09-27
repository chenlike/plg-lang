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
            Stack<ExpressionItem> stack = new Stack<ExpressionItem>();


            ExpressionItem pop()
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
                    stack.Push(item);
                }
            }

            var result = pop();
            return result.Value;
        }

        private ExpressionItem calculate(ExpressionItem left, ExpressionItem right, TokenKind opt,VariableType exceptType)
        {
            if (exceptType == VariableType.String)
            {
                if (opt != TokenKind.Add)
                {
                    throw new NotImplementedException($"操作符({opt.ToString()})不支持字符串");
                }

                return left.Value + right.Value;
            }
            else if (exceptType == VariableType.Number)
            {
              
                if (decimal.TryParse(left.Value,out var v1) && decimal.TryParse(right.Value,out var v2))
                {
                    switch (opt)
                    {
                        case TokenKind.Add:
                            return (v1 + v2).ToString();
                        case TokenKind.Sub:
                            return (v1 - v2).ToString();
                        case TokenKind.Mul:
                            return (v1 * v2).ToString();
                        case TokenKind.Div:
                            return (v1 / v2).ToString();
                        default:
                            throw new NotImplementedException($"操作符({opt.ToString()}) 未支持");
                    }
                }
                else
                {
                    throw new NotImplementedException($"数字解析失败");
                }

            }
            else if(exceptType == VariableType.Bool)
            {
                if (opt != TokenKind.Equal && opt != TokenKind.NotEqual)
                {
                    throw new NotImplementedException($"操作符({opt.ToString()})不支持bool");
                }
                if (left == right)
                {
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
            else
            {
                throw new Exception("不支持的类型");
            }
        }
            
    }

	
}
