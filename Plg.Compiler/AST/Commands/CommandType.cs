using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plg.Compiler.AST.Commands
{
    /// <summary>
    /// 语句类型
    /// </summary>
    public enum CommandType
    {
        /// <summary>
        /// 变量声明
        /// </summary>
        DefineVariable,
        /// <summary>
        /// 变量赋值
        /// </summary>
        VariableAssign,
        /// <summary>
        /// If 语句
        /// </summary>
        IfStatement,
        /// <summary>
        /// for循环
        /// </summary>
        ForStatement,
        
        /// <summary>
        /// 调用方法
        /// </summary>
        CallFunc,
        

    }
}
