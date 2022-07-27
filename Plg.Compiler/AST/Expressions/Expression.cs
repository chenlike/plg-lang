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
                sb.AppendLine($"类型:{item.Type}  Token:{item.Token.Kind}  值:{item.Value}");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 逆波兰表达式处理
        /// </summary>
        public void ReversePolishNotation()
        {

            Stack<ExpressionItem> stack = new Stack<ExpressionItem>();
            Stack<ExpressionItem> stack2 = new Stack<ExpressionItem>();
;
            foreach(var item in Items)
            {
                
            }
            

            // TODO

        }

    }

}
