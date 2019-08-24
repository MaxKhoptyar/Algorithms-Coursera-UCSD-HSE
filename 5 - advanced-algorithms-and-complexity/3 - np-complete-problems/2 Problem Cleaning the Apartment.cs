using System;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var arr = input.Split();

            var countEdge = int.Parse(arr[1]);
            var countVerticies = int.Parse(arr[0]);

            var Fv_m = new int[countVerticies][];
            var permutations = GetPermutation(countVerticies);
            var counter = 1;
            for (int i = 0; i < countVerticies; i++)
            {
                Fv_m[i] = new int[countVerticies];
                for (int j = 0; j < countVerticies; j++)
                {
                    Fv_m[i][j] = counter++;
                }
            }

            var block = permutations.Length + 1;
            var twoBlock = block * 2;
            var Fv_al = new int[twoBlock * countVerticies][];
            for (int i = 0; i < countVerticies; i++)
            {
                var bi = i * twoBlock;
                for (int j = 0; j < permutations.Length; j++)
                {
                    Fv_al[bi + j] = new int[] { -Fv_m[i][permutations[j][0]], -Fv_m[i][permutations[j][1]] };
                }

                Fv_al[bi + permutations.Length] = new int[countVerticies];
                for (int j = 0; j < countVerticies; j++)
                {
                    Fv_al[bi + permutations.Length][j] = Fv_m[i][j];
                }

                var bi2 = bi + block;
                for (int j = 0; j < permutations.Length; j++)
                {
                    Fv_al[bi2 + j] = new int[] { -Fv_m[permutations[j][0]][i], -Fv_m[permutations[j][1]][i] };
                }

                Fv_al[bi2 + permutations.Length] = new int[countVerticies];
                for (int j = 0; j < countVerticies; j++)
                {
                    Fv_al[bi2 + permutations.Length][j] = Fv_m[j][i];
                }
            }

            var adjMatrix = new bool[countVerticies, countVerticies];
            for (int i = 0; i < countEdge; i++)
            {
                input = Console.ReadLine();
                arr = input.Split();

                var x1 = int.Parse(arr[0]) - 1;
                var x2 = int.Parse(arr[1]) - 1;

                adjMatrix[x1, x2] = true;
                adjMatrix[x2, x1] = true;
            }

            var notAdjCount = 0;
            for (int i = 0; i < countVerticies; i++)
            {
                for (int j = i + 1; j < countVerticies; j++)
                {
                    if (!adjMatrix[i, j])
                    {
                        for (int k = 0; k < countVerticies - 1; k++)
                        {
                            notAdjCount++;
                        }
                    }
                }
            }

            var Fe = new int[notAdjCount * 2][];
            var counterL = 0;
            for (int i = 0; i < countVerticies; i++)
            {
                for (int j = i + 1; j < countVerticies; j++)
                {
                    if (!adjMatrix[i, j])
                    {
                        for (int k = 0; k < countVerticies - 1; k++)
                        {
                            Fe[counterL++] = new[] { -Fv_m[k][i], -Fv_m[k + 1][j] };
                            Fe[counterL++] = new[] { -Fv_m[k + 1][i], -Fv_m[k][j] };
                        }
                    }
                }
            }

            var sb = new StringBuilder();
            sb.Append(string.Format("{0} {1}\n", Fv_al.Length + Fe.Length, counter - 1));
            for (int i = 0; i < Fv_al.Length; i++)
            {
                for (int j = 0; j < Fv_al[i].Length; j++)
                {
                    sb.Append(string.Format("{0} ", Fv_al[i][j]));
                }
                sb.Append("0\n");
            }
            for (int i = 0; i < Fe.Length; i++)
            {
                for (int j = 0; j < Fe[i].Length; j++)
                {
                    sb.Append(string.Format("{0} ", Fe[i][j]));
                }
                sb.Append("0\n");
            }

            Console.WriteLine(sb);
        }

        private static int[][] GetPermutation(int countVerticies)
        {
            var count = countVerticies * (countVerticies - 1) / 2;
            var res = new int[count][];

            var b = 0;
            for (int j = 0; j < countVerticies; j++)
            {
                for (int k = j + 1; k < countVerticies; k++)
                {
                    res[b + k - j - 1] = new int[] { j, k };
                }
                b += countVerticies - j - 1;
            }
            return res;
        }

    }
}