using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication25
{
    class BaseVerice
    {
        public int Id;
        public List<BaseEdge> Edges;
    }

    class BaseEdge
    {
        public BaseVerice StartBaseVerice;
        public BaseVerice EndBaseVerice;
    }

    class Vertice
    {
        public int Id;
        public int IdStock;
        public List<Edge> Edges;
    }

    class Edge
    {
        public Vertice StartVertice;
        public Vertice EndVertice;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var arr = input.Split();

            var countStocks = int.Parse(arr[0]);
            var countPoints = int.Parse(arr[1]);
            var stocks = new int[countStocks][];
            var baseVerticesArray = new BaseVerice[countStocks];

            for (int i = 0; i < countStocks; i++)
            {
                stocks[i] = new int[countPoints];
                input = Console.ReadLine();
                arr = input.Split();

                for (int j = 0; j < countPoints; j++)
                {
                    stocks[i][j] = int.Parse(arr[j]);
                }
                baseVerticesArray[i] = new BaseVerice() { Id = i, Edges = new List<BaseEdge>() };
            }

            for (int i = 0; i < countStocks; i++)
            {

                for (int j = 0; j < countStocks; j++)
                {
                    var needCreateEdge = true;
                    for (int k = 0; k < countPoints; k++)
                    {
                        if (stocks[i][k] >= stocks[j][k])
                        {
                            needCreateEdge = false;
                            break;
                        }
                    }

                    if (needCreateEdge)
                    {
                        var e = new BaseEdge();
                        e.StartBaseVerice = baseVerticesArray[i];
                        e.EndBaseVerice = baseVerticesArray[j];
                        baseVerticesArray[i].Edges.Add(e);
                    }
                }
            }

            var verticeArray = new Vertice[countStocks * 2 + 2];
            var source = new Vertice() { Edges = new List<Edge>(), IdStock = -1, Id = 0 };
            verticeArray[0] = source;
            var sink = new Vertice() { Edges = new List<Edge>(), IdStock = -1, Id = countStocks * 2 + 1 };
            verticeArray[countStocks * 2 + 1] = sink;
            var verticiesCount = countStocks * 2 + 2;

            for (int i = 0; i < countStocks; i++)
            {
                verticeArray[i * 2 + 1] = new Vertice() { Edges = new List<Edge>(), Id = i * 2 + 1, IdStock = i };
                var se = new Edge() { StartVertice = source, EndVertice = verticeArray[i * 2 + 1] };
                source.Edges.Add(se);

                verticeArray[(i * 2) + 2] = new Vertice() { Edges = new List<Edge>(), Id = (i * 2) + 2, IdStock = i };
                var ee = new Edge() { StartVertice = verticeArray[(i * 2) + 2], EndVertice = sink };
                verticeArray[(i * 2) + 2].Edges.Add(ee);
            }

            for (int i = 0; i < countStocks; i++)
            {
                foreach (var e in baseVerticesArray[i].Edges)
                {
                    var startIndex = e.StartBaseVerice.Id * 2 + 1;
                    var endIndex = e.EndBaseVerice.Id * 2 + 2;

                    var newE = new Edge();
                    newE.StartVertice = verticeArray[startIndex];
                    newE.EndVertice = verticeArray[endIndex];
                    verticeArray[startIndex].Edges.Add(newE);
                }
            }

            var graph = new int[verticiesCount, verticiesCount];
            for (int i = 0; i < verticiesCount; i++)
            {
                foreach (var e in verticeArray[i].Edges)
                {
                    graph[e.StartVertice.Id, e.EndVertice.Id] = 1;
                }
            }

            var flow = Karp(graph, 0, verticiesCount - 1);
            Console.WriteLine(countStocks - flow);
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

        public static int Karp(int[,] graph, int s, int t)
        {
            int u, v;
            int[,] rGraph = new int[graph.GetLength(0), graph.GetLength(1)];

            for (u = 0; u < graph.GetLength(0); u++)
            {
                for (v = 0; v < graph.GetLength(1); v++)
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
