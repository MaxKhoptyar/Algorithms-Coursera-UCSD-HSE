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
            var n = int.Parse(input);

            input = Console.ReadLine();
            var inputArr = input.Split();
            var arr = new int[n];
            for (int i = 0; i < n; i++)
            {
                arr[i] = int.Parse(inputArr[i]);
            }

            arr = MergeSort(arr);

            var currInt = arr[0];
            var countInt = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == currInt)
                {
                    countInt += 1;
                    if (countInt > (arr.Length / 2))
                    {
                        Console.WriteLine(1);
                        return;
                    }
                }
                else
                {
                    currInt = arr[i];
                    countInt = 1;
                }
            }
            Console.WriteLine(0);
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
    }
}
