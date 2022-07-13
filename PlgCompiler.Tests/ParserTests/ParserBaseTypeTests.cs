using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgCompiler.Tests.ParserTests
{
    public class ParserBaseTypeTests
    {
        [Test]
        public void ParseString()
        {
            PlgParser parser = new PlgParser("\"123456789 0\"");
            string v = parser.ParseString();

            
            Assert.That(v, Is.EqualTo("123456789 0"));
        }


        
    }
}
