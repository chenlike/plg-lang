using Plg.Compiler.Common;
using Plg.Compiler.Parsers;

namespace PlgLang
{
    internal class Program
    {
        static void Main(string[] args)
        {


            /*
             
let variable1:string = "Hello World";
let variable2:number = 123123;
let variable3:bool = true;

if(variable3){
            
}
variable2 += 1;
variable2 -= "";

for(let i:number = 0; i < 10; i++){
    variable2 += 1;        
}

fn sa(a:string,b:number) -> (number,string) {


}

print(variable);

let res:plg = sa(variable1,variable2);      
            

            
            
             */
            Logger.Enable = true;
            Parser parser = new Parser(@"

let variable1:string = ""Hello World"";
let variable2: number = 123123;
let variable3:bool = true;

if (variable3)
{

}

variable2 += 1;
variable2 -= """";

for (let i:number = 0; i < 10; i++){
    variable2 += 1;
}

fn sa(a:string, b:number) -> (number, string) {


}

print(variable);

let res:plg = sa(variable1, variable2);

");
            var scope = parser.Parse();


            Console.WriteLine();




        }
    }
}