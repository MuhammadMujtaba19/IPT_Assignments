using System;
using System.IO;
using System.Collections.Generic;

namespace Question1
{
    class Program
    {
        // Since the numnber of zones are finite and known (24) we can use Array data structure here as it would be easy to traverse and consumes less memory 
        // we could easily index any zone number using array 
        // for e.g we need to see the status of zone6 we can simply use array by writing zones[6] and this would return 0/1 based on the input within the file

        static void Main(string[] args)
        {
            int i = 1;
            string textFile= Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Q1Input.txt");
            int[] zones = new int[25];

            if (File.Exists(textFile))
            {
                string[] lines = File.ReadAllLines(textFile);
                foreach (string line in lines) 
                { 
                    //Console.WriteLine(line);
                    string temp=line.Split(",")[1];
                    zones[i]=(Convert.ToInt32(temp));
                }
            }
            Console.WriteLine(textFile);
        }
        
    }
    }


