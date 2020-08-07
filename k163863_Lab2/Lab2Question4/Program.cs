using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Question4
{
    class Program
    {
        static void Main(string[] args)
        {
            string dllFileLocation= ConfigurationManager.AppSettings["dllPath"];
            Assembly assembly = Assembly.LoadFile(dllFileLocation);
                
                foreach(System.Type t in assembly.ExportedTypes)
                {
                    foreach(System.Reflection.MethodInfo method in t.GetMethods())
                    {
                        Console.Write("Method: "+method.Name);
                        bool isParameterized = method.GetParameters().Length > 0 ? true : false;
                        if (isParameterized)
                        {
                            Console.Write("(");
                            foreach (System.Reflection.ParameterInfo param in method.GetParameters())
                                    Console.Write(param.ParameterType + ", ");
                            Console.Write(")");
                        }
                        else if(!isParameterized)
                            Console.Write("(void)  |  Parameter less");

                    Console.WriteLine("\n-----");
                    }
                }
            Console.ReadKey();
            }
    }
}
