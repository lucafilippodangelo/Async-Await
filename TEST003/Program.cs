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
                Task<int> task = Method1();

            //LD play with "Thread.Sleep(10);" in method2 to see the different behaviour
            //this execution is sequencial, once started has to finish, before to evaluate the code below
            // so if the Method1 finish the execution before method2, if the method two is still running
            // the method3 will kickoff after the end of the execution of the Method2(and of course the end of Method1) 
            Method2();

            int count = await task;
                Method3(count);
            }

            public static async Task<int> Method1()
            {
                int count = 0;
                await Task.Run(() =>
                {
                    for (int i = 0; i < 100; i++)
                    {
                        Console.WriteLine("- > Method 1 - TASK:" + Task.CurrentId + " "+ System.DateTime.UtcNow.ToString () );
                        count += 1;
                        Thread.Sleep(25);
                    }
                });
                return count;
            }


            public static void Method2()
            {
                for (int i = 0; i < 25; i++)
                {
                    Console.WriteLine("- - > Method 2 - TASK:" + Task.CurrentId + " " + System.DateTime.UtcNow.ToString());
                    Thread.Sleep(10);
            }
            }


            public static void Method3(int count)
            {
                Console.WriteLine("- - - > Method 3 - TASK:" + Task.CurrentId + " " + System.DateTime.UtcNow.ToString() +" , Total count is " + count);
            }
        }
    
}
