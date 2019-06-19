using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication14
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();

            var edges = int.Parse(input);
            var lengths = new double[edges, edges];
            var x_c = new int[edges];
            var y_c = new int[edges];
            for (int i = 0; i < edges; i++)
            {
                input = Console.ReadLine();
                var arr = input.Split();
                var x = int.Parse(arr[0]);
                var y = int.Parse(arr[1]);

                x_c[i] = x;
                y_c[i] = y;
            }

            for (int i = 0; i < edges; i++)
            {
                for (int j = 0; j < edges; j++)
                {
                    lengths[i, j] = Math.Sqrt(Math.Pow(x_c[i] - x_c[j], 2) + Math.Pow(y_c[i] - y_c[j], 2));
                }
            }
            double s = 0;
            var sortedList = new PriorityQueue();
            var setEdges = new HashSet<int>();
            setEdges.Add(0);
            for (int i = 1; i < edges; i++)
            {
                var n = new Node();
                n.id = i;
                n.weight = lengths[0, i];
                sortedList.Enqueue(n);
            }

            for (int i = 1; i < edges; i++)
            {
                Node el;
                while (true)
                {

                    el = sortedList.Dequeue();
                    if (!setEdges.Contains(el.id))
                    {
                        break;
                    }
                }
                setEdges.Add(el.id);
                for (int j = 0; j < edges; j++)
                {
                    var n = new Node();
                    n.id = j;
                    n.weight = lengths[el.id, j];
                    sortedList.Enqueue(n);
                }
                s += el.weight;
            }

            Console.WriteLine(s);
        }

        class Node
        {
            public int id;
            public double weight;
        }

        class PriorityQueue
        {
            public List<Node> list;
            public int Count
            {
                get { return list.Count; }
            }

            public PriorityQueue()
            {
                list = new List<Node>();
            }

            public void Enqueue(Node x)
            {
                if (Count == 0)
                {
                    list.Add(x);
                }
                else
                {
                    var end = Count;
                    var start = 0;
                    while (end - start > 0)
                    {
                        var i = start + (end - start) / 2;

                        if (list[i].weight > x.weight)
                        {
                            end = i;
                        }
                        else
                        {
                            start = i + 1;
                        }
                    }

                    list.Insert(start, x);
                }
            }

            public Node Dequeue()
            {
                var min = Peek();
                list.Remove(min);
                return min;
            }


            public Node Peek()
            {
                if (Count == 0) throw new InvalidOperationException("empty");
                return list[0];
            }
        }
    }
}
