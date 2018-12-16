using System;
using System.Threading;
using System.Threading.Tasks;

namespace TEST003
{
    class Program
    {
            static void Main(string[] args)
            {
                mainMethod();
                Console.ReadKey();
            }

            public static async void mainMethod()
            {
            Task<int> taskReturnCount = ASYNC_one();

            SYNC_one("");

            int count = await taskReturnCount;
            Display(count);

            Task<int> task2 = ASYNC_two();

            SYNC_one("ciao-2");
        }

        /// <summary>
        /// ASYNC TASK print an incremental number every 15msec. When 100 prints, return the total of the prints->100
        /// </summary>
        /// <returns></returns>
        public static async Task<int> ASYNC_one()
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
                        Console.WriteLine("- > ASYNC_one - TASK:" + Task.CurrentId + " "+ System.DateTime.UtcNow.ToString () );
                        count += 1;
                        Thread.Sleep(15);
                        
                    }
                });
                return count;
            }

        /// <summary>
        /// ASYNC TASK print an incremental number every 25msec. When 100 prints, return the total of the prints->100
        /// </summary>
        /// <returns></returns>
        public static async Task<int> ASYNC_two()
        {
            int count = 0;
            await Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine("- >ASYNC_two - TASK:" + Task.CurrentId + " " + System.DateTime.UtcNow.ToString());
                    count += 1;
                    Thread.Sleep(25);

                }
            });
            return count;
        }

        /// <summary>
        /// SYNC TASK print an incremental number every 200msec. After 25 prints ends
        /// </summary>
        /// <param name="s"></param>
        public static void SYNC_one(string s)
            {
                for (int i = 0; i < 25; i++)
                {
                    Console.WriteLine("- - > SYNC_one: " + s +" - TASK:" + Task.CurrentId + " " + System.DateTime.UtcNow.ToString());
                    Thread.Sleep(200);
                }
            }

        /// <summary>
        /// Display at console the count of sync method
        /// </summary>
        /// <param name="count"></param>
        public static void Display(int count)
            {
                Console.WriteLine("- - - > Display - TASK:" + Task.CurrentId + " " + System.DateTime.UtcNow.ToString() +" , Total count is " + count);
            }
        }
    
}
