using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var n = int.Parse(input);

            List<int>[] arr;
            if (n <= 3)
            {
                arr = new List<int>[4];
            }
            else
            {
                arr = new List<int>[n + 1];
            }

            arr[1] = new List<int>() { 1 };
            arr[2] = new List<int>() { 1, 2 };
            arr[3] = new List<int>() { 1, 3 };

            if (n > 3)
            {
                for (int i = 4; i < n + 1; i++)
                {
                    var compareList = new List<List<int>>();
                    var l = arr[i - 1].ToList();
                    l.Add(l.Last() + 1);
                    compareList.Add(l);
                    if (i % 3 == 0)
                    {
                        l = arr[i / 3].ToList();
                        l.Add(l.Last() * 3);
                        compareList.Add(l);
                    }
                    if (i % 2 == 0)
                    {
                        l = arr[i / 2].ToList();
                        l.Add(l.Last() * 2);
                        compareList.Add(l);
                    }

                    var minL = compareList[0];
                    foreach (var c in compareList)
                    {
                        if (c.Count < minL.Count)
                        {
                            minL = c;
                        }
                    }
                    arr[i] = minL;
                }
            }
            
            var sb = new StringBuilder();
            sb.Append(string.Format("{0}\n", arr[n].Count - 1));
            foreach (var e in arr[n])
            {
                sb.Append(string.Format("{0} ", e));
            }
            Console.WriteLine(sb);
        }
    }
}
