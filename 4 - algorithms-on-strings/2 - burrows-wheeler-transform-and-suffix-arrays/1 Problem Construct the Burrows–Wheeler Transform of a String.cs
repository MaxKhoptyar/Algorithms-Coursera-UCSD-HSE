using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var inputArr = input.ToCharArray();
            var inputLength = inputArr.Length;

            var l = new BwtSortedList();
            for (int i = 0; i < inputLength; i++)
            {
                var arr = new char[inputLength];

                Array.Copy(inputArr, 0, arr, i, inputLength - i);
                Array.Copy(inputArr, inputLength - i, arr, 0, i);
                l.Add(arr);
            }

            var sb = new StringBuilder();
            foreach (var el in l.list)
            {
                sb.Append(el[inputLength - 1]);
            }

            Console.WriteLine(sb);
            //Console.ReadLine();
        }
    }

    class BwtSortedList
    {
        public List<char[]> list = new List<char[]>();

        public BwtComparer BwtComparer = new BwtComparer();

        public void Add(char[] s)
        {
            if (list.Count == 0)
            {
                list.Add(s);
            }
            else
            {
                var start = 0;
                var end = list.Count;
                while (end > start)
                {
                    var n = start + ((end - start) / 2);
                    if (BwtComparer.Compare(s, list[n]) == 1)
                    {
                        end = n;
                    }
                    else
                    {
                        start = n + 1;
                    }
                }
                list.Insert(start, s);
            }
        }
    }

    class BwtComparer : IComparer<char[]>
    {
        public int Compare(char[] x, char[] y)
        {
            var x_arr = x;
            var x_arr_l = x_arr.Length;
            var y_arr = y;
            var y_arr_l = y_arr.Length;
            var min = Math.Min(x_arr_l, y_arr_l);

            for (int i = 0; i < min; i++)
            {
                if (x_arr[i] == '$' && y_arr[i] == '$')
                {
                    continue;
                }
                if (x_arr[i] == '$')
                {
                    return 1;
                }
                if (y_arr[i] == '$')
                {
                    return -1;
                }
                if (x_arr[i] > y_arr[i])
                {
                    return -1;
                }
                if (x_arr[i] < y_arr[i])
                {
                    return 1;
                }
            }
            if (y_arr_l == x_arr_l)
            {
                return 0;
            }
            if (y_arr_l == min)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }
}