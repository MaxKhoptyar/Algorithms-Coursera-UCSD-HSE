using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication15
{


    class Program
    {
        class ComparerEdges : IComparer<Edge>
        {
            public int Compare(Edge x, Edge y)
            {
                var l = Math.Min(x.Length, y.Length);
                for (int i = 0; i < l; i++)
                {
                    if (Arr[x.Start + i] == Arr[y.Start + i])
                    {
                        continue;
                    }
                    if (Arr[x.Start + i] == '$')
                    {
                        return 1;
                    }
                    if (Arr[y.Start + i] == '$')
                    {
                        return -1;
                    }
                    if (Arr[x.Start + i] > Arr[y.Start + i])
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
                if (x.Length == y.Length)
                {
                    return 0;
                }
                if (x.Length > y.Length)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
        }

        class EdgeList
        {
            public List<Edge> Storage;
            private ComparerEdges _comparer;
            private int Count
            {
                get { return Storage.Count; }
            }

            public EdgeList()
            {
                Storage = new List<Edge>();
                _comparer = new ComparerEdges();
            }

            public void Add(Edge e)
            {
                if (!Storage.Any())
                {
                    Storage.Add(e);
                }
                else
                {
                    var start = 0;
                    var end = Count;
                    while (end > start)
                    {
                        var i = start + ((end - start) / 2);
                        if (_comparer.Compare(Storage[i], e) == 1)
                        {
                            start = i + 1;
                        }
                        else
                        {
                            end = i;
                        }
                    }
                    Storage.Insert(start, e);
                }
            }
        }

        class Edge
        {
            public int Start;
            public int Length;
            public Edge Parent;
            public bool IsLeaf;
            public List<Edge> Childs;

        }

        static char[] Arr;

        static void Main(string[] args)
        {
            var input = Console.ReadLine();

            var inputArr = input.ToCharArray();
            Arr = inputArr;
            var length = inputArr.Length;

            var sortEdge = new EdgeList();
            for (int i = 0; i < length; i++)
            {
                var edge = new Edge();
                edge.Length = i + 1;
                edge.Start = length - 1 - i;
                
                sortEdge.Add(edge);
            }
            var sb = new StringBuilder();
            foreach (var edge in sortEdge.Storage)
            {
                sb.Append(string.Format("{0} ",length - edge.Length));
            }
            Console.WriteLine(sb.ToString());
        }
    }
}