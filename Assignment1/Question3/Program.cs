 using System;
using System.IO;
using Question2;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
namespace Question3
{

    /* Question 
     *a) Populate the collections by generating 1M(one million) random values
       b) Carry out traversal and find the sum of the element values.Print the sum and the time required to carry out the traversal in each case.
       c) Search five randomly chosen values from each of the collection by calling the IndexOf() function and compare response times 
                  i. 722100 (70119)  ii. 786311 (338399)  iii. 820266 (663595)  iv. 557246 (802736)  v. 495133 (960864)  */


    /*  Summary of analysis 
        i. The speed of insertion in DynamicIntArray is very slow as compared to other Collections. It dependent upon the initial value used to allocate the DynamicArray. 
        if the initial value is to small the insertion is too slow and a great time (approx 15mins) while if the initial value is too large it consumes a lot of Process memory 
        but speeds the insertion. The traversal and Searching speed are acceptable yet slowest from other collections

        ii. The Traditional C# array have a draw back as they cant resize them selves one allocated a size. so after the limit they reject to insert any more elements. so 
        we cant get accurate answer about the sum of elements and searching return -1 as elements cant be find within the limit capacity
        
        iii. System.Collection ArrayList and Generics Class List<T> were the fastest in Insertion of all as they didnot need any size at the time of declaration and they 
        grow as the elements increases. since ArrayList converts every Data type to Object type so while retreving we explictly need to unbox the value and then use it to
        compare(Searching) and calculate sum (Traversing) therefore ArrayList is a bit overhead and is slower then List<> while Generic List<> is Fastest of all collections
        at the time of initialization we need to mention its dataType hence the overhead to unbox also reduces



     */
    class Program
    {

