using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TestingAsync
{
    class Program
    {
        
         static void Main(string[] args)
        {

            //Example();
            //AsyncExample.Example();
            //SecondExample();
            //ParamTaskExample();
            //Console.ReadLine();
            Part2Examples.Example();
            Console.ReadKey();
        }

         static void Example()
         {
             CancellationTokenSource c1 = new CancellationTokenSource();
             //CancellationTokenSource c2;
             //var s = Amethod();
             //var r = SecondMethod();

             Console.WriteLine("Returned");
             try
             {
                 var token = c1.Token;
                 var sc = Task.Run(async () =>
                 {
                     await Task.Delay(2000);
                     token.ThrowIfCancellationRequested();
                     Console.WriteLine("Scott");
                    

                 }, token);
                 c1.Cancel();
                 Console.WriteLine("Second Returned");
                 sc.Wait();
                 sc.ContinueWith((any) =>
                 {
                     if (any.Exception != null)
                         throw any.Exception;
                     Console.WriteLine("Something");
                     
                 });
             }
             catch (AggregateException ae)
             {
                 Console.WriteLine();

                 ae = ae.Flatten();  // could have a tree of exceptions, so flatten first:
                 foreach (Exception ex in ae.InnerExceptions)
                     Console.WriteLine("Tasking error: {0}", ex.Message);
             }
             //s.Wait();
             Console.WriteLine("Third Return");
             //var t = Task.WhenAll(new Task[] { s, r});          
             //r.Wait();
             //Console.WriteLine(s.Result + " " + r.Result);

            
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
                 
                 //var tasks = new List<Task<int>> { t1, t2, t3 };
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
                 
                 //Task.WaitAll(new Task[] { t1, t2, t3 }); //all exceptions if any will be thrown

                 var tasks = new Task<int>[] { t1, t2, t3 };
                 //will be thrown when the result prop is accessed.
                 var result = Task.WhenAny(tasks);
                 result.Wait();
                 Console.WriteLine(result.Result.Result);//could remove this

                 //var tasks = new Task<int>[] { t1, t2, t3 };
                // var result = Task.WhenAll(tasks);//only thrown when accessed
                 //var value = tasks[0].Result;
                 //Console.ReadLine();

             }
             catch (AggregateException ae)
             {
                 Console.WriteLine();

                 ae = ae.Flatten();  // could have a tree of exceptions, so flatten first:
                 foreach (Exception ex in ae.InnerExceptions)
                     Console.WriteLine("Tasking error: {0}", ex.Message);
             }
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

        static void ParamTaskExample()
        {
            //if not passed in each may have 10 as the 
            //loop may have finnised beofre the threads are actually started.
            for (int i = 0; i < 10; i++)
            {
                var t = Task.Factory.StartNew((param) => 
                {
                    int result = (int)param;
                    Console.WriteLine("The result: {0}", param);
                    
                },i);
            }
        }
        
    }
}
