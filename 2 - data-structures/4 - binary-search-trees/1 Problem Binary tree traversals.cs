using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StructureToolbox
{
    class Program
    {
        public struct TreeElement
        {
            public bool IsCreated;
            public int Data;
            public int LeftIn;
            public int RightIn;
        }

        static public Dictionary<int, string> Dict;
        static public TreeElement[] Arr;

        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var m = int.Parse(input);

            Arr = new TreeElement[m];
            Dict = new Dictionary<int, string>();
            for (int i = 0; i < m; i++)
            {
                input = Console.ReadLine();
                var splitted = input.Split();
                var elementData = int.Parse(splitted[0]);
                Arr[i].Data = int.Parse(splitted[0]);
                Arr[i].LeftIn = int.Parse(splitted[1]);
                Arr[i].RightIn = int.Parse(splitted[2]);
                Dict.Add(elementData, splitted[0] + " ");
            }

            var str = new StringBuilder();
            InOrder(Arr[0], str);
            Console.WriteLine(str);
            str.Clear();
            PreOrder(Arr[0], str);
            Console.WriteLine(str);
            str.Clear();
            PostOrder(Arr[0], str);
            Console.WriteLine(str);
        }
        static void InOrder(TreeElement root, StringBuilder str)
        {
            if (root.LeftIn > 0)
            {
                InOrder(Arr[root.LeftIn], str);
            }

            str.Append(Dict[root.Data]);
            if (root.RightIn > 0)
            {
                InOrder(Arr[root.RightIn], str);
            }
        }

        static void PreOrder(TreeElement root, StringBuilder str)
        {
            str.Append(Dict[root.Data]);
            if (root.LeftIn > 0)
            {
                PreOrder(Arr[root.LeftIn], str);
            }
            if (root.RightIn > 0)
            {
                PreOrder(Arr[root.RightIn], str);
            }
        }

        static void PostOrder(TreeElement root, StringBuilder str)
        {
            if (root.LeftIn > 0)
            {
                PostOrder(Arr[root.LeftIn], str);
            }
            if (root.RightIn > 0)
            {
                PostOrder(Arr[root.RightIn], str);
            }
            str.Append(Dict[root.Data]);
        }
    }
}
