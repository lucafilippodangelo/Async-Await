using System;
using System.Threading;
using System.Threading.Tasks;

namespace TEST001
{
    class Program
    {
        static void Main(string[] args)
        {
            //LD use of cancellation tocken
            TEST001();
        }


        public static void TEST001()
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
    }
}
