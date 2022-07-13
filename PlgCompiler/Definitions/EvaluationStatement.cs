using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgCompiler.Definitions
{
    /// <summary>
    /// 求值语句
    /// </summary>
    public class EvaluationStatement:Statement
    {

        
        public Variable Result { get; set; }

        public List<Variable> Items { get; set; }



        public Variable Exec()
        {
            return null;
        }

    }
}
