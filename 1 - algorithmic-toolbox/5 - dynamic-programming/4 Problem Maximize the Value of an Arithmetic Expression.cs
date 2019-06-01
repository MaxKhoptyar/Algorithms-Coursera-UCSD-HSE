using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingToolbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var arrInput = input.ToCharArray();
            var l = arrInput.Length;
            var digL = new List<long>();
            var opL = new List<string>();
            var isDig = false;
            var buff = "";
            for (int i = 0; i < l; i++)
            {
                if (Char.IsDigit(arrInput[i]))
                {
                    buff += arrInput[i];
                }
                else
                {
                    digL.Add(long.Parse(buff));
                    buff = "";
                    opL.Add(arrInput[i].ToString());
                }
            }
            digL.Add(long.Parse(buff));
            var arrDig = digL.ToArray();
            var arrOp = opL.ToArray();
            var n = arrDig.Length;
            var arrMin = new long[n, n];
            var arrMax = new long[n, n];
            for (int i = 0; i < n; i++)
            {
                arrMin[i, i] = arrDig[i];
                arrMax[i, i] = arrDig[i];
            }

            for (int s = 0; s < n; s++)
            {
                for (int i = 0; i < n - s - 1; i++)
                {
                    var j = i + s + 1;
                    var a = MinMax(i, j, arrMin, arrMax, arrOp);
                    arrMin[i,j] = a[0];
                    arrMax[i,j] = a[1];
                }
            }
            Console.WriteLine(arrMax[0,n - 1]);
            Console.ReadLine();
        }

        static long[] MinMax(int i,int j,long[,] arrMin,long[,] arrMax,string[] arrOp)
        {
            var min = long.MaxValue;
            var max = long.MinValue;

            for (int k = i; k < j; k++)
            {
                var a = Operand(arrMax[i, k], arrMax[k + 1, j], arrOp[k]);
                var b = Operand(arrMax[i, k], arrMin[k + 1, j], arrOp[k]);

                var c = Operand(arrMin[i, k], arrMax[k + 1, j], arrOp[k]);
                var d = Operand(arrMin[i, k], arrMin[k + 1, j], arrOp[k]);

                min = Math.Min(min, Math.Min(Math.Min(a, b), Math.Min(c, d)));
                max = Math.Max(max, Math.Max(Math.Max(a, b), Math.Max(c, d)));
            }
            var retArr = new long[2];
            retArr[0] = min;
            retArr[1] = max;
            return retArr;
        }

        static long Operand(long a, long b, string op)
        {
            if (op.Equals("+"))
            {
                return a + b;
            }
            else if (op.Equals("-"))
            {
                return a - b;
            }
            else
            {
                return a*b;
            }
        }
    }
}
