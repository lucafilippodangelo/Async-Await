using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace AsyncAwaitDemoProject
{
    class Program
    {
        static string ipAdr = "0.0.0.0";

        static void Main(string[] args)
        {

            //LD TEST 001 usage of tasks with cancellation token
            //TEST001();

            //LD test 002 usage of async await with main thread, 
            // kick off a timer to make console prints in a main thread and in the mean time calls to async tasks
            TEST002();
        }


        #region REGION metodi utili per test 002

        static void TEST002()
        {
            //LD this test is just to execute, easy figure out how nested threads are managed
            EntryPoint();
            NumberGenerator();

            Console.ReadLine();
        }

        static async Task EntryPoint()
        {
            Console.WriteLine("Print from 'EntryPoint'");
            var task = CallASubTask();
            await task;
            Console.WriteLine("Scrivo dal metodo EntryPoint() " + " - " + task.Id);
        }

        static async Task CallASubTask()
        {
            Console.WriteLine("Print from 'CallASubTask'");
            var task = Wait3SecAndReturnANumber();
            int answer = await task;

            Console.WriteLine("Print from 'CallASubTask' with answer: " + answer + " - TASK ID: " + task.Id);
        }

        static async Task<int> Wait3SecAndReturnANumber()
        {
            Console.WriteLine("Print from 'Wait3SecAndReturnANumber'");
            var task = Task.Delay(3000);
            await task;

            int answer = 11 * 2;
            Console.WriteLine("Print from 'Wait3SecAndReturnANumber' after wait 3 sec " + " - TASK ID: " + task.Id);
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

        #endregion






        #region REGION metodi utili per test 001

        static void TEST001()
        {
            var cTokenSource = new CancellationTokenSource();
            //LD any token created from this cancellation source will expire in 10000ms
            cTokenSource.CancelAfter(10000);
            //LD Create a cancellation token from CancellationTokenSource
            var cToken = cTokenSource.Token;


            // Create a task to run "GenerateNumbers" and pass the cancellation token
            var t1 = Task<int>.Factory.StartNew(() => GenerateNumbers(cToken, " First call to: GenerateNumbers "), cToken);

            // Create a task to run "GenerateNumbersNoToken" 
            var t2 = Task<int>.Factory.StartNew(() => GenerateNumbersNoToken(" First call to: GenerateNumbers No Token "));

            //LDregister a delegate for a callback when a cancellation request is made
            cToken.Register(() => cancelNotification()); 
            Console.ReadLine();
            
        }

        static int GenerateNumbers(CancellationToken ct, string str)
        {
            Console.WriteLine(str);
            int i;
            for (i = 0; i < 5000; i++)
            {
                Console.WriteLine("Generate Numbers, FOR id:{0}, then sleep 1500ms", i);
                Thread.Sleep(1500);

                //LD poll the IsCancellationRequested property to check if cancellation was requested 
                if (ct.IsCancellationRequested) 
                { break; }
            }
            return i;
        }

        static int GenerateNumbersNoToken(string str)
        {
            Console.WriteLine(str);
            int i;
            for (i = 0; i < 20; i++)
            {
                Console.WriteLine("Generate Numbers No Token, FOR id:{0}/20, then sleep 1500ms", i);
                Thread.Sleep(1500);
            }
            return i;
        }

        static void cancelNotification()
        {
            Console.WriteLine("Delegate Subscriber method - t1 stopped notification!");
        }

        #endregion 

    }
}