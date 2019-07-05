using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication15
{


    class Program
    {
        static char[] _arr;
        static int _positionLattice;
        static string _minString = null;
        static int _minStringLength = int.MaxValue;
        class Edge
        {
            public int Id;
            public int Start;
            public int Length;
            public List<Edge> Childs;
            public bool IsCandidate;
            public string StrContent;
            public string Content
            {
                get
                {
                    return new string(_arr, Start, Length);
                }
            }
        }

        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var firstArr = input.ToCharArray();
            var firstArrLength = firstArr.Length;
            input = Console.ReadLine();
            var secondArr = input.ToCharArray();
            var secondArrLength = secondArr.Length;

            var length = firstArrLength + secondArrLength + 2;
            _arr = new char[length];
            for (int i = 0; i < firstArrLength; i++)
            {
                _arr[i] = firstArr[i];
            }
            _arr[firstArrLength] = '#';
            for (int i = 0; i < secondArrLength; i++)
            {
                _arr[i + 1 + firstArrLength] = secondArr[i];
            }
            _arr[length - 1] = '$';
            _positionLattice = firstArrLength;
            var root = new Edge();
            root.Childs = new List<Edge>();
            var counter = 0;
            var arrayEdges = new Edge[10000];
            for (int i = length - 1; i >= 0; i--)
            {
                var curentEdge = root;

                for (int j = i; j < length; j++)
                {
                    Edge searchElement = null;
                    var c = _arr[j];
                    foreach (var v in curentEdge.Childs)
                    {
                        if (_arr[v.Start] == c)
                        {
                            searchElement = v;
                            break;
                        }
                    }
                    if (searchElement == null)
                    {
                        var newEdge = new Edge();
                        newEdge.Length = length - j;
                        newEdge.Start = j;
                        newEdge.Childs = new List<Edge>();
                        newEdge.Id = counter;
                        curentEdge.Childs.Add(newEdge);
                        arrayEdges[newEdge.Id] = newEdge;
                        counter++;
                        break;
                    }
                    else
                    {

                        var minL = Math.Min(searchElement.Length, length - j);
                        var copyL = 1;
                        for (int k = 1; k < minL; k++)
                        {
                            if (_arr[searchElement.Start + k] == _arr[j + k])
                            {
                                copyL++;
                            }
                            else
                            {
                                break;
                            }
                        }

                        if (copyL < searchElement.Length)
                        {
                            var newEdge = new Edge();

                            newEdge.Start = searchElement.Start + copyL;
                            newEdge.Length = searchElement.Length - copyL;
                            newEdge.Childs = searchElement.Childs;
                            newEdge.Id = counter;
                            arrayEdges[newEdge.Id] = newEdge;
                            counter++;

                            searchElement.Childs = new List<Edge>();

                            searchElement.Childs.Add(newEdge);
                            searchElement.Length = copyL;
                            searchElement.Start = j;

                        }
                        j = j + copyL - 1;
                        curentEdge = searchElement;
                    }
                }
            }
            Dfs(root, "");
            Console.WriteLine(_minString);
        }


        static void Dfs(Edge edge, string str)
        {
            var count = edge.Childs.Count;
            var sb = new StringBuilder();
            sb.Append(str);
            if (count > 0)
            {
                sb.Append(_arr, edge.Start, edge.Length);
                edge.StrContent = sb.ToString();
                var counter = 0;
                foreach (var child in edge.Childs)
                {
                    Dfs(child, edge.StrContent);
                    if (child.IsCandidate)
                    {
                        counter++;
                    }
                }

                if (counter == count)
                {
                    edge.IsCandidate = true;
                    var l = edge.StrContent.Length;
                    if (l > 0 && l < _minStringLength)
                    {
                        _minString = edge.StrContent;
                        _minStringLength = l;
                    }
                }
            }
            else
            {
                if (edge.Start <= _positionLattice && _positionLattice < edge.Start + edge.Length)
                {
                    edge.IsCandidate = true;

                    if (_arr[edge.Start] != '#')
                    {
                        sb.Append(_arr, edge.Start, 1);
                        edge.StrContent = sb.ToString();
                        var l = sb.Length;
                        if (l > 0 && l < _minStringLength)
                        {
                            _minString = edge.StrContent;
                            _minStringLength = l;
                        }
                    }
                }
            }
        }
    }
}