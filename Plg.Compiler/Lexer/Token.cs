using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plg.Compiler.Lexer
{
    public enum TokenKind
    {

        /// <summary>
        /// 空格 / 换行
        /// </summary>
        Ignore,
        /// <summary>
        /// 抵达最后
        /// </summary>
        EOF,

        /// <summary>
        /// 变量名 [_A-Za-z][_0-9A-Za-z]*
        /// </summary>
        Name,
        /// <summary>
        /// 数字
        /// </summary>
        Number,

        

        /// <summary>
        /// 分号 ;
        /// </summary>
        Semicolon,
        /// <summary>
        /// 冒号 :
        /// </summary>
        Colon,
        /// <summary>
        /// 等号 = 
        /// </summary>
        Equal,
        /// <summary>
        /// == 
        /// </summary>
        DoubleEqual,

        /// <summary>
        /// 引号 "
        /// </summary>
        Quote,
        /// <summary>
        /// 逗号 ,
        /// </summary>
        Comma,

        /// <summary>
        /// 左花括号 {
        /// </summary>
        LeftCurly,
        /// <summary>
        /// 右花括号 }
        /// </summary>
        RightCurly,
        /// <summary>
        /// 左方括号 [
        /// </summary>
        LeftBracket,
        /// <summary>
        /// 右方括号 ]
        /// </summary>
        RightBracket,

        /// <summary>
        /// 左括号 (
        /// </summary>
        LeftParenthesis,
        /// <summary>
        /// 右括号 )
        /// </summary>
        RightParenthesis,

        /// <summary>
        /// &&
        /// </summary>
        And,
        /// <summary>
        /// ||
        /// </summary>
        Or,


        /// <summary>
        /// 加 +
        /// </summary>
        Add,
        /// <summary>
        /// 减 -
        /// </summary>
        Sub,
        /// <summary>
        /// 乘 *
        /// </summary>
        Mul,
        /// <summary>
        /// 除 /
        /// </summary>
        Div,

        /// <summary>
        /// 大于 >
        /// </summary>
        GreatThan,
        /// <summary>
        /// 小于 <
        /// </summary>
        LessThan,





        /// <summary>
        /// 定义变量  let
        /// </summary>
        Let,
        /// <summary>
        /// 定义方法 fn
        /// </summary>
        Fn,
        /// <summary>
        /// if
        /// </summary>
        If,
        /// <summary>
        /// else if
        /// </summary>
        Elif,
        /// <summary>
        /// else
        /// </summary>
        Else,
        /// <summary>
        /// for
        /// </summary>
        For,





        #region 基础类型
        /// <summary>
        /// 字符串 string
        /// </summary>
        TypeString,
        /// <summary>
        /// 数字 number (包含小数)
        /// </summary>
        TypeNumber,
        /// <summary>
        /// 布尔 bool   
        /// </summary>
        TypeBool,
        /// <summary>
        /// 对象 plg
        /// </summary>
        TypePlg,
        #endregion
        /// <summary>
        /// const true
        /// </summary>
        True,
        /// <summary>
        /// const false
        /// </summary>
        Fasle,

        /*            
let variable1:string = "Hello World";
let variable2:number = 123123;
let variable3:bool = true;

fn sa(a:string,b:btc) -> (sss:number,bbb:string) {
    if(a == "" && b ==1 || b == 2){
    }
    for(){
            
    }
}

print(variable);

let res = sa(variable1,variable2);
         */





    }

    public class Token
    {
        /// <summary>
        /// token 类型
        /// </summary>
        public TokenKind Kind { get; set; }
        
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        
    }
}
