using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication10
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var arr = input.Split();

            var edges = int.Parse(arr[0]);
            var verticies = int.Parse(arr[1]);

            var visited = new int[edges];
            var adj = new List<int>[edges];
            var queue = new Queue<int>();
            for (int i = 0; i < edges; i++)
            {
                adj[i] = new List<int>();
                visited[i] = 0;
            }

            for (int i = 0; i < verticies; i++)
            {
                input = Console.ReadLine();
                arr = input.Split();

                var edge1 = int.Parse(arr[0]) - 1;
                var edge2 = int.Parse(arr[1]) - 1;

                adj[edge1].Add(edge2);
                adj[edge2].Add(edge1);
            }

            input = Console.ReadLine();
            arr = input.Split();

            var start = int.Parse(arr[0]) - 1;
            var end = int.Parse(arr[1]) - 1;

            var l = BFS(start, end, adj, visited, queue);

            Console.WriteLine(l);
            Console.ReadLine();
        }

        private static int BFS(int start, int end, List<int>[] adj, int[] visited, Queue<int> queue)
        {
            queue.Enqueue(start);
            visited[start] = 2;
            var steps = 0;
            while (queue.Count > 0)
            {
                var queue2 = new Queue<int>();

                while (queue.Count > 0)
                {
                    var el = queue.Dequeue();

                    foreach (var v in adj[el])
                    {
                        if (visited[v] != 2)
                        {
                            if (v == end)
                            {
                                return steps + 1;
                            }
                            queue2.Enqueue(v);
                            visited[v] = 2;
                        }
                    }
                }
                queue = queue2;
                steps += 1;
            }
            return -1;
        }
    }
}
