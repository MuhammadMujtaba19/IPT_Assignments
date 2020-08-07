using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

/* in order to analyze equally all the value to be search would be same in q2partA, q2partB and question3
 *
 * Case 1: Average Case     Value (557246) Index = 802735  Value after Mid of file 
 * Case 2: Worst Case       Value (778451) Index = 999999  Last value of file
 * Case 1: Edge Case        Value (11111111) Index = null  Value does not exist inside the file 
 */
namespace Lab2Question2
{
    public delegate void SearchArrayCallback(int threadIndex,int index,Stopwatch watch);

    class Program
    {
        // The variables declared below is to check the case where the value is not within then array
        // Every thread would decrement the SharedVariableThreadCounter incase they didnt find the Value
        //if the value of  Counter == 0 means Value not found in the entire  array

       // The Volatile keyword is used so the value is not cached
       // inorder to maintian concurency between all the thread 
       // A change in the Variable made by one thread would be same for all other Threads working
        public static volatile int sharedVariableOfThreadCounter = 5;

        
        #region  Generate 1M random Value
        /*
         try
            {
                using (StreamWriter writer = new StreamWriter(textFile))
                    {
                        for (int i = 1; i <= 100000; i++)
                        {
                            Random r = new Random();
                            writer.WriteLine(r.Next(10,1000000));
                        }
                    }
            }
        catch (Exception exp)
            {
                Console.Write(exp.Message);
            }*/
        #endregion
        static void Main(string[] args)
        {
            


            var watch = new Stopwatch();
            var threadWatch = new Stopwatch();
            int[] valuesToSearch = { 557246, 778451, 11111111 };

            PartA withoutThread = new PartA();
            string textFile = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Values.txt");
            string[] lines = File.ReadAllLines(textFile);


            Console.WriteLine("\t-----------Part A Searching without Threads -----------\n");
            watch.Start();
            for (int j = 0; j < 3; j++)
            {
                watch.Restart();
                var x = withoutThread.searchArray(valuesToSearch[j], lines);
                watch.Stop();
                Console.WriteLine($"\n{x.Item3} Array Contains Value {x.Item1}  at Index: {x.Item2}");
                Console.WriteLine($"Execution Time : {watch.ElapsedMilliseconds} ms");

            }

            Console.WriteLine("\n\nPress Enter to Continue");
            Console.ReadKey();
                                   
            Console.WriteLine("\n\n\t-----------Part B Searching with 5 Threads -----------\n");
            for (int j = 0; j < 3; j++)
            {
                int starting = 0;
                int ending = 200000;
                Thread[] threadArray = new Thread[5];
                sharedVariableOfThreadCounter = 5;
                watch.Restart();
                for (int i = 0; i < 5; i++)
                {
                    // Callback used inorder to receive data back from the thread 
                    SearchArrayCallback searchArrayCallback = new SearchArrayCallback(searchArrayCallbackFunction);
                    threadWatch.Restart();

                    //Note. Passing the  Stopwatch variable to the helper class inorder to get the execution time of Individual thread
                    // This stopwatch would be stop and the execution time of thread would be returned via callback function and printed there
                    PartB withFiveThread = new PartB(valuesToSearch[j], i, starting, ending, searchArrayCallback, threadWatch);
                    threadArray[i] = new Thread(new ThreadStart(withFiveThread.searchArray));
                    threadArray[i].Start();
                    starting += 200000;
                    ending += 200000;
                }
                for (int i = 0; i < 5; i++)
                {
                    threadArray[i].Join();
                }
                watch.Stop();
                Console.WriteLine($"Total Execution Time of 5 Threads : {watch.ElapsedMilliseconds} ms");
                
            }
            Console.ReadKey();



        }

        public static void searchArrayCallbackFunction(int threadIndex,int index,Stopwatch threadWatch)
        {
            Interlocked.Decrement(ref sharedVariableOfThreadCounter);
            
        if (!(index < 0))
            {
                Console.WriteLine($"\nArray contains Value at Index: {index} found at Thread# {threadIndex}");
                Console.WriteLine($"Execution Time of Thread# {threadIndex}: {threadWatch.ElapsedMilliseconds} ms");
                Interlocked.Increment(ref sharedVariableOfThreadCounter);
            }
            if (sharedVariableOfThreadCounter == 0)
            {
                Console.WriteLine($"\nError! Array doesnt contain the Value");

            }
        }
    }
}
