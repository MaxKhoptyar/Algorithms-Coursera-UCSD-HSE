using System;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var countColors = 3;

            var input = Console.ReadLine();
            var arr = input.Split();

            var countEdge = int.Parse(arr[1]);
            var countVerticies = int.Parse(arr[0]);

            var matrix = new bool[countVerticies][];
            var matrixColors = new bool[countVerticies][];
            var Fe = new int[countVerticies * 4][];
            var counter = 1;

            var sb = new StringBuilder();
            for (int i = 0; i < countVerticies; i++)
            {
                var bi = 4 * i;

                matrix[i] = new bool[countVerticies];
                matrixColors[i] = new bool[countColors];
                Fe[bi] = new int[countColors];

                for (int j = 0; j < countColors; j++)
                {
                    Fe[bi][j] = counter++;
                }

                Fe[bi + 1] = new int[] { -Fe[bi][0], -Fe[bi][1] };
                Fe[bi + 2] = new int[] { -Fe[bi][1], -Fe[bi][2] };
                Fe[bi + 3] = new int[] { -Fe[bi][0], -Fe[bi][2] };
                sb.Append(string.Format("{0} {1} {2} 0\n{3} {4} 0\n{5} {6} 0\n{7} {8} 0\n", Fe[bi][0], Fe[bi][1], Fe[bi][2],
                    -Fe[bi][0], -Fe[bi][1], -Fe[bi][1], -Fe[bi][2], -Fe[bi][0], -Fe[bi][2]));
            }

            var Fv = new int[countEdge * countColors][];
            for (int i = 0; i < countEdge; i++)
            {
                input = Console.ReadLine();
                arr = input.Split();

                var x1 = int.Parse(arr[0]) - 1;
                var x2 = int.Parse(arr[1]) - 1;

                matrix[x1][x2] = true;
                matrix[x2][x1] = true;

                var bi = i * countColors;
                var bi_x1 = x1 * countColors;
                var bi_x2 = x2 * countColors;

                for (int j = 0; j < countColors; j++)
                {
                    Fv[bi + j] = new int[countColors];
                    Fv[bi + j][0] = -(bi_x1 + 1 + j);
                    Fv[bi + j][1] = -(bi_x2 + 1 + j);
                    sb.Append(string.Format("{0} {1} 0\n", Fv[bi + j][0], Fv[bi + j][1]));
                }
            }

            Console.WriteLine(String.Format("{0} {1}", Fv.Length + Fe.Length, counter));
            Console.WriteLine(sb);
        }
    }
}