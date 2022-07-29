using Plg.Compiler.AST.Commands;
using Plg.Compiler.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plg.Compiler.AST.Expressions
{
    public enum ExpressionItemType
    {
        /// <summary>
        /// 变量
        /// </summary>
        Variable,
        /// <summary>
        /// 字符串
        /// </summary>
        String,
        /// <summary>
        /// 数字
        /// </summary>
        Number,
        /// <summary>
        /// bool
        /// </summary>
        Bool,
        /// <summary>
        /// 连接符 加减乘除
        /// </summary>
        Operator,
        /// <summary>
        /// 括号
        /// </summary>
        Parenthesis,
        /// <summary>
        /// 命令
        /// </summary>
        Command,

    }
    /// <summary>
    /// 表达式组成Item
    /// </summary>
    public class ExpressionItem
    {
        /// <summary>
        /// 类型
        /// </summary>
        public ExpressionItemType Type { get; set; }
        /// <summary>
        /// token
        /// </summary>
        public Token Token { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 命令 (Type == Command 时)
        /// </summary>
        public ICommand? Command { get; set; }
        
        /// <summary>
        /// 对象成员
        /// </summary>
        public string ObjectMember { get; set; }
        
    }
}
