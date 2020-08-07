using System;

namespace Question4
{
    public sealed class DynamicArray<T>
    {
        public int curentSize;
        public int currentCapacity;
        public T[] array;

        public DynamicArray ()
        {
            array = new T[10];
            curentSize = 0;
            currentCapacity = 10;
        }
        public DynamicArray(int x)
        {
            array = new T[x];
            curentSize = 0;
            currentCapacity = x;

        }
        public void Add(T value)
        {
            if (currentCapacity == 0 && curentSize == array.Length)
            {
                Array.Resize(ref array, array.Length + 1);
            }
            else
            {
                currentCapacity--;
            }
            array[curentSize] = value;
            curentSize++;
        }
        public T Get(int index)
        {
            return array[index];
        }

        public int indexOf(T value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (value.Equals(array[i])==true)
                    return i;
                
            }
            return -1;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            DynamicArray<char> dynamicCharArray  = new DynamicArray<char>(5);
            dynamicCharArray.Add('a');
            dynamicCharArray.Add('b');
            dynamicCharArray.Add('c');
            dynamicCharArray.Add('d');
            dynamicCharArray.Add('e');
            dynamicCharArray.Add('f');
            dynamicCharArray.Add('g');
            dynamicCharArray.Add('h');
            dynamicCharArray.Add('i');
            dynamicCharArray.Add('j');
            Console.WriteLine(dynamicCharArray.indexOf('c'));
            Console.WriteLine(dynamicCharArray.Get(7));
            Console.WriteLine("\n\n");


            DynamicArray<float> dynamicFloatArray = new DynamicArray<float>(5);
            dynamicFloatArray.Add(1.1f);
            dynamicFloatArray.Add(2.5f);
            dynamicFloatArray.Add(3.9f);
            dynamicFloatArray.Add(4.3f);
            dynamicFloatArray.Add(5.0f);
            dynamicFloatArray.Add(6.6f);
            dynamicFloatArray.Add(7.2f);
            dynamicFloatArray.Add(8.9f);
            dynamicFloatArray.Add(9.3f);
            dynamicFloatArray.Add(11.2f);
            Console.WriteLine(dynamicFloatArray.indexOf(8.9f));
            Console.WriteLine(dynamicFloatArray.Get(5));
            Console.WriteLine("\n\n");



            DynamicArray<string> dynamicStringArray = new DynamicArray<string>(5);
            dynamicStringArray.Add("Mujtaba");
            dynamicStringArray.Add("Mustafa");
            dynamicStringArray.Add("Raza");
            dynamicStringArray.Add("Tahir");
            dynamicStringArray.Add("Ammar");
            dynamicStringArray.Add("Moazzam");
            dynamicStringArray.Add("Ahmed");
            dynamicStringArray.Add("Mehdi");
            dynamicStringArray.Add("Huzaifa");
            Console.WriteLine(dynamicStringArray.indexOf("Moazzam"));
            Console.WriteLine(dynamicStringArray.Get(4));
            Console.WriteLine("\n\n");




            DynamicArray<int> dynamicIntArray = new DynamicArray<int>(5);
            dynamicIntArray.Add(11);
            dynamicIntArray.Add(22);
            dynamicIntArray.Add(33);
            dynamicIntArray.Add(44);
            dynamicIntArray.Add(55);
            dynamicIntArray.Add(66);
            dynamicIntArray.Add(77);
            dynamicIntArray.Add(88);
            dynamicIntArray.Add(99);
            dynamicIntArray.Add(110);
            Console.WriteLine(dynamicIntArray.indexOf(99));
            Console.WriteLine(dynamicIntArray.Get(5));







        }
    }
}
