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

            Example();
        }

         static void Example()
         {
             var s = Amethod();
             var r = SecondMethod();
             Console.WriteLine("Returned");

             var sc = Task.Run(async () =>
             {
                 await Task.Delay(4000);
                 Console.WriteLine("Scott");
             });

             Console.WriteLine("Second Returned");

             s.ContinueWith((any) =>
             {
                 Console.WriteLine("Something");
             });

             Console.WriteLine("Third Return");
             var t = Task.WhenAll(s, r);
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
