using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication13
{

    class Program
    {
        static List<int>[] adj;

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

            foreach (var e in adj[v])
            {
                if (!visited[e])
                {
                    FillOlder(e, visited);
                }
            }

            Stack.Push(v);
        }

        public static void Dfs(int v, bool[] visited,ref List<int> list)
        {
            visited[v] = true;
            list.Add(v);
            foreach (var e in TransposeAdj[v])
            {
                if (!visited[e])
                {
                    Dfs(e, visited,ref list);
                }
            }
        }
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var arr = input.Split();

            var edges = int.Parse(arr[0]);
            var verticies = int.Parse(arr[1]);
            var d = new long[edges];

            var edge = new List<Edge>[edges];
            adj = new List<int>[edges];
            TransposeAdj = new List<int>[edges];
            for (int i = 0; i < edges; i++)
            {
                adj[i] = new List<int>();
                edge[i] = new List<Edge>();
                d[i] = long.MaxValue;
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
                adj[edge1].Add(edge2);
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
                    Dfs(i, visited,ref list);
                    lists.Add(list);
                }
            }

            var h = false;
            foreach (var l in lists)
            {
                var x = -1;
                var localEdges = l.Count;
                d[l.First()] = 0;
                for (int i = 0; i < localEdges + 1; i++)
                {
                    x = 0;
                    foreach (var j in l)
                    {
                        foreach (var e in edge[j])
                        {
                            long v;
                            if (d[j] == long.MaxValue)
                            {
                                v = long.MaxValue;
                            }
                            else
                            {
                                v = d[j] + e.Cost;
                            }
                            if (d[e.End] > v)
                            {
                                d[e.End] = v;
                                x = -1;
                            }
                        }
                    }
                }
                if (x == -1)
                {
                    h = true;
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
