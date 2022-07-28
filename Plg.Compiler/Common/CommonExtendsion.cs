using Plg.Compiler.AST.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Plg.Compiler.Common
{
    public static class CommonExtendsion
    {
        public static string ToJsonString(this object obj)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            var serializeString = JsonSerializer.Serialize(obj, options);
            return serializeString;
        }
    }
}
