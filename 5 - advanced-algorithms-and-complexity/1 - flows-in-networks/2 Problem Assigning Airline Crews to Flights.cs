using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication13
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var arr = input.Split();

            var verticiesCount1 = int.Parse(arr[0]);
            var verticiesCount2 = int.Parse(arr[1]);
            var verticiesCount = verticiesCount1 + verticiesCount2 + 2;

            var graph = new int[verticiesCount, verticiesCount];
            for (int i = 0; i < verticiesCount1; i++)
            {
                graph[0, i + 1] = 1;
            }
            for (int i = 1; i < verticiesCount1 + 1; i++)
            {
                input = Console.ReadLine();
                arr = input.Split();

                for (int j = 0; j < verticiesCount2; j++)
                {
                    if (int.Parse(arr[j]) == 1)
                    {
                        graph[i, j + verticiesCount1 + 1] = 1;
                    }
                    else
                    {
                        graph[i, j + verticiesCount1 + 1] = 0;
                    }
                }
            }

            for (int i = 0; i < verticiesCount2; i++)
            {
                graph[i + verticiesCount1 + 1, verticiesCount - 1] = 1;
            }

            var rGraph = Karp(graph, 0, verticiesCount - 1);
            var sb = new StringBuilder();
            for (int i = 1; i < verticiesCount1 + 1; i++)
            {
                for (int j = verticiesCount1 + 1; j < verticiesCount - 1; j++)
                {
                    if (rGraph[j, i] == 1)
                    {
                        sb.Append(j - verticiesCount1);
                        break;
                    }
                    if (j == verticiesCount - 2)
                    {
                        sb.Append(-1);
                    }
                }
                sb.Append(' ');
            }

            Console.WriteLine(sb.ToString());
            //Console.ReadLine();
        }

        public static bool Bfs(int[,] rGraph, int s, int t, int[] parent)
        {
            bool[] visited = new bool[parent.Length];
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(s);
            visited[s] = true;
            parent[s] = -1;

            while (queue.Any())
            {
                int u = queue.Dequeue();

                for (int v = 0; v < parent.Length; v++)
                {
                    if (visited[v] == false && rGraph[u, v] > 0)
                    {
                        queue.Enqueue(v);
                        parent[v] = u;
                        visited[v] = true;
                    }
                }
            }
            return visited[t];
        }

        public static int[,] Karp(int[,] graph, int s, int t)
        {
            int u, v;
            int[,] rGraph = new int[graph.GetLength(0), graph.GetLength(1)];

            for (u = 0; u < graph.GetLength(0); u++)
            {
                for (v = 0; v < graph.GetLength(0); v++)
                {
                    rGraph[u, v] = graph[u, v];
                }
            }

            int[] parent = new int[graph.GetLength(0)];
            int maxFlow = 0;

            while (Bfs(rGraph, s, t, parent))
            {
                int pathFlow = int.MaxValue;
                for (v = t; v != s; v = parent[v])
                {
                    u = parent[v];
                    pathFlow = Math.Min(pathFlow, rGraph[u, v]);
                }
                for (v = t; v != s; v = parent[v])
                {
                    u = parent[v];
                    rGraph[u, v] -= pathFlow;
                    rGraph[v, u] += pathFlow;
                }
                maxFlow += pathFlow;
            }
            return rGraph;
        }
    }
}