using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var arr = input.Split();

            var verticiesCount = int.Parse(arr[0]);
            var edgeCount = int.Parse(arr[1]);
            var graph = new int[verticiesCount, verticiesCount];

            for (int i = 0; i < edgeCount; i++)
            {
                input = Console.ReadLine();
                arr = input.Split();

                var edge1 = int.Parse(arr[0]) - 1;
                var edge2 = int.Parse(arr[1]) - 1;
                var localFlow = int.Parse(arr[2]);
                graph[edge1, edge2] += localFlow;
            }


            var flow = Karp(graph, 0, verticiesCount - 1);
            Console.WriteLine(flow);
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

                for (int v = 0; v < visited.Length; v++)
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

        public static int Karp(int[,] graph, int s, int t)
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

            return maxFlow;
        }
    }
}