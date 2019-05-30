using System;
using System.Collections;

namespace Algoritm2
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var inputArr = input.Split();
            var n = int.Parse(inputArr[0]);
            var maxCapacity = int.Parse(inputArr[1]);

            var weightArr = new int[n];
            var valueArr = new int[n];
            var keys = new double[n];
            var items = new int[n];

            for (int i = 0; i < n; i++)
            {
                input = Console.ReadLine();
                inputArr = input.Split();

                valueArr[i] = int.Parse(inputArr[0]);
                weightArr[i] = int.Parse(inputArr[1]);

                keys[i] = valueArr[i] / (double)weightArr[i];
                items[i] = i;
            }

            Array.Sort(keys,items);

            double sumValue = 0;
            long capacity = 0;

            for (int i = 0; i < n; i++)
            {
                if (capacity == maxCapacity)
                {
                    break;
                }
                else
                {
                    var res = maxCapacity - capacity;
                    if (weightArr[n - 1 - i] <= res)
                    {
                        sumValue = sumValue + valueArr[n - 1 - i];
                        capacity = capacity + weightArr[n - 1 - i];
                    }
                    else
                    {
                        var d = Convert.ToDouble(res) / weightArr[n - 1 - i];
                        capacity = capacity + res;
                        sumValue = sumValue + (d * valueArr[n - 1 - i]);
                    }
                }

            }
            Console.WriteLine(sumValue);
        }
    }
}
