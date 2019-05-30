using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Algoritm2
{
    class Program
    {
        public class O : IComparable<O>
        {
            public long Id;
            public string Value;
            public long Length;

            public int CompareTo(O other)
            {
                var o1 = this;
                var o2 = other;

                var len = o1.Length;
                if (o2.Length < len)
                {
                    len = o2.Length;
                }

                var list1 = o1.Value.ToCharArray();
                var list2 = o2.Value.ToCharArray();

                for (int i = 0; i < len; i++)
                {
                    var int1 = Char.GetNumericValue(list1[i]);
                    var int2 = Char.GetNumericValue(list2[i]);

                    if (int1 > int2)
                    {
                        return 1;
                    }
                    if (int2 > int1)
                    {
                        return -1;
                    }
                }

                var sumInt1First = long.Parse(o1.Value + o2.Value);
                var sumInt2First = long.Parse(o2.Value + o1.Value);

                if (sumInt1First > sumInt2First)
                {
                    return 1;
                }
                if (sumInt2First > sumInt1First)
                {
                    return -1;
                }
                return 0;
            }
        }

        static void Main(string[] args)
        {

            var input = Console.ReadLine();
            var n = long.Parse(input);

            var list = new List<O>();
            input = Console.ReadLine();

            var inputArray = input.Split();
            var maxLength = 0;

            for (int i = 0; i < n; i++)
            {
                if (inputArray[i].Length > maxLength)
                {
                    maxLength = inputArray[i].Length;
                }

                var o = new O();
                o.Value = inputArray[i];
                o.Length = inputArray[i].Length;
                o.Id = i;
                list.Add(o);
            }

            list = list.OrderByDescending(x => x).ToList();
            var sb = new StringBuilder();
            foreach (var e in list)
            {
                sb.Append(e.Value);
            }
            Console.WriteLine(sb);
        }
    }
}
