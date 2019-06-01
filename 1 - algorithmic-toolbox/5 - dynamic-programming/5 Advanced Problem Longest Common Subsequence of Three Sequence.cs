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
            var aN = int.Parse(input);
            var arrA = new int[aN];
            input = Console.ReadLine();
            var arrInput = input.Split();
            for (int i = 0; i < aN; i++)
            {
                arrA[i] = int.Parse(arrInput[i]);
            }

            input = Console.ReadLine();
            var bN = int.Parse(input);
            var arrB = new int[bN];
            input = Console.ReadLine();
            arrInput = input.Split();
            for (int i = 0; i < bN; i++)
            {
                arrB[i] = int.Parse(arrInput[i]);
            }

            input = Console.ReadLine();
            var cN = int.Parse(input);
            var arrC = new int[cN];
            input = Console.ReadLine();
            arrInput = input.Split();
            for (int i = 0; i < cN; i++)
            {
                arrC[i] = int.Parse(arrInput[i]);
            }

            var r = new int[aN + 1, bN + 1, cN + 1];

            for (int x = 1; x < aN + 1; x++)
            {
                for (int y = 1; y < bN + 1; y++)
                {
                    for (int z = 1; z < cN + 1; z++)
                    {
                        if (arrA[x - 1] == arrB[y - 1] && arrB[y - 1] == arrC[z - 1])
                        {
                            r[x, y, z] = r[x - 1, y - 1, z - 1] + 1;
                        }
                        else
                        {
                            r[x, y, z] = Math.Max(r[x - 1, y, z], Math.Max(r[x, y - 1, z], r[x, y, z - 1]));
                        }
                    }
                }
            }
            Console.WriteLine(r[aN, bN, cN]);
        }
    }
}
