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
        private Runtime() { }

        public static Runtime Create()
        {
            return new Runtime();
        }
        public Runtime CreateChild()
        {
            return new Runtime() {  ParentScope = this};
        }

        public Runtime ParentScope { get; private set; }



        // 当前的变量
        public Dictionary<string, Variable> CurrentVaribales { get; private set; } = new Dictionary<string, Variable>();

        /// <summary>
        /// 尝试获得变量
        /// </summary>
        /// <param name="name"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool TryGetVariable(string name,out Variable v)
        {
            var res = CurrentVaribales.TryGetValue(name, out v);

            if(res == false)
            {
                if (ParentScope != null)
                {
                    return ParentScope.TryGetVariable(name, out v);
                }
            }
            return res;
        }
            
      





        public void ExecuteScope(Scope scope)
        {

            foreach(var cmd in scope.Commands)
            {

                switch (cmd.Type)
                {
                    case CommandType.DefineVariable:
                        ExecDefineVariable(cmd as DefineVariableCommand);
                        break;
                    
                }

            }


        }

        

    }
}
