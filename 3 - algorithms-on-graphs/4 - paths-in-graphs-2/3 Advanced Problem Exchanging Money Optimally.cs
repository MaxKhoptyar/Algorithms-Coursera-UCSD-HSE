using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication13
{

    class Program
    {
        static Edge[] EdgesArr;
        struct Edge
        {
            public List<Vertice> Vertices;
        }

        struct Vertice
        {
            public int End;
            public int Cost;
        }

        public static void Dfs(int v, bool[] visited, ref List<int> list)
        {
            visited[v] = true;
            list.Add(v);
            foreach (var e in EdgesArr[v].Vertices)
            {
                if (!visited[e.End])
                {
                    Dfs(e.End, visited, ref list);
                }
            }
        }

        public static void DfsCycle(int v, bool[] visited)
        {
            visited[v] = true;
            foreach (var e in EdgesArr[v].Vertices)
            {
                if (!visited[e.End])
                {
                    DfsCycle(e.End, visited);
                }
            }
        }
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var arr = input.Split();

            var edgesCount = int.Parse(arr[0]);
            var verticiesCount = int.Parse(arr[1]);
            var resultArr = new long[edgesCount];

            EdgesArr = new Edge[edgesCount];
            for (int i = 0; i < edgesCount; i++)
            {
                var e = new Edge();
                e.Vertices = new List<Vertice>();
                EdgesArr[i] = e;
                resultArr[i] = long.MaxValue;
            }
            for (int i = 0; i < verticiesCount; i++)
            {
                input = Console.ReadLine();
                arr = input.Split();

                var edge1 = int.Parse(arr[0]) - 1;
                var edge2 = int.Parse(arr[1]) - 1;
                var cost = int.Parse(arr[2]);

                var v = new Vertice();
                v.Cost = cost;
                v.End = edge2;

                EdgesArr[edge1].Vertices.Add(v);
            }

            var startP = int.Parse(Console.ReadLine());


            bool[] visited = new bool[edgesCount];

            var list = new List<int>();
            Dfs(startP - 1, visited, ref list);

            var listArr = list.ToArray();
            var listArrLength = listArr.Length;
            resultArr[startP - 1] = 0;
            int lastV;
            for (int i = 0; i < listArrLength - 1; i++)
            {
                lastV = -1;
                for (int ji = 0; ji < listArrLength; ji++)
                {
                    var j = listArr[ji];
                    foreach (var e in EdgesArr[j].Vertices)
                    {
                        long v = resultArr[j] + e.Cost;
                        if (resultArr[e.End] > v)
                        {
                            resultArr[e.End] = v;
                            lastV = e.End;
                        }
                    }
                }
                if (lastV == -1)
                {
                    break;
                }
            }

            bool[] visited2 = new bool[edgesCount];
            for (int ji = 0; ji < listArrLength; ji++)
            {
                var j = listArr[ji];
                foreach (var e in EdgesArr[j].Vertices)
                {
                    long v = resultArr[j] + e.Cost;
                    if (resultArr[e.End] > v)
                    {
                        resultArr[e.End] = v;
                        if (!visited2[e.End])
                        {
                            DfsCycle(e.End, visited2);
                        }
                    }
                }
            }

            var sb = new StringBuilder();
            for (int i = 0; i < edgesCount; i++)
            {
                if (!visited[i])
                {
                    sb.Append(string.Format("*\n"));
                }
                else
                {
                    if (visited2[i])
                    {
                        sb.Append(string.Format("-\n"));
                    }
                    else
                    {
                        sb.Append(string.Format("{0}\n", resultArr[i]));
                    }
                }
            }
            Console.WriteLine(sb.ToString());
        }
    }
}
