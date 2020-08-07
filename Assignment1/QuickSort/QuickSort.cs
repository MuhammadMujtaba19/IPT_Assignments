using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSort
{
    //Algorithm for QuickSort taken from GeeksforGeeks
    public class quickSort
    {
        public unsafe void Sort(int* array,int start,int end)
        {
            if (start< end)
            {
                int pi = partition(array,start, end);

                Sort(array,start, pi - 1);  
                Sort(array,pi + 1, end); 
            }

        }
        public unsafe int partition(int* array,int start,int end)
        {
            int temp = 0;
            int pivot = *(array + end);
            int i = start - 1;
            for (int j = start; j < end; j++)
            {
                if (*(array + j ) < pivot)
                {
                    i++; 
                     temp = *(array + i);
                    *(array + i) = *(array + j);
                    *(array + j) = temp;
                }
            }
            temp = *(array + i+1);
            *(array + i + 1) = *(array + end);
            *(array + end)  = temp;

            return i + 1;
        }
    }
}

