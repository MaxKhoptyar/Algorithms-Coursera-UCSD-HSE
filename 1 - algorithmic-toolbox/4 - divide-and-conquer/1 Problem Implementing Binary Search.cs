using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritm3
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var inputArr = input.Split();
            var n = int.Parse(inputArr[0]);
            var arr = new int[n];

            for (int i = 1; i < n + 1; i++)
            {
                arr[i - 1] = int.Parse(inputArr[i]);
            }

            arr = MergeSort(arr);

            input = Console.ReadLine();
            inputArr = input.Split();
            var k = int.Parse(inputArr[0]);

            var sb = new StringBuilder();
            for (int i = 1; i < k + 1; i++)
            {
                var index = int.Parse(inputArr[i]);
                sb.Append(string.Format("{0} ", BinarySearch(arr, index)));
            }

        }
        static Int32[] MergeSort(Int32[] arr)
        {
            if (arr.Length == 1)
                return arr;
            Int32 midPoint = arr.Length / 2;
            return Merge(MergeSort(arr.Take(midPoint).ToArray()), MergeSort(arr.Skip(midPoint).ToArray()));
        }

        static Int32[] Merge(Int32[] arrLeft, Int32[] arrRight)
        {
            Int32 a = 0, b = 0;
            Int32[] merged = new int[arrLeft.Length + arrRight.Length];
            for (Int32 i = 0; i < arrLeft.Length + arrRight.Length; i++)
            {
                if (b < arrRight.Length && a < arrLeft.Length)
                    if (arrLeft[a] > arrRight[b])
                        merged[i] = arrRight[b++];
                    else 
                        merged[i] = arrLeft[a++];
                else
                  if (b < arrRight.Length)
                    merged[i] = arrRight[b++];
                else
                    merged[i] = arrLeft[a++];
            }
            return merged;
        }

        static int BinarySearch(int[] arr, int x)
        {
            var l = arr.Length;
            if (x < arr[0] || x > arr[l - 1])
            {
                return -1;
            }

            int first = 0;
            int last = l;
            while (first < last)
            {
                int mid = first + (last - first) / 2;

                if (x <= arr[mid])
                {
                    last = mid;
                    if (x == arr[mid])
                    {
                        return last;
                    }
                }
                else
                {
                    first = mid + 1;
                }
            }

            if (arr[last] == x)
            {
                return last;
            }
            else
            {
                return -1;
            }

        }
    }
}
