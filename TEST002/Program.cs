using System;
using System.Threading;
using System.Threading.Tasks;

namespace TEST002
{
    class Program
    {
        public static void Main(string[] args)
        {
            TEST002();
        }

        static void TEST002()
        {
            //LD nested threads managing using await. Any task will await the nested one to
            // be completed.
            EntryPoint();

            //LD "NumberGenerator" run on the main thread
            NumberGenerator();

            Console.ReadLine();
        }

        static async Task EntryPoint()
        {
            Console.WriteLine("-> EntryPoint START'");
            var task = CallASubTask();
            await task;
            Console.WriteLine("-> EntryPoint END " + " - " + task.Id);
        }

        static async Task CallASubTask()
        {
            Console.WriteLine("- - > CallASubTask START");
            var task = Wait3SecAndReturnANumber();
            int answer = await task;

            Console.WriteLine("- - > CallASubTask END - answer: " + answer + " - TASK ID: " + task.Id);
        }

        static async Task<int> Wait3SecAndReturnANumber()
        {
            Console.WriteLine("- - - > Wait3SecAndReturnANumber");

            //LD way to declare one
            var task = Task.Delay(3000);
            await task;

        

            int answer = 11 * 2;
            Console.WriteLine("- - - > Wait3SecAndReturnANumber AFTER 3 SECS " + " - TASK ID: " + task.Id);
            return answer;
        }


        static void NumberGenerator()
        {
            int i;
            for (i = 0; i < 25; i++)
            {
                Console.WriteLine("Number: {0}", i);
                Thread.Sleep(300);
            }
        }

    }
}