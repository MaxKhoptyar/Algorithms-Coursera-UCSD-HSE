using System;

namespace APlusB
{
    class Program
    {
        static void Main(string[] args)
        {
            var r = Console.ReadLine();
            var result = Fibonachi(int.Parse(r));
            Console.WriteLine(result);
        }

        static int Fibonachi(int n)
        {
            if (n <= 1)
            {
                return n;
            }
            else
            {
                var result = 0;
                var prev1 = 1;
                var prev2 = 0;
                for (int i = 0; i < n - 1; i++)
                {
                    result = prev2 + prev1;
                    prev2 = prev1;
                    prev1 = result;
                }
                return result;
            }
        }
    }
}