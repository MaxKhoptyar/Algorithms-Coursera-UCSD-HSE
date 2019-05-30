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
            var m = int.Parse(arr[1]);

            var result = HugeFibNum(arr[0], m);

            Console.WriteLine(result);
        }

        static long HugeFibNum(string num, int m)
        {
            var numArr = num.ToCharArray();
            var l = numArr.Length;
            var modArr = new int[6 * m];
            var k = 0;

            int res = 1;
            int p2 = 0;
            int p1 = 1;

            modArr[0] = 0;
            modArr[1] = 1;

            for (int i = 2; i < 6 * m; i++)
            {
                res = (p1 + p2) % m;
                modArr[i] = res;
                p2 = p1;
                p1 = res;
                k = k + 1;
                if (p1 == 1 && p2 == 0)
                {
                    break;
                }
            }

            var seq = k;
            var countDigits = (int)Math.Log10(seq) + 1;

            long res = 0;
            for (int i = 0; i < l; i++)
            {
                var powerTen = l - i - 1;
                var val = (int)Char.GetNumericValue(numArr[i]);

                long l = 1;
                l = val % seq;
                var c = powerTen / (countDigits);
                for (int j = 0; j < c; j++)
                {
                    l = l * ((int)Math.Pow(10, countDigits)) % seq;
                }
                var d = powerTen % countDigits;
                if (d != 0)
                {
                    l = l * (int)Math.Pow(10,d);
                }
                res = res + l;
            }

            var n = res % seq;
            return modArr[n];
        }
    }
}