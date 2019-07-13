using System;
using System.Collections.Generic;
using System.Linq;


namespace ConsoleApplication15
{


    class Program
    {
        class CountSorted
        {
            private int SizeStorage;
            private int[] Storage;
            private int CountElements = 0;
            public Dictionary<char, int> ChainsStart = new Dictionary<char, int>();
            public int[,] CountMatrix;
            public char[] SortedArr;
            public Dictionary<char, int> CorrespondingSymbols = new Dictionary<char, int>();
            public Dictionary<int, char> CorrespondingInts = new Dictionary<int, char>();
            public CountSorted()
            {
                CorrespondingSymbols.Add('$', 0);
                CorrespondingSymbols.Add('A', 1);
                CorrespondingSymbols.Add('C', 2);
                CorrespondingSymbols.Add('G', 3);
                CorrespondingSymbols.Add('T', 4);

                CorrespondingInts.Add(0, '$');
                CorrespondingInts.Add(1, 'A');
                CorrespondingInts.Add(2, 'C');
                CorrespondingInts.Add(3, 'G');
                CorrespondingInts.Add(4, 'T');

                SizeStorage = CorrespondingSymbols.Count;
                Storage = new int[SizeStorage];
                for (int i = 0; i < SizeStorage; i++)
                {
                    Storage[i] = 0;
                }
            }
            public void Create(char[] source)
            {
                var sourceLength = source.Length;
                CountMatrix = new int[SizeStorage, sourceLength + 1];
                for (int i = 0; i < SizeStorage; i++)
                {
                    CountMatrix[i, 0] = 0;
                }

                for (int i = 0; i < sourceLength; i++)
                {
                    var x = source[i];
                    var v = CorrespondingSymbols[x];
                    Storage[v] += 1;

                    CountMatrix[v, i + 1] = CountMatrix[v, i + 1 - 1] + 1;

                    for (int j = 0; j < v; j++)
                    {
                        CountMatrix[j, i + 1] = CountMatrix[j, i + 1 - 1];
                    }
                    for (int j = v + 1; j < SizeStorage; j++)
                    {
                        CountMatrix[j, i + 1] = CountMatrix[j, i + 1 - 1];
                    }
                }
            }

            public void GetArray()
            {
                var car = 0;
                for (int i = 0; i < SizeStorage; i++)
                {
                    if (Storage[i] > 0)
                    {
                        var c = CorrespondingInts[i];
                        ChainsStart.Add(c, car);
                        car += Storage[i];
                    }
                }
            }
        }

        static void Main(string[] args)
        {

            var input = Console.ReadLine();
            var inputArr = input.ToCharArray();
            var inputArrLentg = inputArr.Length;
            var s = new CountSorted();
            s.Create(inputArr);
            s.GetArray();
            var dictStart = s.ChainsStart;
            var matrix = s.CountMatrix;
            var dictCorr = s.CorrespondingSymbols;
            input = Console.ReadLine();
            var countPatterns = int.Parse(input);
            var arrayPatterns = Console.ReadLine().Split();
            for (int i = 0; i < countPatterns; i++)
            {
                var count = PatternCounts(inputArr, arrayPatterns[i], dictStart, matrix, dictCorr);
                Console.Write(string.Format("{0} ", count));
            }
            Console.WriteLine();
            Console.ReadLine();
        }

        static int PatternCounts(char[] real, string pattern, Dictionary<char, int> chainStart, int[,] matrix, Dictionary<char, int> correspondingDict)
        {
            var top = 0;
            var bottom = real.Length - 1;

            var patternArray = pattern.ToCharArray();
            var patternArrayLen = patternArray.Length;

            var car = patternArrayLen - 1;

            while (top <= bottom)
            {
                var symbol = patternArray[car];

                if (chainStart.ContainsKey(symbol))
                {
                    var symbolInt = correspondingDict[symbol];
                    var s1 = top;
                    var s2 = bottom;
                    if (top > 0)
                    {
                        s1 -= 1;
                    }
                    if (bottom < real.Length)
                    {
                        s2 += 1;
                    }
                    var c = matrix[symbolInt, s2] - matrix[symbolInt, s1];
                    var d = c == 0;

                    if (d)
                    {
                        return 0;
                    }
                    else
                    {
                        top = chainStart[symbol] + matrix[symbolInt, top];
                        bottom = chainStart[symbol] + matrix[symbolInt, bottom + 1] - 1;
                    }
                }
                else
                {
                    return 0;
                }

                car--;
                if (car < 0)
                {
                    break;
                }
            }
            return bottom - top + 1;
        }
    }
}