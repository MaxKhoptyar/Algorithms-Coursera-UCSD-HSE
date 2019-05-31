using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingToolbox
{
    class Program
    {
        private static long count;
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var n = int.Parse(input);

            input = Console.ReadLine();
            var inputArr = input.Split();
            var arr = new int[n];

            for (int i = 0; i < n; i++)
            {
                arr[i] = int.Parse(inputArr[i]);
            }
            arr = MergeSortCountInvertions(0, n - 1, arr);
            Console.WriteLine(count);
        }

        static int[] MergeSortCountInvertions(int start, int end, int[] arr)
        {
            int[] ret;

            if (end - start == 0)
            {
                ret = new int[1];
                ret[0] = arr[start];
                return ret;
            }

            var n = (end - start);
            var c = n / 2;

            var arr1 = MergeSortCountInvertions(start, start + c, arr);
            var arr2 = MergeSortCountInvertions(start + c + 1, end, arr);

            var k1 = 0;
            var k2 = 0;
            var k = arr1.Length + arr2.Length;
            var arr3 = new int[k];

            for (int i = 0; i < k; i++)
            {
                if (k1 == arr1.Length)
                {
                    arr3[i] = arr2[k2];
                    k2 += 1;
                    continue;
                }
                if (k2 == arr2.Length)
                {
                    arr3[i] = arr1[k1];
                    k1 += 1;
                    continue;
                }
                if (arr2[k2] < arr1[k1])
                {
                    arr3[i] = arr2[k2];
                    count += arr1.Length - k1;
                    k2 += 1;
                }
                else
                {
                    arr3[i] = arr1[k1];
                    k1 += 1;
                }
            }
            return arr3;
        }
    }
}
