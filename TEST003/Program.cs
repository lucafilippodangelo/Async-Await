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
            Task<int> taskReturnCount = AsyncPrintAndSleep15();

            ////LD SAME CODE POSITION ONE if I put the code here, "SinkMethodPrintEvery200" will wait until the return of "taskReturnCount"
            //int count = await taskReturnCount;

            //SincMethodDisplayCount(count);

            SyncPrintAndSleep200("");

            //LD SAME CODE POSITION TWO if I put the code here, "SincMethodDisplayCount(count)" will wait until "SyncPrintAndSleep200" is complete
            int count = await taskReturnCount;
            SincMethodDisplayCount(count);

            //LD will be called after "SincMethodDisplayCount(count)" if "LD SAME CODE POSITION TWO" uncommented
            // otherwise after the end of the execution of  "SyncPrintAndSleep200("")";
            Task<int> task2 = AsyncPrintAndSleep25();

            //LD will be called after "SincMethodDisplayCount(count)" if "LD SAME CODE POSITION TWO" uncommented
            // otherwise after the end of the execution of "SyncPrintAndSleep200("")";
            SyncPrintAndSleep200("ciao-2");
        }

        /// <summary>
        /// ASYNC TASK print an incremental number every 15msec. When 100 prints, return the total of the prints->100
        /// </summary>
        /// <returns></returns>
        public static async Task<int> AsyncPrintAndSleep15()
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
                        Console.WriteLine("- > AsyncPrintAndSleep15 - TASK:" + Task.CurrentId + " "+ System.DateTime.UtcNow.ToString () );
                        count += 1;
                        Thread.Sleep(15);
                        
                    }
                });
                return count;
            }

        /// <summary>
        /// SYNC TASK print an incremental number every 200msec. After 25 prints ends
        /// </summary>
        /// <param name="s"></param>
        public static void SyncPrintAndSleep200(string s)
            {
                for (int i = 0; i < 25; i++)
                {
                    Console.WriteLine("- - > SinkMethodPrintEvery200: " + s +" - TASK:" + Task.CurrentId + " " + System.DateTime.UtcNow.ToString());
                    Thread.Sleep(200);
                }
            }

        /// <summary>
        /// ASYNC TASK print an incremental number every 25msec. When 100 prints, return the total of the prints->100
        /// </summary>
        /// <returns></returns>
        public static async Task<int> AsyncPrintAndSleep25()
        {
            int count = 0;
            await Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine("- >AsyncPrintAndSleep25 - TASK:" + Task.CurrentId + " " + System.DateTime.UtcNow.ToString());
                    count += 1;
                    Thread.Sleep(25);

                }
            });
            return count;
        }

        /// <summary>
        /// Display at consolle the count of sync method
        /// </summary>
        /// <param name="count"></param>
        public static void SincMethodDisplayCount(int count)
            {
                Console.WriteLine("- - - > SincMethodDisplayCount - TASK:" + Task.CurrentId + " " + System.DateTime.UtcNow.ToString() +" , Total count is " + count);
            }
        }
    
}
