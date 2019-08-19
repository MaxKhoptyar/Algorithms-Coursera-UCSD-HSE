using System;
using System.Text;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var countEquations = int.Parse(input);

            var matrix = new double[countEquations][];

            for (int i = 0; i < countEquations; i++)
            {
                input = Console.ReadLine();
                var arr = input.Split();
                matrix[i] = new double[countEquations + 1];
                for (int j = 0; j < countEquations + 1; j++)
                {
                    matrix[i][j] = double.Parse(arr[j]);
                }
            }

            for (int i = 0; i < countEquations; i++)
            {
                var maxEl = Math.Abs(matrix[i][i]);
                var maxRow = i;
                for (int j = i; j < countEquations; j++)
                {
                    if (Math.Abs(matrix[j][i]) > maxEl)
                    {
                        maxRow = j;
                        maxEl = matrix[j][i];
                    }
                }

                Swap(i, maxRow, matrix);

                for (int j = i + 1; j < countEquations; j++)
                {
                    var c = (-matrix[j][i]) / matrix[i][i];
                    for (int k = i; k < countEquations + 1; k++)
                    {
                        if (i == k)
                        {
                            matrix[j][k] = 0;
                        }
                        else
                        {
                            matrix[j][k] += c * matrix[i][k];
                        }
                    }
                }
            }

            var r = new double[countEquations];
            for (int i = countEquations - 1; i >= 0; i--)
            {
                r[i] = matrix[i][countEquations] / matrix[i][i];
                for (int j = i - 1; j >= 0; j--)
                {
                    matrix[j][countEquations] -= matrix[j][i] * r[i];
                }
            }

            var sb = new StringBuilder();
            for (int i = 0; i < countEquations; i++)
            {
                sb.Append(string.Format("{0:0.000000} ", r[i]));
            }

            Console.WriteLine(sb);
            Console.ReadLine();
        }

        static void Swap(int i, int j, double[][] matrix)
        {
            for (int k = 0; k < matrix[0].Length; k++)
            {
                var c = matrix[j][k];
                matrix[j][k] = matrix[i][k];
                matrix[i][k] = c;
            }
        }
    }
}