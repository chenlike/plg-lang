using Plg.Compiler.AST;
using Plg.Compiler.AST.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plg.Compiler.Backend.Runtimes
{
    public partial class Runtime
    {



        public void ExecDefineVariable(DefineVariableCommand cmd)
        {

            // 判断变量是否存在

            if(TryGetVariable(cmd.Variable.Name,out _))
            {
                throw new Exception($"变量{cmd.Variable.Name}已存在");
            }

            // 表达式求值
            var expr = cmd.Variable.Expression;

            Variable variable = new Variable()
            {
                Name = cmd.Variable.Name,
                Type = cmd.Variable.Type,
                Value = cmd.Variable.Value
            };

            variable.Value = ExecuteExpression(expr, cmd.Variable.Type);

            CurrentVaribales[cmd.Variable.Name] = variable;
        }


    }
}
