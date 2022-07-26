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


            { TokenKind.TypeString,"string" },
            { TokenKind.TypeNumber,"number" },
            { TokenKind.TypeBool,"bool" },

            { TokenKind.True,"true" },
            { TokenKind.Fasle,"false" },

        };



    }
}