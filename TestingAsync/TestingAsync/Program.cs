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
            //AsyncExample.Example();
            //SecondExample();
            Console.ReadLine();
                      
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

         static void SecondExample()
         {
             var t1 = Task.Run(() =>
             {
                 int abc = 0;
                 int x = 1000 / abc;
                 return x;
             });

             var t2 = Task.Run(() =>
             {
                 int abc = 0;
                  int x = 1000 / abc;
                  return x;
             });

             var t3 = t2.ContinueWith((task) => 
             {
                 int y = t2.Result;
                 return y;
             });
             try
             {
                 
                 var tasks = new List<Task<int>> { t1, t2, t3 };
                 //Task.Factory.ContinueWhenAll(tasks.ToArray(), (setOfTasks) =>
                 //{
                     
                 //});
                 //Task.Factory.ContinueWhenAny(tasks.ToArray(), (firstTask) => 
                 //{
 
                 //});
                 //var index = Task.WaitAny(tasks); 
                 //var result = tasks[index].Exception;
                 //if (result != null)
                 //{
                 //    throw result;
                 //}
                 
                 //while (tasks.Count > 0)
                 //{
                    
                 //    int winner = Task.WaitAny(tasks.ToArray());  


                 //    // was task successful?  Check exception here:
                 //    if (tasks[winner].Exception == null)  // success!
                 //    {
                 //        var result = tasks[winner].Result;
                 //        tasks.RemoveAt(winner);
                         
                 //    }

                 //    // else this task failed, wait for next to finish:
                 //    tasks.RemoveAt(winner);
                     
                 //}
                 
                 //Task.WaitAll(new Task[] { t1, t2, t3 }); //the exception if any will be thrown

                 //var tasks = new Task<int>[] { t1, t2, t3 };
                 //var result = Task.WhenAny(tasks);
                 //Console.WriteLine(result.Result.Result);//could remove this

                 //var tasks = new Task<int>[] { t1, t2, t3 };
                 //var result = Task.WhenAll(tasks);//only thrown when accessed
                 //var value = tasks[0].Result;
                 

             }
             catch (AggregateException ae)
             {
                 Console.WriteLine();

                 ae = ae.Flatten();  // could have a tree of exceptions, so flatten first:
                 foreach (Exception ex in ae.InnerExceptions)
                     Console.WriteLine("Tasking error: {0}", ex.Message);
             }
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
