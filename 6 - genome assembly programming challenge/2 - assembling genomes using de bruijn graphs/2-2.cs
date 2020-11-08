using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication25
{
    class Vertice
    {
        public int Id;
        public List<Edge> Edges;

        public int Degree;
    }

    class Edge
    {
        public int Id;
        public Vertice StartVertice;
        public Vertice EndVertice;

        public int Time;
    }
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var arr = input.Split();
            var verticeCount = int.Parse(arr[0]);
            var edgeCount = int.Parse(arr[1]);

            var arrayVertice = new Vertice[verticeCount];
            var arrayEdge = new Edge[edgeCount];
            for (int i = 0; i < verticeCount; i++)
            {
                arrayVertice[i] = new Vertice() { Degree = 0, Edges = new List<Edge>(), Id = i };
            }
            for (int i = 0; i < edgeCount; i++)
            {
                input = Console.ReadLine();
                arr = input.Split();

                var x1 = int.Parse(arr[0]) - 1;
                var x2 = int.Parse(arr[1]) - 1;
                arrayVertice[x1].Degree++;
                arrayVertice[x2].Degree--;

                var e = new Edge() { Id = i, StartVertice = arrayVertice[x1], EndVertice = arrayVertice[x2] };
                arrayEdge[i] = e;
                arrayVertice[x1].Edges.Add(e);
            }
            var notEulerianGraph = false;
            for (int i = 0; i < verticeCount; i++)
            {
                if (arrayVertice[i].Degree != 0)
                {
                    notEulerianGraph = true;
                }
            }
            if (notEulerianGraph)
            {
                Console.WriteLine(0);
            }
            else
            {
                Console.WriteLine(1);
                var r = GetPath(arrayVertice, arrayEdge, verticeCount, edgeCount);
                var sb = new StringBuilder();
                foreach (var e in r)
                {
                    sb.Append(string.Format("{0} ",e + 1));
                }
                Console.WriteLine(sb);
            }
            //Console.ReadLine();
        }

        private static List<int> GetPath(Vertice[] arrayVertice, Edge[] arrayEdge, int verticeCount, int edgeCount)
        {
            var cycles = new List<List<int>>();
            var starts = new List<int>();

            var addedEdge = new bool[edgeCount];

            var start = 0;
            var count = 0;
            var queue = new Queue<int>();
            var time = 0;

            while (true)
            {
                if (count == edgeCount)
                {
                    break;
                }
                while (queue.Any())
                {
                    var le = queue.Dequeue();
                    if (!addedEdge[le])
                    {
                        start = le;
                        break;
                    }
                }
                var l = new List<int>();
                var e = arrayEdge[start];

                starts.Add(start);
                l.Add(e.StartVertice.Id);
                DfsEdge(arrayEdge[start], arrayVertice, arrayEdge, addedEdge, l, ref count, time);
                GetStarts(arrayVertice, l, addedEdge, queue);
                cycles.Add(l);
                time++;
            }

            addedEdge = new bool[edgeCount];
            start = starts.Last();
            var res = new List<int>();
            DfsEdgeResult(arrayEdge[start], addedEdge, res);
            return res;
        }

        private static void DfsEdgeResult(Edge edge, bool[] addedEdge, List<int> res)
        {
            addedEdge[edge.Id] = true;
            res.Add(edge.EndVertice.Id);
            Edge next = null;
            var min = int.MaxValue;
            foreach (var e in edge.EndVertice.Edges)
            {
                if (!addedEdge[e.Id])
                {
                    if (e.Time < min)
                    {
                        next = e;
                    }
                }
            }
            if (next != null)
            {
                DfsEdgeResult(next,addedEdge,res);
            }
        }

        private static void GetStarts(Vertice[] arrayVertice, List<int> l, bool[] addedEdge, Queue<int> queue)
        {
            foreach (var verticeId in l)
            {
                foreach (var newEdge in arrayVertice[verticeId].Edges)
                {
                    if (!addedEdge[newEdge.Id])
                    {
                        queue.Enqueue(newEdge.Id);
                    }
                }
            }
        }

        private static void DfsEdge(Edge edge, Vertice[] arrayVertice, Edge[] arrayEdge, bool[] addedEdge, List<int> path, ref int count, int time)
        {
            addedEdge[edge.Id] = true;
            edge.Time = time;
            path.Add(edge.EndVertice.Id);
            count++;
            Edge nextEdge = null;
            foreach (var e in edge.EndVertice.Edges)
            {
                if (!addedEdge[e.Id])
                {
                    nextEdge = e;
                    break;
                }
            }
            if (nextEdge != null)
            {
                DfsEdge(nextEdge, arrayVertice, arrayEdge, addedEdge, path, ref count, time);
            }
        }
    }
}