namespace PlgLang
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello, World!");


            var a = "123jkljasd" switch
            {
                string t when t.StartsWith("123") => "123",
                string t when t.Contains("jasd") => "abc",
                _ => throw new NotImplementedException(),
            };

            Console.WriteLine();

            /*
             
let variable1:string = "Hello World";
let variable2:number = 123123;
let variable3:bool = true;

fn sa(a:string,b:number) -> (sss:number,bbb:string) {
    if a == "" && b ==1 || b == 2 {
    }

}

print(variable);

let res:plg = sa(variable1,variable2);
let sss:string,b:string = sa(variable1,variable2);        
            
res.sss == 1

            
            
             */




        }
    }
}