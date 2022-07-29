using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plg.Compiler.AST.Commands
{
    public class FuncArgument
    {
        /// <summary>
        /// 变量类型
        /// </summary>
        public VariableType Type { get; set; }
        /// <summary>
        /// 变量名
        /// </summary>
        public string Name { get; set; }
    }
    public class DefineFuncCommand : ICommand
    {
        public CommandType Type => CommandType.DefineFunc;
        
        /// <summary>
        /// 方法名
        /// </summary>
        public string FuncName { get; set; }

        /// <summary>
        /// 入参
        /// </summary>
        public List<FuncArgument> Arguments { get; set; } = new List<FuncArgument>();

        /// <summary>
        /// 返回值
        /// </summary>
        public List<VariableType> ReturnValues { get; set; } = new List<VariableType>();

        public Scope BodyScope { get; set; }


        public void Execute()
        {

            



        }
    }
}
