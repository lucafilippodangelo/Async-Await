using System;
using System.Threading;
using System.Threading.Tasks;

namespace TEST003
{
    class Program
    {
            static void Main(string[] args)
            {
                callMethod();
                Console.ReadKey();
            }

            public static async void callMethod()
            {
            Task<int> taskReturnCount = AsyncMethodReturnACountAtTheEndSleep15AndPrint();

            ////LD SAME CODE POSITION ONE if I put the code here, "SinkMethodPrintEvery200" will wait until the return of "taskReturnCount"
            //int count = await taskReturnCount;
            //SincMethodDisplayCount(count);

            SinkMethodPrintEvery200("");

            //LD SAME CODE POSITION TWO if I put the code here, "SincMethodDisplayCount(count)" will wait until "SinkMethodPrintEvery200" is complete
            int count = await taskReturnCount;
            SincMethodDisplayCount(count);

            //LD will be called after "SincMethodDisplayCount(count)" if "LD SAME CODE POSITION TWO" uncommented
            // otherwise after the end of the execution of  "SinkMethodPrintEvery200("")";
            Task<int> task2 = AsyncMethodReturnACountAtTheEndSleep25AndPrint();

            //LD will be called after "SincMethodDisplayCount(count)" if "LD SAME CODE POSITION TWO" uncommented
            // otherwise after the end of the execution of "SinkMethodPrintEvery200("")";
            SinkMethodPrintEvery200("ciao-2");
        }

        public static async Task<int> AsyncMethodReturnACountAtTheEndSleep15AndPrint()
            {
                int count = 0;

            /*
            In .NET 4.5, the Task type exposes a static Run method as a shortcut to StartNew and which may be 
            used to easily launch a compute-bound task that targets the ThreadPool. As of .NET 4.5, this
            is the preferred mechanism for launching a compute-bound task; 
            */
            await Task.Run(() =>
                {
                    for (int i = 0; i < 100; i++)
                    {
                        Console.WriteLine("- > AsyncMethodReturnACountAtTheEndSleep15AndPrint - TASK:" + Task.CurrentId + " "+ System.DateTime.UtcNow.ToString () );
                        count += 1;
                        Thread.Sleep(15);
                        
                    }
                });
                return count;
            }

        public static void SinkMethodPrintEvery200(string s)
            {
                for (int i = 0; i < 25; i++)
                {
                    Console.WriteLine("- - > SinkMethodPrintEvery200: " + s +" - TASK:" + Task.CurrentId + " " + System.DateTime.UtcNow.ToString());
                    Thread.Sleep(200);
                }
            }

        public static async Task<int> AsyncMethodReturnACountAtTheEndSleep25AndPrint()
        {
            int count = 0;
            await Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine("- >AsyncMethodReturnACountAtTheEndSleep25AndPrint - TASK:" + Task.CurrentId + " " + System.DateTime.UtcNow.ToString());
                    count += 1;
                    Thread.Sleep(25);

                }
            });
            return count;
        }

        public static void SincMethodDisplayCount(int count)
            {
                Console.WriteLine("- - - > SincMethodDisplayCount - TASK:" + Task.CurrentId + " " + System.DateTime.UtcNow.ToString() +" , Total count is " + count);
            }
        }
    
}
