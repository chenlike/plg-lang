using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgCompiler.Lexer
{
    /// <summary>
    /// token 类型
    /// </summary>
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
        /// 分号
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
        LeftBrace,
        /// <summary>
        /// 右花括号 }
        /// </summary>
        RightBrace,
        /// <summary>
        /// 左括号 [
        /// </summary>
        LeftBracket,
        /// <summary>
        /// 右括号 ]
        /// </summary>
        RightBracket,

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
        /// for
        /// </summary>
        For,




        #region 基础类型
        /// <summary>
        /// 字符串 string
        /// </summary>
        String,
        /// <summary>
        /// 数字 number (包含小数)
        /// </summary>
        Number,
        /// <summary>
        /// 布尔 bool   
        /// </summary>
        Bool,
        #endregion



    }

    public static class TokenMapper
    {
        public static SortedDictionary<TokenKind, string> Map = new SortedDictionary<TokenKind, string>(){
            { TokenKind.Semicolon,";" },
            { TokenKind.Colon,":" },
            { TokenKind.Equal,"=" },
            { TokenKind.Quote,"\"" },
            { TokenKind.Comma,"," },
            { TokenKind.LeftBrace,"{" },
            { TokenKind.RightBrace,"}" },
            { TokenKind.LeftBracket,"[" },
            { TokenKind.RightBracket,"]" },
            { TokenKind.Add,"+" },
            { TokenKind.Sub,"-" },
            { TokenKind.Mul,"*" },
            { TokenKind.Div,"/" },
            { TokenKind.GreatThan,">" },
            { TokenKind.LessThan,"<" },
            { TokenKind.And,"&&" },
            { TokenKind.Or,"||" },
            { TokenKind.Let,"let" },
            { TokenKind.Fn,"fn" },
            { TokenKind.If,"if" },
            { TokenKind.For,"for" },


            { TokenKind.String,"string" },
            { TokenKind.Number,"number" },
            { TokenKind.Bool,"bool" },


            { TokenKind.Ignore,"" },
            { TokenKind.EOF,"" },


        };

        
        
    }

}
