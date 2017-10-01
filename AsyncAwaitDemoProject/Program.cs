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
            // TEST 001 usage of tasks with cancellation token
            TEST001();

            // test 002 usage of async await with main thread, 
            // kick off a timer to make console prints in a main thread and in the mean time calls to async tasks
            //TEST002();
        }


        #region REGION metodi utili per test 002

        static void TEST002()
        {
            Go();
            generaNumeri();


            Console.ReadLine();

        }

        static async Task Go()
        {
            var task = PrintAnswerToLife();
            await task;
            Console.WriteLine("Scrivo dal metodo Go() ");
        }

        static async Task PrintAnswerToLife()
        {
            var task = GetAnswerToLife();
            int answer = await task;
            Console.WriteLine("Scrivo la risposta: " + answer);
        }

        static async Task<int> GetAnswerToLife()
        {
            var task = Task.Delay(3000);
            await task;

            int answer = 11 * 2;
            Console.WriteLine("Che fantastico ho generato la risposta dopo 3 secondi ");
            return answer;
        }


        static int generaNumeri()
        {
            int i;
            for (i = 0; i < 5000; i++)
            {
                Console.WriteLine("Number: {0}", i);
                Thread.Sleep(300);
            }
            return i;
        }

        #endregion






        #region REGION metodi utili per test 001

        static void TEST001()
        {

            var cTokenSource = new CancellationTokenSource();
            cTokenSource.CancelAfter(5000);

            getIp();
            ipAdr = ipAdr + " finish main thread!";
            var cToken = cTokenSource.Token;// Create a cancellation token from CancellationTokenSource
            var t1 = Task<int>.Factory.StartNew(() => GenerateNumbers(cToken, " E N T E R "), cToken); // Create a task and pass the cancellation token
            cToken.Register(() => cancelNotification()); // to register a delegate for a callback when a cancellation request is made
            Console.ReadLine();
            Thread.Sleep(3100);
        }

        static int GenerateNumbers(CancellationToken ct, string ciao)
        {
            int i;
            for (i = 0; i < 5000; i++)
            {
                Console.WriteLine("Method1 - Number: {0}", i); ipAdr = "1.1.1.1 Rafa we got an ip address before than 3 second!";
                Thread.Sleep(1500);
                if (ct.IsCancellationRequested) // poll the IsCancellationRequested property to check if cancellation was requested 
                { break; }
            }
            return i;
        }

        // Notify when task is cancelled 
        static void cancelNotification()
        {
            Console.WriteLine("DELEGATE Final IP: " + ipAdr);
        }

        static string getIp()
        {
            Thread.Sleep(5500);
            string externalIP = "12345";//(new WebClient() { Proxy = null }).DownloadString("http://checkip.dyndns.org/"); return externalIP; //inserire laa classe per il timeout 
            return externalIP;
        }

        #endregion 

    }
}