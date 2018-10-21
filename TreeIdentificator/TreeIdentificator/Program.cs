using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TreeIdentificator
{
    class Program
    {
      
        static void Main(string[] args)
        {
            string[] values = new string[9];
            values[0] = "int a";
            values[1] = "float b";
            values[2] = "string c";
            values[3] = "const float Const1 3,14";
            values[4] = "string method1(int x1, ref char x2, out float x3)";
            values[5] = "class MyClass";
            values[6] = "bool iy";
            values[7] = "int method3()";
            values[8] = "int proverka true";

            var tree = new BinarTree();          
            tree = Parser.Parse(values);

            Console.ReadLine();
        }
    }
}
