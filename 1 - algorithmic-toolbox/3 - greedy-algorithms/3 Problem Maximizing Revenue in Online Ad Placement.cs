using System;

namespace Algoritm2
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var n = int.Parse(input);

            var seqA = new int[n];
            var seqB = new int[n];

            var inputA = Console.ReadLine();
            var inputB = Console.ReadLine();

            var inputAArr = inputA.Split();
            var inputBArr = inputB.Split();

            for (int i = 0; i < n; i++)
            {
                seqA[i] = int.Parse(inputAArr[i]);
                seqB[i] = int.Parse(inputBArr[i]);
            }

            Array.Sort(seqA);
            Array.Sort(seqB);

            Console.WriteLine(Sum(n, seqA, seqB));
            Console.ReadLine();
        }

        static long Sum(int n, int[] seqA, int[] seqB)
        {
            int s = 0;
            for (int i = 0; i < n; i++)
            {
                s = s + (seqB[i] * seqA[i]);
            }
            return s;
        }
    }
}
