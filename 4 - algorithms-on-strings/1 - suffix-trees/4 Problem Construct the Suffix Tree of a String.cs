using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication15
{
    class Edge
    {
        public char[] ValueList;
        public List<Edge> Childs;
    }
    class Edge2
    {
        public char Value;
        public List<Edge2> Childs;
    }

    class Program
    {
        public static List<string> l1 = new List<string>();
        public static List<string> l2 = new List<string>();

        static void Main(string[] args)
        {
            var input = Console.ReadLine();

            var root = new Edge();
            root.Childs = new List<Edge>();

            var inputArr = input.ToCharArray();
            var length = inputArr.Length;
            var root2 = new Edge2();
            root2.Childs = new List<Edge2>();
            for (int i = 0; i < length; i++)
            {
                var edge = root2;
                for (int j = i; j < length; j++)
                {
                    var searchElement = edge.Childs.FirstOrDefault(c => c.Value == inputArr[j]);
                    if (searchElement == null)
                    {
                        var newEdge = new Edge2();
                        newEdge.Value = inputArr[j];
                        newEdge.Childs = new List<Edge2>();

                        edge.Childs.Add(newEdge);
                        edge = newEdge;
                    }
                    else
                    {
                        edge = searchElement;
                    }
                }
            }

            foreach (var c in root2.Childs)
            {
                var sb= new StringBuilder();
                Dfs2(c, sb, l2);
            }

            for (int i = length - 1; i >= 0; i--)
            {
                var curentEdge = root;

                for (int j = i; j < length; j++)
                {
                    var searchElement = curentEdge.Childs.FirstOrDefault(x => x.ValueList[0] == inputArr[j]);
                    if (searchElement == null)
                    {
                        var newEdge = new Edge();
                        newEdge.ValueList = new char[length - j];

                        for (int k = 0; k < length - j; k++)
                        {
                            newEdge.ValueList[k] = inputArr[j + k];
                        }

                        newEdge.Childs = new List<Edge>();

                        curentEdge.Childs.Add(newEdge);
                        break;
                    }
                    else
                    {
                        var arr = searchElement.ValueList;

                        var minL = Math.Min(arr.Length, length - j);
                        var copyL = 1;
                        for (int k = 1; k < minL; k++)
                        {
                            if (arr[k] == inputArr[j + k])
                            {
                                copyL++;
                            }
                            else
                            {
                                break;
                            }
                        }

                        if (copyL < arr.Length)
                        {
                            var newEdge = new Edge();

                            newEdge.ValueList = new char[arr.Length - copyL];
                            for (int k = copyL; k < arr.Length; k++)
                            {
                                newEdge.ValueList[k - copyL] = arr[k];
                            }
                            newEdge.Childs = searchElement.Childs;
                            searchElement.Childs = new List<Edge>();
                            searchElement.Childs.Add(newEdge);
                            searchElement.ValueList = new char[copyL];
                            for (int k = 0; k < copyL; k++)
                            {
                                searchElement.ValueList[k] = arr[k];
                            }
                        }
                        j = j + copyL - 1;
                        curentEdge = searchElement;
                    }
                }
            }

            foreach (var child in root.Childs)
            {
                Dfs(child, l1);
            }

            Console.WriteLine(l1.Count);
            Console.WriteLine(l2.Count);

            var counter = 0;
            foreach (var e in l1)
            {
                if (l2.Contains(e))
                {
                    counter++;
                    l2.Remove(e);
                }
            }
            if (counter == l1.Count)
            {
                Console.WriteLine("yes");
            }
            Console.ReadLine();
        }

        private static void Dfs2(Edge2 edge2, StringBuilder sb, List<string> list)
        {
            sb.Append(edge2.Value);
            if (edge2.Childs.Count == 1)
            {
                Dfs2(edge2.Childs.First(),sb,list);
            }
            else
            {
                list.Add(sb.ToString());
                foreach (var v in edge2.Childs)
                {
                    var sb2 = new StringBuilder();
                    Dfs2(v,sb2,list);
                }
            }
        }

        public static void Dfs(Edge edge,List<string> l)
        {
            Console.WriteLine(new string(edge.ValueList));
            l.Add(new string(edge.ValueList));
            foreach (var c in edge.Childs)
            {
                Dfs(c,l);
            }
        }
    }
}