using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Question2
{
    public class PartA
    {
       public Tuple<int, int,bool> searchArray(int value, string[] lines)
        {
            int index = 0;
            bool isFound = false;
            foreach (string line in lines)
            {
                if (line.Equals(Convert.ToString(value)))
                {
                    isFound = true;
                    return Tuple.Create(value,index,isFound);
                }
                index++;
            }

            return Tuple.Create(value,-1,isFound);

        }
    }

}
