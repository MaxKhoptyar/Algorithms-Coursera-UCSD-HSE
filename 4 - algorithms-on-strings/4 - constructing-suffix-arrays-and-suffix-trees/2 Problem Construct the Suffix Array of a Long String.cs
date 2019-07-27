using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication15
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var length = input.Length;

            var order = SortCharacters(input);
            var classes = ComputeCharClasses(input, order);
            var l = 1;

            while (l < length)
            {
                order = SortDoubled(input, l, order, classes);
                classes = UpdateClasses(order, classes, l);
                l = l * 2;
            }

            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(string.Format("{0} ", order[i]));
            }
            Console.WriteLine(sb);
            //Console.ReadLine();
        }

        static int[] SortDoubled(string s, int l, int[] order, int[] classes)
        {
            var sL = s.Length;
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

        static int[] SortCharacters(string s)
        {
            var charArr = s.ToCharArray();
            var length = s.Length;

            var order = new int[length];
            var count = new int[100];
            for (int i = 0; i < length; i++)
            {
                count[charArr[i]] = count[charArr[i]] + 1;
            }

            for (int i = 1; i < 100; i++)
            {
                count[i] = count[i] + count[i - 1];
            }

            for (int i = length - 1; i > -1; i--)
            {
                var c = charArr[i];
                count[c] = count[c] - 1;
                order[count[c]] = i;
            }
            return order;
        }

        static int[] ComputeCharClasses(string s, int[] order)
        {
            var sArr = s.ToCharArray();
            var sL = s.Length;
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
    }
}