using System;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Algoritm
{
    class Program
    {
        static void Main(string[] args)
        {
            var r = Console.ReadLine();
            var arr = r.Split();
            var a = int.Parse(arr[0]);
            var b = int.Parse(arr[1]);
            
            var result = GCD(a,b);

            Console.WriteLine(result);
        }

        static int GCD(int a, int b)
        {
            while (a != 0 & b != 0)
            {
                if (a > b)
                {
                    a = a % b;
                }
                else
                {
                    b = b % a;
                }
            }
            if (a == 0)
            {
                return b;
            }
            return a;
        }
    }
}