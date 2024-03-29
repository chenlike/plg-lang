﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plg.Compiler.Lexer
{
    public static class TokenMapper
    {
        public static SortedDictionary<TokenKind, string> Map = new SortedDictionary<TokenKind, string>(){
            { TokenKind.Semicolon,";" },
            { TokenKind.Colon,":" },
            { TokenKind.Dot,"." },
            { TokenKind.Equal,"=" },
            { TokenKind.DoubleEqual,"==" },
            { TokenKind.NotEqual,"!=" },
            { TokenKind.Quote,"\"" },
            { TokenKind.Comma,"," },
            { TokenKind.LeftCurly,"{" },
            { TokenKind.RightCurly,"}" },
            { TokenKind.LeftBracket,"[" },
            { TokenKind.RightBracket,"]" },
            { TokenKind.LeftParenthesis,"(" },
            { TokenKind.RightParenthesis,")" },

            { TokenKind.Add,"+" },
            { TokenKind.Sub,"-" },
            { TokenKind.Mul,"*" },
            { TokenKind.Div,"/" },


            { TokenKind.AddEqual,"+=" },
            { TokenKind.SubEqual,"-=" },
            { TokenKind.MulEqual,"*=" },
            { TokenKind.DivEqual,"/=" },
            { TokenKind.Increase,"++" },
            { TokenKind.Decrease,"--" },




            { TokenKind.GreatThan,">" },
            { TokenKind.LessThan,"<" },
            { TokenKind.And,"&&" },
            { TokenKind.Or,"||" },
            { TokenKind.Let,"let" },
            { TokenKind.Fn,"fn" },
            { TokenKind.If,"if" },
            { TokenKind.Elif,"elif" },
            { TokenKind.Else,"else" },
            { TokenKind.For,"for" },
            { TokenKind.Arrow,"->" },
            { TokenKind.Break,"break" },
            { TokenKind.Continue,"continue" },
            { TokenKind.Return,"return" },


            { TokenKind.TypeString,"string" },
            { TokenKind.TypeNumber,"number" },
            { TokenKind.TypeBool,"bool" },
            { TokenKind.TypePlg,"plg" },

            { TokenKind.True,"true" },
            { TokenKind.Fasle,"false" },
            

        };



    }
}
