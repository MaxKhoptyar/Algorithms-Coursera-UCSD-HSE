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
            var result = LastNumberSumPartFib(arr[0], arr[1]);
            Console.WriteLine(result);
        }
        static long GetOst(int seq, char[] numArr)
        {
            var l = numArr.Length;
            var countDigits = (int)Math.Log10(seq) + 1;

            long result = 0;
            for (int i = 0; i < l; i++)
            {
                var powerTen = l - i - 1;
                var val = (int)Char.GetNumericValue(numArr[i]);

                long res1 = 1;
                res1 = val % seq;
                var c = powerTen / (countDigits);
                for (int j = 0; j < c; j++)
                {
                    res1 = res1 * ((int)Math.Pow(10, countDigits)) % seq;
                }
                var d = powerTen % countDigits;
                if (d != 0)
                {
                    res1 = res1 * (int)Math.Pow(10, d);
                }
                result = (result + res1) % seq;
            }
            return result;
        }
        static long LastNumberSumPartFib(string num1, string num2)
        {
            var numArr1 = num1.ToCharArray();
            var l1 = numArr1.Length;
            var numArr2 = num2.ToCharArray();
            var l2 = numArr2.Length;
            var a = 10;
            var ostArr = new int[6 * a + 2];
            var k = 0;
            int res = 1;
            int p2 = 0;
            int p1 = 1;
            ostArr[0] = 0;
            ostArr[1] = 1;
            for (int i = 2; i < 6 * a + 2; i++)
            {
                res = (p1 + p2) % a;
                ostArr[i] = res;
                p2 = p1;
                p1 = res;
                k = k + 1;
                if (p1 == 1 && p2 == 0)
                {
                    break;
                }
            }

            var seq = k;
            var ost1 = GetOst(k, numArr1);
            var ost2 = GetOst(k, numArr2);
            var seqSum1 = 0;
            for (int i = 0; i < seq - ost1; i++)
            {
                seqSum1 = (seqSum1 + ostArr[ost1 + i]) % 10;
            }

            var seqSum2 = 0;
            for (int i = 0; i < ost2 + 1; i++)
            {
                seqSum2 = (seqSum2 + ostArr[i]) % 10;
            }
            return (seqSum1 + seqSum2)%10;
        }
    }
}