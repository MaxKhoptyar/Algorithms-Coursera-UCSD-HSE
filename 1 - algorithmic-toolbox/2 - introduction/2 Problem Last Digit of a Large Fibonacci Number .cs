using System;

namespace APlusB
{
    class Program
    {
        static void Main(string[] args)
        {
            var r = Console.ReadLine();
            var result = M(long.Parse(r));
            Console.WriteLine(result);
        }

        static long M(long n)
        {
            if (n <= 1)
            {
                return n;
            }
            else
            {
                int result = 1;
                int p2 = 0;
                int p1 = 1;
                for (int i = 0; i < n - 1; i++)
                {
                    result = (p1 + p2) % 10;
                    p2 = p1;
                    p1 = result;
                }
                return result;
            }
        }
    }
}