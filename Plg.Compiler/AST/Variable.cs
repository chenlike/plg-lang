using Plg.Compiler.AST.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plg.Compiler.AST
{

    public enum VariableType
    {
        String,
        Number,
        Bool,
        Plg
    }
        
    
    public class Variable
    {
        /// <summary>
        /// 变量名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 变量类型
        /// </summary>
        public VariableType Type { get; set; }
        
        /// <summary>
        /// 表达式
        /// </summary>
        public Expression Expression { get; set; }

        /// <summary>
        /// 是否已经执行过表达式了
        /// </summary>
        public bool Executed { get { return Expression == null;  } }

        /// <summary>
        /// 当前值
        /// </summary>
        public object Value { get; set; }

    }
}
