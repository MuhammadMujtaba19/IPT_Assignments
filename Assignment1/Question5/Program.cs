using System;
using System.Collections;
using System.Collections.Generic;


namespace Question5
{
    class DynamicArray<T> : IList<T>
    {
        public int curentSize;
        public int currentCapacity;
        public T[] array;

        public DynamicArray()
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

        public T this[int index] { get => ((IList<T>)array)[index]; set => ((IList<T>)array)[index] = value; }

        public int Count => ((IList<T>)array).Count;

        public bool IsReadOnly => ((IList<T>)array).IsReadOnly;

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

        public void Clear()
        {
            ((IList<T>)array).Clear();
        }

        public bool Contains(T item)
        {
            return ((IList<T>)array).Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((IList<T>)this.array).CopyTo(array, arrayIndex);
        }

        public T Get(int index)
        {
            return array[index];
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IList<T>)array).GetEnumerator();
        }

        public int IndexOf(T value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Equals(value))
                    return i;

            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (array.Length < (index+1))
            {
                Array.Resize(ref array,index+1);
                array[index] = item;

            }
            else
            {
                array[index] = item;
            }
        }


        public bool Remove(T item)
        {
            for(int i = 0; i < array.Length; i++)
            {
                if (item.Equals(array[i]) == true)
                    return true;
                
            }return false;
        }

        public void RemoveAt(int index)
        {
            ((IList<T>)array).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<T>)array).GetEnumerator();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

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
            Console.WriteLine(dynamicIntArray.IndexOf(99));
            Console.WriteLine(dynamicIntArray.Get(5));

            int []arr= new int[dynamicIntArray.Count];

            dynamicIntArray.CopyTo(arr, 0);
            foreach (int i in arr)
            {
                Console.WriteLine(i);
            }


            Console.WriteLine(dynamicIntArray.Contains(11));
            dynamicIntArray.Insert(10,120);
            dynamicIntArray.Insert( 11,130);

            dynamicIntArray.Remove(55);
            

            


        }
    }
}
