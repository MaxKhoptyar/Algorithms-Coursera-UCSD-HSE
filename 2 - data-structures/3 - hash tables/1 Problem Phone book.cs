using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureToolbox
{
    class Program
    {

        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var n = int.Parse(input);
            var dict = new Dictionary<int, string>();

            var sb = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                var splitedArr = Console.ReadLine().Split();
                var o = splitedArr[0].ToLower();
                var number = int.Parse(splitedArr[1]);
                if (o.Equals("add"))
                {
                    dict[number] = splitedArr[2];
                }
                if (o.Equals("find"))
                {
                    if (dict.ContainsKey(number))
                    {
                        sb.Append(dict[number]);
                    }
                    else
                    {
                        sb.Append("not found");
                    }
                }
                if (o.Equals("del"))
                {
                    if (dict.ContainsKey(number))
                    {
                        dict.Remove(number);
                    }
                }
            }
            Console.WriteLine(sb);
        }
    }
}
