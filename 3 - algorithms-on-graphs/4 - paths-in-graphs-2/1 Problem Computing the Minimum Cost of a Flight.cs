using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication12
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var arr = input.Split();

            var n = int.Parse(arr[0]);
            var verticies = int.Parse(arr[1]);
            var adj = new int[n, n];
            var colors = new int[n];
            var minCosts = new long[n];
            for (int i = 0; i < n; i++)
            {
                colors[i] = 0;
                minCosts[i] = long.MaxValue;
                for (int j = 0; j < n; j++)
                {
                    adj[i, j] = -1;
                }
            }

            for (int i = 0; i < verticies; i++)
            {
                input = Console.ReadLine();
                arr = input.Split();

                var edge1 = int.Parse(arr[0]) - 1;
                var edge2 = int.Parse(arr[1]) - 1;
                var cost = int.Parse(arr[2]);

                adj[edge1, edge2] = cost;
            }

            input = Console.ReadLine();
            arr = input.Split();

            var start = int.Parse(arr[0]) - 1;
            var end = int.Parse(arr[1]) - 1;

            var list = new List<int>();
            list.Add(start);
            colors[start] = 1;
            minCosts[start] = 0;

            var queue = new Queue<int>();
            queue.Enqueue(start);
            while (queue.Any())
            {
                start = queue.Dequeue();
                for (int i = 0; i < n; i++)
                {
                    if (adj[start, i] > -1)
                    {
                        if (colors[i] == 0)
                        {
                            var cost = minCosts[start] + adj[start, i];
                            if (cost < minCosts[i])
                            {
                                minCosts[i] = cost;
                            }
                            queue.Enqueue(i);
                        }
                    }
                }
                colors[start] = 1;
            }
            if (minCosts[end] == long.MaxValue)
            {
                Console.WriteLine(-1);
            }
            else
            {
                Console.WriteLine(minCosts[end]);
            }
        }
    }
}
