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

        public static Scope CreateTopScope()
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

        /// <summary>
        /// 变量
        /// </summary>
        private Dictionary<string, Variable> _variables = new Dictionary<string, Variable>();

        /// <summary>
        /// 添加变量到Scope
        /// </summary>
        /// <param name="variable"></param>
        /// <exception cref="Exception"></exception>
        public void AddVariable(Variable variable)
        {
            if (TryGetVariable(variable.Name,out _))
            {
                throw new Exception("变量名已存在 " + variable.Name);
            }
            _variables.Add(variable.Name, variable);
        }
        /// <summary>
        /// 尝试获得变量
        /// </summary>
        /// <param name="name"></param>
        /// <param name="variable"></param>
        /// <returns></returns>
        public bool TryGetVariable(string name,out Variable variable)
        {
            var currentExist = _variables.TryGetValue(name, out variable);
            if (currentExist == false)
            {
                if (ParentScope != null)
                {
                    return ParentScope.TryGetVariable(name, out variable);
                }
            }
            return currentExist;
        }
        

        

    }
    

}
