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
            var input = Console.ReadLine();
            var n = int.Parse(input);

            var res = LastNumberSumFib(r);

            Console.WriteLine(res);            
        }

        static long LastNumberSumFib(int n)
        {
            var a = 10;
            var modArr = new int[6 * a + 2];
            var k = 0;

            int res = 1;
            int p2 = 0;
            int p1 = 1;

            modArr[0] = 0;
            modArr[1] = 1;

            for (int i = 2; i < 6 * a + 2; i++)
            {
                res = (p1 + p2) % a;
                modArr[i] = res;
                p2 = p1;
                p1 = res;
                k = k + 1;
                if (p1 == 1 && p2 == 0)
                {
                    break;
                }
            }

            long mod = n % k;
            long div = n / k;

            var seqSum = 0;
            for (int i = 0; i < k ; i++)
            {
                seqSum = (seqSum + modArr[i]) % 10;
            }

            long lastDigitInCellSumm = seqSum * div;
            for (int i = 0; i < mod + 1; i++)
            {
                lastDigitInCellSumm = (lastDigitInCellSumm + modArr[i]) % 10;
            }
            return lastDigitInCellSumm;
        }
    }
}