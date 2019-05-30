using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algoritm2
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var k = int.Parse(input);

            var sum = k;
            var listAns = new List<int>();
            for (int i = 1; i < k + 1; i++)
            {
                if (sum > (2 * i) || sum == i)
                {
                    listAns.Add(i);
                    sum -= i;
                }

                if (sum == 0)
                {
                    break;
                }
            }

            var sb = new StringBuilder();
            sb.Append(string.Format("{0}\n", listAns.Count));
            foreach (var e in listAns)
            {
                sb.Append(string.Format("{0} ", e));
            }
            Console.WriteLine(sb);
        }
    }
}
