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
            public int x;
            public int y;
        }

        static float Dist(C p1, C p2)
        {
            return (float)Math.Sqrt((p1.x - p2.x) * (p1.x - p2.x) +
                (p1.y - p2.y) * (p1.y - p2.y)
            );
        }

        static float StripClosest(C[] strip, int size, float d)
        {
            float min = d;
            if (strip.Length == 0)
            {
                return min;
            }
            strip = MergeSort(strip, 0, strip.Length - 1,false);

            for (int i = 0; i < size; ++i)
                for (int j = i + 1; j < size && (strip[j].y - strip[i].y) < min; ++j)
                    if (Dist(strip[i], strip[j]) < min)
                        min = Dist(strip[i], strip[j]);

            return min;
        }
        static float ClosestUtil(C[] P, int n)
        {
            int j;
            if (n <= 3)
            {
                float min = float.MaxValue;
                for (int i = 0; i < n; i++)
                    for (j = i + 1; j < n; j++)
                        if (Dist(P[i], P[j]) < min)
                            min = Dist(P[i], P[j]);
                return min;
            }

            int mid = n / 2;
            C midPoint = P[mid];
            var p1 = P.Take(mid).ToArray();
            float dl = ClosestUtil(p1, mid);
            var p2 = P.Skip(mid).ToArray();
            float dr = ClosestUtil(p2, n - mid);

            float d = Math.Min(dl, dr);

            var ls = new List<C>();
            j = 0;
            for (int i = 0; i < n; i++)
            {
                if (Math.Abs(P[i].x - midPoint.x) < d)
                {
                    ls.Add(P[i]);
                    j++;
                }
            }
            var strip = ls.ToArray();
            return Math.Min(d, StripClosest(strip, j, d));
        }
        static void Main(string[] args)
        {
            var input = Console.ReadLine();

            var n = int.Parse(input);
            var fullArr = new C[n];
            for (int i = 0; i < n; i++)
            {
                input = Console.ReadLine();
                var inputArr = input.Split(' ');
                fullArr[i] = new C() { x = int.Parse(inputArr[0]), y = int.Parse(inputArr[1]) };
            }
            fullArr = MergeSort(fullArr, 0, n - 1);
            var a = ClosestUtil(fullArr, n);
            Console.WriteLine(a);
        }
        static C[] MergeSort(C[] arr, int start, int end, bool isXsort = true)
        {
            C[] returnArr = new C[] { };
            if (start == end)
            {
                returnArr = new C[1];
                returnArr[0] = arr[start];
                return returnArr;
            }

            var c = (end - start) / 2;
            var arr1 = MergeSort(arr, start, start + c);
            var arr2 = MergeSort(arr, start + c + 1, end);

            var k1 = 0;
            var k2 = 0;

            var l1 = arr1.Length;
            var l2 = arr2.Length;

            returnArr = new C[l1 + l2];
            for (int i = 0; i < l1 + l2; i++)
            {
                if (k2 == l2)
                {
                    returnArr[i] = arr1[k1];
                    k1 += 1;
                    continue;
                }
                if (k1 == l1)
                {
                    returnArr[i] = arr2[k2];
                    k2 += 1;
                    continue;
                }
                if (isXsort)
                {
                    if (arr1[k1].x < arr2[k2].x)
                    {
                        returnArr[i] = arr1[k1];
                        k1 += 1;
                    }
                    else
                    {
                        returnArr[i] = arr2[k2];
                        k2 += 1;
                    }
                }
                else
                {
                    if (arr1[k1].y < arr2[k2].y)
                    {
                        returnArr[i] = arr1[k1];
                        k1 += 1;
                    }
                    else
                    {
                        returnArr[i] = arr2[k2];
                        k2 += 1;
                    }
                }

            }
            return returnArr;
        }
    }
}
