using Plg.Compiler.AST.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plg.Compiler.AST
{
    public class Scope
    {

        private Scope() { }

        public static Scope CreateScope()
        {
            return new Scope()
            {
                ParentScope = null,
            };
        }

        public Scope CreateChildScope()
        {
            var childScope = new Scope()
            {
                ParentScope = this,
            };
            return childScope;
        }
  






        /// <summary>
        /// 父级scope
        /// </summary>
        public Scope ParentScope { get; private set; }
        /// <summary>
        /// 是否是最顶级scope
        /// </summary>
        public bool IsTopScope { get { return ParentScope == null; } }


        public List<ICommand> Commands { get; set; } = new List<ICommand>();

    }
    

}
