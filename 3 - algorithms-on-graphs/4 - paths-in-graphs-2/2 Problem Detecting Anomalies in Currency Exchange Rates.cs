using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication13
{

    class Program
    {
        static List<int>[] Adj;

        static List<int>[] TransposeAdj;

        static Stack<int> Stack;

        struct Edge
        {
            public int End;
            public int Cost;
        }

        public static void FillOlder(int v, bool[] visited)
        {
            visited[v] = true;

            foreach (var e in Adj[v])
            {
                if (!visited[e])
                {
                    FillOlder(e, visited);
                }
            }

            Stack.Push(v);
        }

        public static void Dfs(int v, bool[] visited, ref List<int> list)
        {
            visited[v] = true;
            list.Add(v);
            foreach (var e in TransposeAdj[v])
            {
                if (!visited[e])
                {
                    Dfs(e, visited, ref list);
                }
            }
        }
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var arr = input.Split();

            var edges = int.Parse(arr[0]);
            var verticies = int.Parse(arr[1]);
            var distances = new long[edges];

            var edge = new List<Edge>[edges];
            Adj = new List<int>[edges];
            TransposeAdj = new List<int>[edges];
            for (int i = 0; i < edges; i++)
            {
                Adj[i] = new List<int>();
                edge[i] = new List<Edge>();
                distances[i] = long.MaxValue;
                TransposeAdj[i] = new List<int>();
            }
            for (int i = 0; i < verticies; i++)
            {
                input = Console.ReadLine();
                arr = input.Split();

                var edge1 = int.Parse(arr[0]) - 1;
                var edge2 = int.Parse(arr[1]) - 1;
                var cost = int.Parse(arr[2]);

                var e = new Edge();
                e.Cost = cost;
                e.End = edge2;

                edge[edge1].Add(e);
                Adj[edge1].Add(edge2);
                TransposeAdj[edge2].Add(edge1);
            }

            Stack = new Stack<int>();

            bool[] visited = new bool[edges];
            for (int i = 0; i < edges; i++)
            {
                visited[i] = false;
            }

            for (int i = 0; i < edges; i++)
            {
                if (visited[i] == false)
                {
                    FillOlder(i, visited);
                }
            }

            for (int i = 0; i < edges; i++)
            {
                visited[i] = false;
            }


            var lists = new List<List<int>>();
            while (Stack.Count != 0)
            {
                int i = Stack.Pop();

                if (visited[i] == false)
                {
                    var list = new List<int>();
                    Dfs(i, visited, ref list);
                    lists.Add(list);
                }
            }

            var h = false;
            foreach (var l in lists)
            {
                var localEdges = l.Count;
                distances[l.First()] = 0;
                for (int i = 0; i < localEdges + 1; i++)
                {
                    foreach (var j in l)
                    {
                        foreach (var e in edge[j])
                        {
                            long v;
                            if (distances[j] == long.MaxValue)
                            {
                                v = long.MaxValue;
                            }
                            else
                            {
                                v = distances[j] + e.Cost;
                            }
                            if (distances[e.End] > v)
                            {
                                distances[e.End] = v;
                                h = true;
                            }
                        }
                    }
                }
                if (h)
                {
                    break;
                }
            }

            if (h)
            {
                Console.WriteLine(1);
            }
            else
            {
                Console.WriteLine(0);
            }
        }
    }
}
