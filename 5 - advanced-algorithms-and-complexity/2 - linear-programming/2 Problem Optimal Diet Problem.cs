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
            var epsilon = 0.001;
            var countInequality = int.Parse(arr[0]);
            var countAmounts = int.Parse(arr[1]);
            var countMatrixRows = countInequality + countAmounts + 1;

            var matrix = new double[countMatrixRows][];

            for (int i = 0; i < countInequality; i++)
            {
                input = Console.ReadLine();
                arr = input.Split();
                matrix[i] = new double[countAmounts + 1];
                for (int j = 0; j < countAmounts; j++)
                {
                    matrix[i][j] = double.Parse(arr[j]);
                }
            }

            for (int i = countInequality; i < countInequality + countAmounts; i++)
            {
                matrix[i] = new double[countAmounts + 1];
                matrix[i][i - countInequality] = -1;
                matrix[i][countAmounts] = 0;
            }

            matrix[countInequality + countAmounts] = new double[countAmounts + 1];
            for (int i = 0; i < countAmounts; i++)
            {
                matrix[countInequality + countAmounts][i] = 1;
            }
            matrix[countInequality + countAmounts][countAmounts] = 1000000000;

            input = Console.ReadLine();
            arr = input.Split();
            for (int i = 0; i < countInequality; i++)
            {
                matrix[i][countAmounts] = double.Parse(arr[i]);
            }

            input = Console.ReadLine();
            arr = input.Split();
            var optimizationArr = new double[countAmounts];
            for (int i = 0; i < countAmounts; i++)
            {
                optimizationArr[i] = double.Parse(arr[i]);
            }

            long upper = 1;
            for (int i = 1; i <= countMatrixRows; i++)
            {
                upper *= i;
            }
            long lower = 1;
            for (int i = 1; i <= countAmounts; i++)
            {
                lower *= i;
            }
            long lower2 = 1;
            var c2 = (countMatrixRows - countAmounts);
            for (int i = 1; i <= c2; i++)
            {
                lower2 *= i;
            }
            long setsCount = upper / (lower * lower2);
            var setsArr = new int[setsCount][];

            var optimizeFunctionLocalMax = double.MinValue;
            var optimizeFunctionLocalMaxIndex = -1;

            var statusAnswerArr = new int[setsCount];
            var answerArr = new double[setsCount][];
            var optimizeFunctionValues = new double[setsCount];

            GetSets(countAmounts, countMatrixRows, setsArr);

            for (int i = 0; i < setsCount; i++)
            {
                var currentSet = setsArr[i];
                var newM = new double[countAmounts][];
                for (int j = 0; j < countAmounts; j++)
                {
                    newM[j] = new double[countAmounts + 1];
                    var index = currentSet[j];
                    for (int k = 0; k < countAmounts + 1; k++)
                    {
                        newM[j][k] = matrix[index][k];
                    }
                }

                var r = Gaussian(countAmounts, newM);

                // Проверяем наличие решения
                if (r == null)
                {
                    statusAnswerArr[i] = -1;
                    continue;
                }
                answerArr[i] = r;

                //Проверяем валидность значений для уравнений
                for (int j = 0; j < countMatrixRows; j++)
                {
                    var localAnwser = 0.0;
                    for (int k = 0; k < countAmounts; k++)
                    {
                        localAnwser += (r[k] * matrix[j][k]);
                    }
                    if (localAnwser > matrix[j][countAmounts] + epsilon)
                    {
                        statusAnswerArr[i] = -1;
                        break;
                    }
                }

                if (statusAnswerArr[i] == -1)
                {
                    continue;
                }

                answerArr[i] = r;
                var optimizeFunctionValue = 0.0;
                for (int j = 0; j < countAmounts; j++)
                {
                    optimizeFunctionValue += (r[j] * optimizationArr[j]);
                }
                optimizeFunctionValues[i] = optimizeFunctionValue;

                if (currentSet[countAmounts - 1] == countMatrixRows - 1)
                {
                    statusAnswerArr[i] = 1;
                    continue;
                }
                else
                {
                    statusAnswerArr[i] = 0;
                }

                //Проверяем значение оптимизирующей функции

                if (optimizeFunctionLocalMax < optimizeFunctionValue)
                {
                    optimizeFunctionLocalMax = optimizeFunctionValue;
                    optimizeFunctionLocalMaxIndex = i;
                }
            }

            var boundedSolution = true;
            for (int i = 0; i < setsCount; i++)
            {
                var statusAnswer = statusAnswerArr[i];
                var optimizeFunction = optimizeFunctionValues[i];
                if (statusAnswer == 1)
                {
                    if (optimizeFunctionLocalMax < optimizeFunction && Math.Abs(optimizeFunctionLocalMax - optimizeFunction) > epsilon)
                    {
                        boundedSolution = false;
                    }
                }
            }

            if (boundedSolution)
            {
                if (optimizeFunctionLocalMaxIndex == -1)
                {
                    Console.WriteLine("No solution");
                }
                else
                {
                    var sb = new StringBuilder("Bounded solution\n");
                    for (int i = 0; i < countAmounts; i++)
                    {
                        sb.Append(string.Format("{0:0.000000000000000} ", answerArr[optimizeFunctionLocalMaxIndex][i]));
                    }
                    Console.WriteLine(sb.ToString());
                }
            }
            else
            {
                Console.WriteLine("Infinity");
            }
            Console.ReadLine();
        }

        private static void GetSets(int countAmounts, int countInequality, int[][] setsArr)
        {
            var counter = 0;
            for (int j = 0; j < countInequality - countAmounts + 1; j++)
            {
                var newArr = new int[1];
                newArr[0] = j;
                GetSet(1, newArr, countAmounts, countInequality, ref counter, setsArr);
            }
        }

        private static void GetSet(int currEl, int[] innerArr, int countAmounts, int countInequality, ref int i, int[][] setsArr)
        {
            var innerArrLength = innerArr.Length;
            if (innerArrLength == countAmounts)
            {
                setsArr[i] = innerArr;
                i++;
            }
            else
            {
                for (int j = innerArr[currEl - 1] + 1; j < countInequality; j++)
                {
                    var newArr = new int[innerArrLength + 1];
                    Array.Copy(innerArr, newArr, innerArrLength);
                    newArr[innerArrLength] = j;
                    GetSet(currEl + 1, newArr, countAmounts, countInequality, ref i, setsArr);
                }
            }
        }

        static void SwapEquations(int i, int j, double[][] matrix)
        {
            if (i == j) return;
            for (int k = 0; k < matrix[0].Length; k++)
            {
                var c = matrix[j][k];
                matrix[j][k] = matrix[i][k];
                matrix[i][k] = c;
            }
        }

        static void SwapResult(int i, int j, double[] matrix)
        {
            var c = matrix[j];
            matrix[j] = matrix[i];
            matrix[i] = c;
        }

        static double[] Gaussian(int countInequality, double[][] matrix)
        {
            var result = new double[countInequality];
            for (int i = 0; i < countInequality; i++)
            {
                result[i] = matrix[i][countInequality];
            }
            for (int diag = 0; diag < countInequality; diag++)
            {
                int maxRow = diag;
                double maxVal = Math.Abs(matrix[diag][diag]);
                double d;
                for (int row = diag + 1; row < countInequality; row++)
                {
                    if ((d = Math.Abs(matrix[row][diag])) > maxVal)
                    {
                        maxRow = row;
                        maxVal = d;
                    }
                }

                SwapEquations(diag, maxRow, matrix);
                SwapResult(diag, maxRow, result);


                double invd = matrix[diag][diag];
                if (invd == 0)
                {
                    return null;
                }
                for (int col = diag; col < countInequality; col++)
                {
                    matrix[diag][col] /= invd;
                }
                result[diag] /= invd;

                for (int row = 0; row < countInequality; row++)
                {
                    d = matrix[row][diag];
                    if (row != diag)
                    {
                        for (int col = diag; col < countInequality; col++)
                        {
                            matrix[row][col] -= d * matrix[diag][col];
                        }

                        result[row] -= d * result[diag];
                    }
                }
            }

            for (int i = 0; i < countInequality; i++)
            {
                var counterHorizontal = 0;
                for (int j = 0; j < countInequality; j++)
                {
                    if (matrix[i][j] == 0 || matrix[i][j] == double.NaN)
                    {
                        counterHorizontal++;
                    }
                }

                var counterVertical = 0;
                for (int j = 0; j < countInequality; j++)
                {
                    if (matrix[j][i] == 0 || matrix[j][i] == double.NaN)
                    {
                        counterVertical++;
                    }
                }
                if (counterHorizontal == countInequality || counterVertical == countInequality)
                {
                    return null;
                }
            }
            return result;
        }
    }
}