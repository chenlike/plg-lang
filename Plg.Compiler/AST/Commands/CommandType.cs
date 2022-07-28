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
        VariableAssignment,
        /// <summary>
        /// If 语句
        /// </summary>
        IfStatement,
        

    }
}
