using System;
using System.Collections.Generic;
using QuickSort;
namespace Question7
{
    class Program
    {
        static unsafe void Main(string[] args)
        {

            quickSort q= new quickSort();

            int[] array ;
            
            //array = { 99,11,88,22,77,33,66,44,55,02};
            //int size = array.Length;
            
            Console.Write("sizeOf Array: ");
            int size = Convert.ToInt32(Console.ReadLine());
            array= new int[size];
            Console.WriteLine("\nEnter: ");
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write("\tElement no. " + (i + 1) + ": ");
                array[i] = Convert.ToInt32(Console.ReadLine());
            }

            fixed (int* ptr = array)
            {
                Console.WriteLine("Unsorted Array");
                for (int i = 0; i < array.Length; i++)
                    Console.Write(*(ptr + i) + " ");


                q.Sort(ptr, 0, size-1);
                
                
                Console.WriteLine("\n\n\nSorted Array");
                for (int i = 0; i < array.Length; i++) 
                    Console.Write(*(ptr + i) + " ");
            }
        }
    }
}
