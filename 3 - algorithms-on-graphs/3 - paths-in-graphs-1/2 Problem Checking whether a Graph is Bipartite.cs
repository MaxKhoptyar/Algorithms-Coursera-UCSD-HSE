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

            var edges = int.Parse(arr[0]);
            var vertticies = int.Parse(arr[1]);

            var adj = new List<int>[edges];
            var colors = new int[edges];
            for (int i = 0; i < edges; i++)
            {
                adj[i] = new List<int>();
                colors[i] = 0;
            }

            for (int i = 0; i < vertticies; i++)
            {
                input = Console.ReadLine();
                arr = input.Split();

                var edge1 = int.Parse(arr[0]) - 1;
                var edge2 = int.Parse(arr[1]) - 1;

                adj[edge1].Add(edge2);
                adj[edge2].Add(edge1);
            }
            var res = Biparite(adj,colors);

            Console.WriteLine(res);
            Console.ReadLine();
        }

        static int Biparite(List<int>[] adj,int[] colors)
        {
            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i] == 0)
                {
                    var queue = new Queue<int>();
                    queue.Enqueue(i);
                    colors[i] = 1;
                    while (queue.Count != 0)
                    {
                        var edge = queue.Dequeue();


                        if (colors[edge] == 1)
                        {
                            foreach (var e in adj[edge])
                            {
                                if (colors[e] == 1)
                                {
                                    return 0;
                                }
                                if (colors[e] == 0)
                                {
                                    colors[e] = 2;
                                    queue.Enqueue(e);
                                }
                            }
                        }
                        if (colors[edge] == 2)
                        {
                            foreach (var e in adj[edge])
                            {
                                if (colors[e] == 2)
                                {
                                    return 0;
                                }
                                if (colors[e] == 0)
                                {
                                    colors[e] = 1;
                                    queue.Enqueue(e);
                                }
                            }
                        }
                    }

                }
            }
            return 1;
        }
    }
}