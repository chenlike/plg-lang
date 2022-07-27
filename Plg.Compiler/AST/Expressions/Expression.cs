using Plg.Compiler.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plg.Compiler.AST.Expressions
{
    public class Expression
    {

        /// <summary>
        /// 运算符  (逆波兰表达式后)
        /// </summary>
        public List<ExpressionItem> Items { get; set; }



        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var item in Items)
            {
                sb.Append($"类型:{item.Type}  Token:{item.Token.Kind}  值:{item.Value}");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 逆波兰表达式处理
        /// </summary>
        public void ReversePolishNotation()
        {

            List<ExpressionItem> result = new List<ExpressionItem>();

            Stack<ExpressionItem> stack = new Stack<ExpressionItem>();




            
            /*

a.当当前字符为数字时，直接输出；
b.当当前字符为"("时，将其压栈；
c.当当前字符为")"时，则弹出堆栈中最上的"("之前的所有运算符并输出，然后删除堆栈中的"(" ；
d.当当前字符为运算符时，则依次弹出堆栈中优先级大于等于当前运算符的(到"("之前为止)，输出，再将当前运算符压栈；
e.当为"#"时，弹出所有栈中的内容输出


             */

            foreach (var item in Items)
            {


                switch (item.Type)
                {
                    case ExpressionItemType.Bool:
                    case ExpressionItemType.String:
                    case ExpressionItemType.Number:
                    case ExpressionItemType.Variable:
                        result.Add(item);
                        continue ;
                    default:
                        break;
                }

                if (item.Type == ExpressionItemType.Operator)
                {

                    // 则依次弹出堆栈中优先级大于等于当前运算符的(到"("之前为止)，输出，再将当前运算符压栈；
                    
                    while (stack.Count > 0)
                    {
                        var top = stack.Peek();
                        if (top.Token.Kind == TokenKind.LeftParenthesis)
                        {
                            break;
                        }
                        if (top.Token.Kind >= item.Token.Kind)
                        {
                            result.Add(stack.Pop());
                        }
                        else
                        {
                            break;
                        }
                    }
                    stack.Push(item);
                }
                else
                {
                    if(item.Token.Kind == TokenKind.LeftParenthesis)
                    {
                        stack.Push(item);
                        continue;
                    }
                    else if (item.Token.Kind == TokenKind.RightParenthesis)
                    {

                        while (stack.Count > 0)
                        {
                            var top = stack.Pop();
                            if (top.Token.Kind == TokenKind.LeftParenthesis)
                            {
                                break;
                            }
                            else
                            {
                                result.Add(top);
                            }
                        }

                    }
                }

            }

            if (stack.Count > 0)
            {
                while (stack.Count > 0)
                {
                    result.Add(stack.Pop());
                }
            }

            Items = result;
        }

    }

}
