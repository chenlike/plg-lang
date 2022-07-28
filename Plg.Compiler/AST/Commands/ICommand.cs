using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plg.Compiler.AST.Commands
{
    public interface ICommand
    {

        /// <summary>
        /// Command Type
        /// </summary>
        CommandType Type { get;  }

        /// <summary>
        /// 执行
        /// </summary>
        void Execute();
        
    }
}
