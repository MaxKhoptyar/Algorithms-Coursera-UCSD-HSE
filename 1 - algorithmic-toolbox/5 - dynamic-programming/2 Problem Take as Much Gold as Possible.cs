using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ');
            var w = long.Parse(input[0]);
            var n = long.Parse(input[1]);
            var arr = new long[n + 1];
            
            input = Console.ReadLine().Split(' ');
            for (int i = 0; i < n ; i++)
            {
                arr[i + 1] = long.Parse(input[i]);
            }

            var arrAns = new long[n + 1, w + 1];
            for (int i = 0; i < w + 1; i++)
            {
                arrAns[0, i] = 0;
            }
            for (int i = 0; i < n + 1; i++)
            {
                arrAns[i,0 ] = 0;
            }
            for (int k = 1; k < n + 1; k++)
            {
                for (int s = 1; s < w + 1; s++)
                {
                    if (s >= arr[k])
                    {
                        arrAns[k,s] = Math.Max(arrAns[k - 1, s],arrAns[k - 1, s - arr[k]] + arr[k]);
                    }
                    else
                    {
                        arrAns[k, s] = arrAns[k - 1, s];
                    }
                }
            }
            Console.WriteLine(arrAns[n,w]);
        }
    }
}