        static void Main(string[] args)
        {
            string textFile = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Values.txt");
            var watch = new Stopwatch();
            string[] lines = File.ReadAllLines(textFile);

            #region  Code for Generation if 1M random Value
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

            DynamicIntArray dynamicIntArray = new DynamicIntArray(500000);
            int[] myArray = new int[100000];
            List<int> myList = new List<int>();
            ArrayList myArrayList = new ArrayList();
            

            #region DynamicIntArray Analysis
            Console.WriteLine("Performanace Anaylsis of Custom DynamicIntArray");
            watch.Start();
            int j = 1;
            foreach (string line in lines)
            {
                Console.WriteLine(j);
                dynamicIntArray.Add(Convert.ToInt32(line));
                j++;
            }
            watch.Stop();
            Console.WriteLine($"Execution Time for Insertion: {watch.ElapsedMilliseconds} ms");

            
            watch.Restart();
            int sum = 0;
            for (int i = 0; i < dynamicIntArray.curentSize; i++)
            {
                sum = sum + dynamicIntArray.Get(i);
            }
            watch.Stop();
            Console.WriteLine($"Sum of Elements in DynamicIntArray class:- {sum}");
            Console.WriteLine($"Execution Time for Traversal: {watch.ElapsedMilliseconds} ms");


            watch.Restart();
            Console.WriteLine($"Index of 722100:- {dynamicIntArray.indexOf(722100)}");
            Console.WriteLine($"Index of 786311:- {dynamicIntArray.indexOf(786311)}");
            Console.WriteLine($"Index of 820266:- {dynamicIntArray.indexOf(820266)}");
            Console.WriteLine($"Index of 557246:- {dynamicIntArray.indexOf(557246)}");
            Console.WriteLine($"Index of 495133:- {dynamicIntArray.indexOf(495133)}");
            watch.Stop();
            Console.WriteLine($"Execution Time for Searching: {watch.ElapsedMilliseconds} ms");
            #endregion

            
            #region C# Array Analysis
            Console.WriteLine("\n\n\nPerformanace Anaylsis of C# Array Datastructure");
            watch.Start();
             j = 0;
            foreach (string line in lines)
            {
                if (myArray.Length <= j)
                {
                    Console.WriteLine("Cant Insert more In Array");
                    break;
                }
                else
                {
                    myArray[j] = (Convert.ToInt32(line));
                    j++;
                }
            }
            watch.Stop();
            Console.WriteLine($"Execution Time for Insertion: {watch.ElapsedMilliseconds} ms");
            
            watch.Restart();
             sum = 0;
            for (int i = 0; i < myArray.Length; i++)
            {
                sum = sum + myArray[i];
            }
            watch.Stop();
            Console.WriteLine($"Sum of Elements in Array:- {sum}");
            Console.WriteLine($"Execution Time for Traversal: {watch.ElapsedMilliseconds} ms");

            watch.Restart();
            Console.WriteLine($"Index of 722100:- {Array.FindIndex(myArray, item => item == 722100)}");
            Console.WriteLine($"Index of 786311:- {Array.FindIndex(myArray, item => item == 786311)}");
            Console.WriteLine($"Index of 820266:- {Array.FindIndex(myArray, item => item == 820266)}");
            Console.WriteLine($"Index of 557246:- {Array.FindIndex(myArray, item => item == 557246)}");
            Console.WriteLine($"Index of 495133:- {Array.FindIndex(myArray, item => item == 495133)}");
            watch.Stop();
            Console.WriteLine($"Execution Time for Searching: {watch.ElapsedMilliseconds} ms");

            #endregion

            
            #region Generic List<Int> Analysis 

            //Analysis for List<int> generics
            Console.WriteLine("\n\n\nPerformanace Anaylsis of System.Generics List<> ");
            
            watch.Start();
                foreach (string line in lines)
                {
                    myList.Add(Convert.ToInt32(line));
                }
                watch.Stop();
                Console.WriteLine($"Execution Time for Insertion: {watch.ElapsedMilliseconds} ms");

                watch.Restart();
                sum = 0;
                for (int i = 0; i < myList.Count; i++)

                {
                    sum = sum + myList[i];
                }
                watch.Stop();
                Console.WriteLine($"Sum of elements in List<>:- {sum}");
                Console.WriteLine($"Execution Time for Treversal: {watch.ElapsedMilliseconds} ms");

            watch.Restart();
            Console.WriteLine($"Index of 722100:- {myList.IndexOf(722100)}");
            Console.WriteLine($"Index of 786311:- {myList.IndexOf(786311)}");
            Console.WriteLine($"Index of 820266:- {myList.IndexOf(820266)}");
            Console.WriteLine($"Index of 557246:- {myList.IndexOf(557246)}");
            Console.WriteLine($"Index of 495133:- {myList.IndexOf(495133)}");
            watch.Stop();
            Console.WriteLine($"Execution Time for Searching: {watch.ElapsedMilliseconds} ms");

            #endregion
        
            
            #region System.Collections ArrayList Analysis
            Console.WriteLine("\n\n\nPerformanace Anaylsis of System.Collection ArrayList");

            watch.Start();
                foreach (string line in lines)
                {
                    myArrayList.Add(Convert.ToInt32(line));
                }
                watch.Stop();
                Console.WriteLine($"Execution Time for Insertion: {watch.ElapsedMilliseconds} ms");

                watch.Restart();
                 sum = 0;
                for (int i = 0; i < myArrayList.Count; i++)
                {
                    sum = sum + (int)myArrayList[i];
                }
                watch.Stop();
                Console.WriteLine($"Sum of Elements in ArrayList:- {sum}");
                Console.WriteLine($"Execution Time for Treversal: {watch.ElapsedMilliseconds} ms");


            watch.Restart();
            Console.WriteLine($"Index of 722100:- {myArrayList.IndexOf(722100)}");
            Console.WriteLine($"Index of 786311:- {myArrayList.IndexOf(786311)}");
            Console.WriteLine($"Index of 820266:- {myArrayList.IndexOf(820266)}");
            Console.WriteLine($"Index of 557246:- {myArrayList.IndexOf(557246)}");
            Console.WriteLine($"Index of 495133:- {myArrayList.IndexOf(495133)}");
            watch.Stop();
            Console.WriteLine($"Execution Time for Searching: {watch.ElapsedMilliseconds} ms");
            #endregion


        }


    }
}



