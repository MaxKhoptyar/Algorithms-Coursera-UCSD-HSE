using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            input = input + "$";

            var sL = input.Length;
            var charArr = input.ToCharArray();
            var order = SortCharacters(charArr);
            var classes = ComputeCharClasses(charArr, order);
            var l = 1;

            while (l < sL)
            {
                order = SortDoubled(sL, l, order, classes);
                classes = UpdateClasses(order, classes, l);
                l = l * 2;
            }

            var orderWithoutDollar = new int[charArr.Length - 1];
            for (int i = 0; i < charArr.Length - 1; i++)
            {
                orderWithoutDollar[i] = order[i + 1];
            }

            charArr = input.ToCharArray();
            input = Console.ReadLine();
            var patternCounts = int.Parse(input);

            input = Console.ReadLine();
            var patterns = input.Split();
            var indexes = new bool[orderWithoutDollar.Length];

            var uniquePatterns = GetUniquePatterns(patterns);
            for (int i = 0; i < uniquePatterns.Length; i++)
            {
                PatternCounts(orderWithoutDollar, charArr, uniquePatterns[i].ToCharArray(), indexes);
            }

            var sb = new StringBuilder();
            for (int i = 0; i < orderWithoutDollar.Length; i++)
            {
                if (indexes[i])
                {
                    sb.Append(string.Format("{0} ", i));
                }
            }
            Console.WriteLine(sb);
        }

        private static string[] GetUniquePatterns(string[] patterns)
        {
            var patternsL = patterns.Length;
            var setString = new HashSet<string>();
            for (int i = 0; i < patternsL; i++)
            {
                setString.Add(patterns[i]);
            }
            return setString.ToArray();
        }

        static int[] SortDoubled(int sL, int l, int[] order, int[] classes)
        {
            var count = new int[sL];
            var newOrder = new int[sL];
            for (int i = 0; i < sL; i++)
            {
                count[classes[i]] = count[classes[i]] + 1;
            }
            for (int i = 1; i < sL; i++)
            {
                count[i] = count[i] + count[i - 1];
            }
            for (int i = sL - 1; i > -1; i--)
            {
                var start = (order[i] - l + sL) % sL;
                var cl = classes[start];
                count[cl] = count[cl] - 1;
                newOrder[count[cl]] = start;
            }
            return newOrder;
        }

        static int[] UpdateClasses(int[] newOrder, int[] classes, int l)
        {
            var n = newOrder.Length;
            var newClasses = new int[n];
            newClasses[newOrder[0]] = 0;

            for (int i = 1; i < n; i++)
            {
                var cur = newOrder[i];
                var prev = newOrder[i - 1];
                var mid = cur + l;
                var midPrev = (prev + l) % n;

                if (classes[cur] != classes[prev] || classes[mid] != classes[midPrev])
                {
                    newClasses[cur] = newClasses[prev] + 1;
                }
                else
                {
                    newClasses[cur] = newClasses[prev];
                }
            }
            return newClasses;
        }

        static int[] SortCharacters(char[] sArr)
        {
            var sL = sArr.Length;
            var order = new int[sL];
            var count = new int[100];
            for (int i = 0; i < sL; i++)
            {
                count[sArr[i]] = count[sArr[i]] + 1;
            }
            for (int i = 1; i < 100; i++)
            {
                count[i] = count[i] + count[i - 1];
            }

            for (int i = sL - 1; i > -1; i--)
            {
                var c = sArr[i];
                count[c] = count[c] - 1;
                order[count[c]] = i;
            }
            return order;
        }

        static int[] ComputeCharClasses(char[] sArr, int[] order)
        {
            var sL = sArr.Length;
            var classes = new int[sL];
            classes[order[0]] = 0;

            for (int i = 1; i < sL; i++)
            {
                if (sArr[order[i]] != sArr[order[i - 1]])
                {
                    classes[order[i]] = classes[order[i - 1]] + 1;
                }
                else
                {
                    classes[order[i]] = classes[order[i - 1]];
                }
            }
            return classes;
        }

        static void PatternCounts(int[] order, char[] baseString, char[] patternArray, bool[] indexes)
        {
            var minIndex = 0;
            var maxIndex = order.Length - 1;

            while (minIndex < maxIndex)
            {
                var midIndex = (maxIndex + minIndex) / 2;
                var compareResult = Compare(baseString, order[midIndex], patternArray);
                if (compareResult.Result == 1)
                {
                    minIndex = midIndex + 1;
                }
                else
                {
                    maxIndex = midIndex;
                }
            }

            var start = minIndex;
            maxIndex = order.Length - 1;

            while (maxIndex - minIndex > 1)
            {
                var midIndex = (maxIndex + minIndex) / 2;
                var compareResult = Compare(baseString, order[midIndex], patternArray);
                if (compareResult.Contains)
                {
                    minIndex = midIndex;
                }
                else
                {
                    maxIndex = midIndex - 1;
                }
            }

            var end = maxIndex;
            var result = Compare(baseString, order[end], patternArray);
            if (!result.Contains)
            {
                result = Compare(baseString, order[minIndex], patternArray);
                if (!result.Contains)
                {
                    return;
                }
                else
                {
                    end = minIndex;
                }
            }

            var l = end - start + 1;
            for (int i = 0; i < l; i++)
            {
                indexes[order[start + i]] = true;
            }
        }

        struct CompareResult
        {
            public bool Contains;
            public int Result;
        }

        private static CompareResult Compare(char[] baseString, int startIndex, char[] pattern)
        {
            var patternL = pattern.Length;
            var length = baseString.Length - startIndex;

            var l = Math.Min(patternL, length);
            for (int i = 0; i < l; i++)
            {
                if (pattern[i] == baseString[startIndex + i])
                {
                    continue;
                }
                if (pattern[i] > baseString[startIndex + i])
                {
                    return new CompareResult() { Contains = false, Result = 1 };
                }
                else
                {
                    return new CompareResult() { Contains = false, Result = -1 };
                }
            }

            if (patternL == length)
            {
                return new CompareResult() { Contains = true, Result = 0 };
            }
            if (patternL > length)
            {
                return new CompareResult() { Contains = true, Result = 1 };
            }
            else
            {
                return new CompareResult() { Contains = true, Result = -1 };
            }
        }
    }
}