using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication9
{
    class Program
    {

        static HashSet<int> VerticiesSet = new HashSet<int>();

        static Dictionary<int, int> PathDict;


        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var arr = input.Split();

            var edges = int.Parse(arr[0]);
            var verticies = int.Parse(arr[1]);

            var matrix = new bool[edges, edges];

            for (int i = 0; i < edges; i++)
            {
                VerticiesSet.Add(i);
            }

            for (int i = 0; i < verticies; i++)
            {
                input = Console.ReadLine();
                arr = input.Split();
                var edge1 = int.Parse(arr[0]) - 1;
                var edge2 = int.Parse(arr[1]) - 1;

                matrix[edge1, edge2] = true;
            }
            bool b =false;

            while (VerticiesSet.Count != 0)
            {
                var edge = VerticiesSet.Last();
                VerticiesSet.Remove(edge);

                PathDict = new Dictionary<int, int>();
                M(edge, edges, matrix);
                PathDict[edge] = 2;

                foreach (var v in PathDict)
                {
                    if (v.Value == 1)
                    {
                        b = true;
                        break;
                    }
                }

            }
            if (b)
            {
                Console.WriteLine(1);
            }
            else
            {
                Console.WriteLine(0);
            }
        }

        private static void M(int i, int edges, bool[,] matrix)
        {
            if (VerticiesSet.Contains(i))
            {
                VerticiesSet.Remove(i);
            }

            PathDict.Add(i, 1);
            for (int j = 0; j < edges; j++)
            {
                if (matrix[i, j])
                {
                    if (PathDict.ContainsKey(j))
                    {
                        if (PathDict[j] == 1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        M(j, edges, matrix);
                    }
                }
            }
            PathDict[i] = 2;
        }
    }
}
