using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingAsync
{
    class Program
    {
         static void Main(string[] args)
        {

            //Example();
            AsyncExample.Example();
           
                      
        }

         static void Example()
         {
             var s = Amethod();
             var r = SecondMethod();

             Console.WriteLine("Returned");

             var sc = Task.Run(async () =>
             {
                 await Task.Delay(1000);
                 Console.WriteLine("Scott");
             });

             Console.WriteLine("Second Returned");

             sc.ContinueWith((any) =>
             {
                 Console.WriteLine("Something");
             });
             //s.Wait();
             Console.WriteLine("Third Return");
             var t = Task.WhenAll(new Task[] { s, r});          
             //r.Wait();
             Console.WriteLine(s.Result + " " + r.Result);

            
             Console.WriteLine("Finnished");
             Console.ReadLine();
         }
        static async Task<string> Amethod()
        {
            await Task.Delay(6000);
            return "Scott Wright";
        }

        static async Task<string> SecondMethod()
        {
            await Task.Delay(3000);
            return "Lee Clark";
        }

        
    }
}
