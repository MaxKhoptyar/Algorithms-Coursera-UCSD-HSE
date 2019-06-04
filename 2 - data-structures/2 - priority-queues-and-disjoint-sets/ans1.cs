using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureToolbox
{
    class Program
    {
        static List<string> Operations = new List<string>();

        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var n = int.Parse(input);
            var arr = new int[n];
            input = Console.ReadLine();
            var inputArr = input.Split(' ');
            for (int i = 0; i < n; i++)
            {
                arr[i] = int.Parse(inputArr[i]);
            }

            for (int i = 0; i < n; i++)
            {
                SiftDown(n - i - 1, n - 1, arr);
            }

            Console.WriteLine(Operations.Count);
            foreach (var o in Operations)
            {
                Console.WriteLine(o);
            }
            Console.ReadLine();
        }

        private static void SiftDown(int i, int n, int[] arr)
        {
            if (i >= n )
            {
                return;
            }
            var leftChIndex = i * 2 + 1 ;
            var rightChIndex = i * 2  + 2;

            if (leftChIndex > n )
            {
                return;
            }
            else
            {
                var el = arr[i];
                var leftCh = arr[leftChIndex];

                if (rightChIndex > n)
                {
                    if (el > leftCh)
                    {
                        var o = arr[i];
                        arr[i] = arr[leftChIndex];
                        arr[leftChIndex] = o;
                        Operations.Add(string.Format("{0} {1}", i , leftChIndex ));

                        SiftDown(leftChIndex,n,arr);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    var rightCh = arr[rightChIndex];
                    var minIndex = rightChIndex;
                    var min = Math.Min(leftCh, rightCh);
                    if (min == leftCh)
                    {
                        minIndex = leftChIndex;
                    }

                    if (el > arr[minIndex])
                    {
                        var o = arr[i];
                        arr[i] = arr[minIndex];
                        arr[minIndex] = o;
                        Operations.Add(string.Format("{0} {1}",i ,minIndex ));

                        SiftDown(minIndex, n, arr);
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
    }
}
