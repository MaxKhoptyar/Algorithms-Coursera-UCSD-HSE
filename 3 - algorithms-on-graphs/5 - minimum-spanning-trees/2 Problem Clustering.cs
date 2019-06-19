using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication15
{
    class Program
    {
        struct Node
        {
            public int X;
            public int Y;
        }

        static Node[] Nodes;
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var arr = input.Split();
            var c = int.Parse(arr[0]);

            var adj = new double[c, c];
            Nodes = new Node[c];
            for (int i = 0; i < c; i++)
            {
                input = Console.ReadLine();
                arr = input.Split();

                var x = int.Parse(arr[0]);
                var y = int.Parse(arr[1]);

                Nodes[i] = new Node() { X = x, Y = y };
            }

            input = Console.ReadLine();
            var clusters = int.Parse(input);
            var queue = new Queue();
            for (int i = 0; i < c; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    var w = Math.Sqrt(Math.Pow(Nodes[i].X - Nodes[j].X, 2) + Math.Pow(Nodes[i].Y - Nodes[j].Y, 2));
                    adj[i, j] = w;
                    if (i != j)
                    {
                        var el = new QueueElement(i, j, w);
                        queue.Enque(el);
                    }

                }
            }

            var edges = new List<int>[c];
            var listKeys = new List<QueueElement>();
            for (int i = 0; i < c; i++)
            {
                edges[i] = new List<int>();
            }
            QueueElement lastElement = null;
            for (int i = 0; i < c - clusters + 1; i++)
            {
                lastElement = null;
                do
                {
                    if (lastElement != null)
                    {
                        edges[lastElement.E1].Remove(lastElement.E2);
                        edges[lastElement.E2].Remove(lastElement.E1);
                    }
                    lastElement = queue.Deque();
                    edges[lastElement.E1].Add(lastElement.E2);
                    edges[lastElement.E2].Add(lastElement.E1);
                } while (CheckCycle(edges));

                listKeys.Add(lastElement);
            }
            Console.WriteLine(lastElement.Weight);
        }

        static bool CheckCycle(List<int>[] gr)
        {
            var l = gr.Length;
            var visited = new bool[l];
            for (int i = 0; i < l; i++)
            {
                visited[i] = false;
            }
            for (int i = 0; i < l; i++)
            {
                visited[i] = true;
                foreach (var v in gr[i])
                {
                    if (!visited[v])
                    {
                        var res = Dfs(i, v, visited, gr);
                        if (res)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        static bool Dfs(int startPoint, int endPoint, bool[] visited, List<int>[] gr)
        {
            if (visited[endPoint])
            {
                return true;
            }
            visited[endPoint] = true;
            foreach (var v in gr[endPoint])
            {
                if (startPoint != v)
                {
                    var res = Dfs(endPoint, v, visited, gr);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public class QueueElement
        {
            public int E1;
            public int E2;
            public double Weight;

            public QueueElement(int e1, int e2, double w)
            {
                E1 = e1;
                E2 = e2;
                Weight = w;
            }
        }
        public class Queue
        {
            public List<QueueElement> List;

            public Queue()
            {
                List = new List<QueueElement>();
            }

            public int Count
            {
                get { return List.Count; }
            }


            public void Enque(QueueElement qe)
            {
                if (Count == 0)
                {
                    List.Add(qe);
                }
                else
                {
                    var start = 0;
                    var end = Count;
                    var i = (end - start) / 2;

                    while (end - start > 0)
                    {
                        if (qe.Weight > List[i].Weight)
                        {
                            start = i + 1;
                            i = (end - start) / 2 + start;
                        }
                        else
                        {
                            end = i;
                            i = (end - start) / 2;
                        }
                    }
                    List.Insert(start, qe);
                }
            }

            public QueueElement Deque()
            {
                var res = List[0];
                List.Remove(res);
                return res;
            }

            public QueueElement GetMin()
            {
                return List[0];
            }
        }
    }
}
