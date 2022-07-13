using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgCompiler.Definitions
{
    /// <summary>
    /// 变量类型
    /// </summary>
    public enum VariableType
    {
        /// <summary>
        /// 字符串
        /// </summary>
        String,
        /// <summary>
        /// 数字
        /// </summary>
        Number,
        /// <summary>
        /// 布尔
        /// </summary>
        Bool,
        /// <summary>
        /// 引用别的变量
        /// </summary>
        Reference,
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
        /// 字符串
        /// </summary>
        public string? StringValue { get; set; }
        /// <summary>
        /// 数组
        /// </summary>
        public decimal? NumberValue { get; set; }
        /// <summary>
        /// 布尔
        /// </summary>
        public bool? BoolValue { get; set; }

        /// <summary>
        /// 引用变量的名称
        /// </summary>
        public string? ReferenceName { get; set; }

        /// <summary>
        /// 求值语句
        /// </summary>
        public EvaluationStatement? EvalStatement { get; set; }


        
    }
}
