using System;
using Question4;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace Question6
// Boolean Test Case
// i. Insertion of 1M values
// ii. Traversal of elements for to count number of true and number false 

// Float and Integer Test Case 
// i. Insertion of 1M values 
// ii. Trevarsal of elements and counting the sum of them.



// (Initial Value for DynamicArray class is 1M foreach type since it takes a great time to execute )
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = new Stopwatch();
            #region Analysis of Boolean 
            Console.WriteLine("~~~Performance Analysis of Boolean~~~\n");

            string booleanFile = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "boolValues.txt");

            DynamicArray<bool> dynamicBoolArray = new DynamicArray<bool>(1000000);
            List<bool> boolList = new List<bool>(1000000);
            bool[] boolArray = new bool[1000000];
            string[] booleanLines = File.ReadAllLines(booleanFile);

            watch.Start();
            foreach (string line in booleanLines)
            {
                dynamicBoolArray.Add(bool.Parse(line));
            }
            watch.Stop();
            Console.WriteLine($"Execution Time for Insertion in DynamicArray<bool> {watch.ElapsedMilliseconds} ms");


            watch.Restart();
            int falseSum = 0, trueSum = 0;
            for (int k = 0; k < dynamicBoolArray.curentSize; k++)
            {
                if (dynamicBoolArray.Get(k) == true)
                    trueSum ++;
                else
                    falseSum++;
            }
            watch.Stop();
            Console.WriteLine($"Number of True:- {trueSum} --- Number of False:- {falseSum}");
            Console.WriteLine($"Execution Time for Traversal in DynamicArray<bool> {watch.ElapsedMilliseconds} ms\n");


            
            watch.Restart();
            int i = 0;
            foreach (string line in booleanLines)
            {
                boolArray[i]=(bool.Parse(line));
                i++;
            }
            watch.Stop();
            Console.WriteLine($"Execution Time for Insertion in C# array {watch.ElapsedMilliseconds} ms");

            watch.Restart();
            falseSum = 0; trueSum = 0;
            for (int k = 0; k < boolArray.Length; k++)
            {
                if (boolArray[k]== true)
                    trueSum++;
                else
                    falseSum++;
            }
            watch.Stop();
            Console.WriteLine($"Number of True:- {trueSum} --- Number of False:- {falseSum}");
            Console.WriteLine($"Execution Time for Traversal in C# Array {watch.ElapsedMilliseconds} ms\n");



            watch.Restart();
            foreach (string line in booleanLines)
            {
                boolList.Add(bool.Parse(line));
            }
            watch.Stop();
            Console.WriteLine($"Execution time for Insertion in Generic List<> {watch.ElapsedMilliseconds} ms");


            watch.Restart();
            falseSum = 0; trueSum = 0;
            for (int k = 0; k < boolList.Count; k++)
            {
                if (boolList[k] == true)
                    trueSum++;
                else
                    falseSum++;
            }
            watch.Stop();
            Console.WriteLine($"Number of True:- {trueSum} ---- Number of False:- {falseSum}");
            Console.WriteLine($"Execution Time for Traversal in Generic List<bool> {watch.ElapsedMilliseconds} ms\n");
            #endregion


            #region Analysis of Decimal
            Console.WriteLine("\n\n~~~Performance Analysis of Decimal~~~\n");

            string decimalFile = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "floatValues.txt");

            DynamicArray<double> dynamicFloatArray = new DynamicArray<double>(1000000);
            List<double> floatList = new List<double>();
            double[] floatArray = new double[1000000]; 
            string[] decimalLines = File.ReadAllLines(decimalFile);

            watch.Restart();
            foreach (string line in decimalLines)
            {
                dynamicFloatArray.Add(double.Parse(line));
            }
            watch.Stop();
            Console.WriteLine($"Execution Time for Insertion in DynamicArray<double> {watch.ElapsedMilliseconds} ms");

            watch.Restart();
            double sum = 0.0f;
            for (int k = 0; k < dynamicFloatArray.curentSize; k++)
            {
                sum = sum + dynamicFloatArray.Get(k);
            }
            watch.Stop();
            Console.WriteLine($"Sum of Elements in DynamicArray:- {sum}");
            Console.WriteLine($"Execution Time for Traversal in DynamicArray<double>: {watch.ElapsedMilliseconds} ms\n");


            watch.Restart();
            int j = 0;
            foreach (string line in decimalLines)
            {
                floatArray[j] = (double.Parse(line));
                j++;
            }
            watch.Stop();
            Console.WriteLine($"Execution Time for Insertion in C# Array {watch.ElapsedMilliseconds} ms");
            
            
            watch.Restart();
            sum = 0.0f;
            for (int k = 0; k < floatArray.Length; k++)
            {
                sum = sum + floatArray[k];
            }
            watch.Stop();
            Console.WriteLine($"Sum of Elements in Array:- {sum}");
            Console.WriteLine($"Execution Time for Traversal in C# Array: {watch.ElapsedMilliseconds} ms\n");


            watch.Restart();
            foreach (string line in decimalLines)
            {
                floatList.Add(double.Parse(line));
            }
            watch.Stop();
            Console.WriteLine($"Execution Time for Insertion in Generic List<> {watch.ElapsedMilliseconds} ms");

            watch.Restart();
            sum = 0.0f;
            for (int k = 0; k < floatList.Count; k++)
            {
                sum = sum + floatList[k];
            }
            watch.Stop();
            Console.WriteLine($"Sum of Elements in List<>:- {sum}");
            Console.WriteLine($"Execution Time for Traversal in Generic List<>: {watch.ElapsedMilliseconds} ms\n");

            #endregion


            #region Analysis of Integer 
            Console.WriteLine("\n\n~~~Performance Analysis of Integer~~~\n");

            string integerFile = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "intValues.txt");

            DynamicArray<int> dynamicIntArray = new DynamicArray<int>(1000000);
            List<int> intList = new List<int>();
            int[] intArray = new int[1000000];
            string[] integerLines = File.ReadAllLines(integerFile);

            watch.Restart();
            foreach (string line in integerLines)
            {
                dynamicIntArray.Add(int.Parse(line));
            }
            watch.Stop();
            Console.WriteLine($"Execution Time for Insertion in DynamicArray<double> {watch.ElapsedMilliseconds} ms");

            watch.Restart();
            int sumx = 0;
            for (int k = 0; k < dynamicIntArray.curentSize; k++)
            {
                sumx = sumx + dynamicIntArray.Get(k);
            }
            watch.Stop();
            Console.WriteLine($"Sum of Elements in DynamicArray:- {sumx}");
            Console.WriteLine($"Execution Time for Traversal in DynamicArray<int>: {watch.ElapsedMilliseconds} ms\n");


            watch.Restart();
            i = 0;
            foreach (string line in integerLines)
            {
                intArray[i]= (int.Parse(line));
            }
            watch.Stop();
            Console.WriteLine($"Execution Time for Insertion in C# Array: {watch.ElapsedMilliseconds} ms");

            watch.Restart();
            sumx = 0;
            for (int k = 0; k < intArray.Length; k++)
            {
                sumx = sumx + intArray[k];
            }
            watch.Stop();
            Console.WriteLine($"Sum of Elements in C# Array:- {sumx}");
            Console.WriteLine($"Execution Time for Traversal in C# Array: {watch.ElapsedMilliseconds} ms\n");


            watch.Restart();
            foreach (string line in integerLines)
            {
                intList.Add(int.Parse(line));
            }
            watch.Stop();
            Console.WriteLine($"Execution Time for Insertion in Generic List<int> {watch.ElapsedMilliseconds} ms");

            watch.Restart();
             sumx = 0;
            for (int k = 0; k < dynamicIntArray.curentSize; k++)
            {
                sumx = sumx + intList[k];
            }
            watch.Stop();
            Console.WriteLine($"Sum of Elements in List<int>:- {sumx}");
            Console.WriteLine($"Execution Time for Traversal in Generic List<int>: {watch.ElapsedMilliseconds} ms\n");


            #endregion


            #region code to Generate Random 1M values 
            /*
             
           try
                {
                    using (StreamWriter writer = new StreamWriter(booleanFile))
                        {
                            for (int i = 1; i <= 1000000; i++)
                            {
                                Random r = new Random();
                                writer.WriteLine(r.Next(2)==1);
                            }
                        }
                }
            catch (Exception exp)
                {
                    Console.Write(exp.Message);
                }

            try
            {
                using (StreamWriter writer = new StreamWriter(decimalFile))
                {
                    for (int i = 1; i <= 1000000; i++)
                    {
                        Random r = new Random();
                        writer.WriteLine(r.NextDouble());
                    }
                }
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(integerFile))
                {
                    for (int i = 1; i <= 1000000; i++)
                    {
                        Random r = new Random();
                        writer.WriteLine(r.Next(1,10000));
                    }
                }
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
            }*/
            #endregion

        }
    }
}
