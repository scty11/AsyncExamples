using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestingAsync
{
    class Part2Examples
    {
        public static void Example()
        {
            //used for locking.
            var l = new object();
            //accessing a shared variable in each task.
            int hits = 0;
            var tasks = new List<Task<int>>();
            for (int i = 0; i < 3; i++)
            {
                tasks.Add(Task.Run<int>(() =>
                {
                    //the tempHits is used for a lock free solution
                    //where each task does the computation and adds the result.
                    int tempHits = 0;
                    for (int a = 0; a < 100000; a++)
                    {
                        //used for simple value type operations.
                        //Interlocked.Add(ref hits, 1);

                        //lock (l)
                        //{
          
                            //hits++;
                        //}

                        
                        tempHits++;
                        
                    }
                    return tempHits;
                }));
            }
            //when using when will return a task which will not stop the program
            //we need to use a wait call. 
             Task.WhenAll(tasks.ToArray()).Wait();

             foreach (var t in tasks)
             {
                 hits += t.Result;
             }
             Console.WriteLine(hits);
        }
    }
}
