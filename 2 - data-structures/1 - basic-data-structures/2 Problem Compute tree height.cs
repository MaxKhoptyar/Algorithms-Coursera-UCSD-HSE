using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureToolbox
{
    class Program
    {
        public class Node
        {
            public int index;
            public List<Node> childs = new List<Node>();

            public void AddCh(Node node)
            {
                childs.Add(node);
            }
        }

        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var n = int.Parse(input);
            var parent = new int[n];
            var nodes = new Node[n];

            input = Console.ReadLine();
            var inputArr = input.Split();
            for (int i = 0; i < n; i++)
            {
                parent[i] = int.Parse(inputArr[i]);
                nodes[i] = new Node() { index = i };
            }
            Node root = null;
            for (int i = 0; i < n; i++)
            {
                if (parent[i] == -1)
                {
                    root = nodes[i];
                }
                else
                {
                    nodes[parent[i]].AddCh(nodes[i]);
                }
            }
            var q = new Queue<Node>();
            q.Enqueue(root);
            var height = 0;

            while (q.Any())
            {
                var size = q.Count;
                if (size > 0)
                {
                    height += 1;
                }
                for (int j = 0; j < size; j++)
                {
                    var item = q.Dequeue();
                    foreach (var c in item.childs)
                    {
                        q.Enqueue(c);
                    }
                }
            }

            Console.WriteLine(height);
        }
    }
}
