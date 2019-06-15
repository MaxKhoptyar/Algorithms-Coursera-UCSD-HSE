using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication9
{
    class Program
    {


        static Dictionary<int, int> PathDict = new Dictionary<int, int>();

        static Stack<int> Stack = new Stack<int>();

        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var arr = input.Split();

            var edges = int.Parse(arr[0]);
            var verticies = int.Parse(arr[1]);

            var matrix = new bool[edges, edges];

            for (int i = 0; i < edges; i++)
            {
                PathDict.Add(i, 1);
            }

            for (int i = 0; i < verticies; i++)
            {
                input = Console.ReadLine();
                arr = input.Split();
                var edge1 = int.Parse(arr[0]) - 1;
                var edge2 = int.Parse(arr[1]) - 1;

                matrix[edge1, edge2] = true;
            }
            
            while (PathDict.Any(x => x.Value == 1))
            {
                var set = PathDict.Where(x => x.Value == 1);
                var edge = set.First().Key;
                var entriesMin = int.MaxValue;
                foreach (var v in set)
                {

                    var entries = 0;
                    for (int i = 0; i < edges; i++)
                    {
                        if (matrix[i, v.Key])
                        {
                            entries += 1;
                        }
                    }

                    var min = entries;

                    if (min < entriesMin)
                    {
                        edge = v.Key;
                        entriesMin = min;
                    }
                    if (min == entriesMin)
                    {
                        if (v.Key > edge)
                        {
                            edge = v.Key;
                            entriesMin = min;
                        }
                    }
                }
                M(edge, edges, matrix);
            }

            while (Stack.Count != 0)
            {
                Console.Write(string.Format("{0} ", Stack.Pop()));
            }
        }

        private static void M(int i, int edges, bool[,] matrix)
        {
            
            for (int j = 0; j < edges; j++)
            {
                if (matrix[i, j])
                {
                    if (PathDict[j] != 2)
                    {
                        M(j, edges, matrix);
                    }
                }
            }

            PathDict[i] = 2;
            Stack.Push(i + 1);
        }
    }
}
