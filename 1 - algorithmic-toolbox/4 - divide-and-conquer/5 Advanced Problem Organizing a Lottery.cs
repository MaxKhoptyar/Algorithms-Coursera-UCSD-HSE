using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingToolbox
{
    class Program
    {
        public struct C
        {
            public int T;
            public int Val;
        }

        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var inputArr = input.Split();
            var sizeDig = int.Parse(inputArr[1]);
            var sizeDiap = int.Parse(inputArr[0]);

            var size = sizeDiap + sizeDiap + sizeDig;
            var arr = new C[size];
            var k = 0;
            for (int i = 0; i < sizeDiap; i++)
            {
                input = Console.ReadLine();
                inputArr = input.Split();
                arr[k] = new C() { T = 1, Val = int.Parse(inputArr[0]) };
                k += 1;
                arr[k] = new C() { T = 2, Val = int.Parse(inputArr[1]) };
                k += 1;
            }
            input = Console.ReadLine();
            inputArr = input.Split(' ');
            var arrDig = new int[sizeDig];
            for (int i = 0; i < sizeDig; i++)
            {
                arr[k] = new C() { T = 0, Val = int.Parse(inputArr[i]) };
                k += 1;
                arrDig[i] = int.Parse(inputArr[i]);
            }
            var d = new Dictionary<int, int>();
            arr = MergeSort(arr);

            var countL = 0;
            var countR = 0;

            for (int i = 0; i < size; i++)
            {
                if (arr[i].T == 1)
                {
                    countL += 1;
                }
                if (arr[i].T == 2)
                {
                    countR += 1;
                }
                if (arr[i].T == 0)
                {
                    if (!d.ContainsKey(arr[i].Val))
                    {
                        d.Add(arr[i].Val, (countL - countR));
                    }
                }
            }

            var sb = new StringBuilder();
            for (int i = 0; i < sizeDig; i++)
            {
                if (d.ContainsKey(arrDig[i]))
                {
                    sb.Append(string.Format("{0} ", d[arrDig[i]]));
                }
                else
                {
                    sb.Append("0 ");
                }
            }
            Console.WriteLine(sb);
            Console.ReadLine();
        }
        static C[] MergeSort(C[] arr)
        {
            if (arr.Length == 1)
            {
                return arr;
            }
            var left = new C[arr.Length / 2];
            var right = new C[arr.Length - left.Length];
            Array.Copy(arr,0,left,0,left.Length);
            Array.Copy(arr,left.Length,right,0,right.Length);
            return Merge(MergeSort(left), MergeSort(right));
        }
        private static C[] Merge(C[] left, C[] right)
        {
            var returnArr = new C[left.Length + right.Length];
            int leftIndex = 0;
            int rightIndex = 0;
            for (int i = 0; i < returnArr.Length; i++)
            {
                if (leftIndex == left.Length)
                {
                    returnArr[i] = right[rightIndex];
                    rightIndex += 1;
                }
                else if (rightIndex == right.Length)
                {
                    returnArr[i] = left[leftIndex];
                    leftIndex += 1;
                }
                else if (left[leftIndex].Val > right[rightIndex].Val)
                {
                    returnArr[i] = right[rightIndex];
                    rightIndex += 1;
                }
                else
                {
                    returnArr[i] = left[leftIndex];
                    leftIndex += 1;
                }
            }
            return returnArr;
        }
    }
}
