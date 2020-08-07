using System;
using System.Collections.Generic;

namespace Question2
{

    public sealed class DynamicIntArray
    {
        public int curentSize;
        public int currentCapacity;
        public int [] array;

        public DynamicIntArray()
        {
            array = new int[10];
            curentSize = 0;
            currentCapacity = 10;
        }
        public DynamicIntArray(int x)
        {
            array = new int[x];
            curentSize = 0;
            currentCapacity = x;

        }
        public void Add(int value)
        {
            if (currentCapacity == 0 && curentSize == array.Length)
            { 
                Array.Resize<int>(ref array, array.Length+1);    
            }
            else { 
                currentCapacity--;
            }
            array[curentSize] = value;

            curentSize++;
        }
        public int Get(int index)
        {
            return array[index];
        }

        public int indexOf(int value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if(array[i] == value)
                {
                    return i;
                }
            }
            return -1;
        }
    }
    class Program
    {
        static void Main(string[] args)       
        {
            DynamicIntArray dynamicIntArray = new DynamicIntArray(5);
            dynamicIntArray.Add(1);
            dynamicIntArray.Add(2);
            dynamicIntArray.Add(3);
            dynamicIntArray.Add(4);
            dynamicIntArray.Add(5);
            dynamicIntArray.Add(6);
            dynamicIntArray.Add(7);
            dynamicIntArray.Add(8);
            dynamicIntArray.Add(9);
            dynamicIntArray.Add(17);

            int a = dynamicIntArray.indexOf(17);
            Console.WriteLine(a);
       }
    }
}
