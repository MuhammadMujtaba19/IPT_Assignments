using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2Question3
{
    public delegate void SearchArrayCallback(int threadIndex,int index);
    class Program
    {
        public static volatile int sharedVariableOfThreadCounter = 5;

        static void Main(string[] args)
        {
            var watch = new Stopwatch();
            int[] valuesToSearch = { 557246, 778451, 11111111 };

            for (int j = 0; j < 3; j++)
            {
                int starting = 0;
                int ending = 200000;
                sharedVariableOfThreadCounter = 5;
                Thread[] threadArray = new Thread[5];
                watch.Restart();

                for (int i = 0; i < 5; i++)
                {
                    SearchArrayCallback callback = new SearchArrayCallback(searchArrayCallbackFunction);
                    HelperClass withFiveThreadOneThreadAtaTime = new HelperClass(valuesToSearch[j], i, starting, ending, callback);
                    threadArray[i] = new Thread(new ThreadStart(withFiveThreadOneThreadAtaTime.searchArray));
                    threadArray[i].Start();
                    starting += 200000;
                    ending += 200000;
                }
                for (int i = 0; i < 5; i++)
                {
                    threadArray[i].Join();
                }
                watch.Stop();
                Console.WriteLine($"Execution Time : {watch.ElapsedMilliseconds} ms");
            }
            Console.ReadKey();

        }
        public static void searchArrayCallbackFunction(int threadIndex, int index)
        {
            Interlocked.Decrement(ref sharedVariableOfThreadCounter);

            if (!(index < 0))
            {
                Console.WriteLine($"\nArray contains Value at Index: {index} at Thread# {threadIndex}");
                Interlocked.Increment(ref sharedVariableOfThreadCounter);

            }
            if (sharedVariableOfThreadCounter == 0)
            {
                Console.WriteLine($"\nError! Array doesnt contain the Value");

            }
        }
    }
}
