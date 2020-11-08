using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication25
{
    class Edge
    {
        public int Id;
        public int Time;
        public Vertice StartVertice;
        public Vertice EndVertice;
    }

    class Vertice
    {
        public int[] CodeChars;
        public char[] Chars;
        public int StartString;
        public int LengthString;
        //public string Str { get { return new string(Chars, StartString, LengthString); } }
        public List<Edge> Edges;
        public int Id;
        public int Degree;
        public HashSet<Vertice> CountInnerEdges;
        public HashSet<Vertice> CountOuterEdges;

        public Vertice()
        {
            CountOuterEdges = new HashSet<Vertice>();
            CountInnerEdges = new HashSet<Vertice>();
        }
    }

    class Diapason
    {
        public int Start;
        public int End;
    }

    class Program
    {
        static void Main(string[] args)
        {

            var input = Console.ReadLine();
            var arr = input.Split();
            var k = int.Parse(arr[0]);
            var t = int.Parse(arr[1]);


            var s = new List<string>();
            while (!string.IsNullOrEmpty(input = Console.ReadLine()))
            {
                s.Add(input);
            }

            var patternCount = s.Count;
            var patternArr = new char[patternCount][];

            var li = 0;
            foreach (var e in s)
            {
                patternArr[li] = e.ToCharArray();
                li++;
            }
            ////////////
            //var random = new Random();
            //var nucleodsArr = new char[] { 'T', 'C', 'A', 'G' };
            //var patternCount = 200;
            //var patternLength = 100;
            //var k = 8;
            //var t = 12;
            //var patternArr = new char[patternCount][];
            //for (int i = 0; i < patternCount; i++)
            //{
            //    patternArr[i] = new char[patternLength];
            //    for (int j = 0; j < patternLength; j++)
            //    {
            //        var next = random.Next(nucleodsArr.Length);
            //        patternArr[i][j] = nucleodsArr[next];
            //    }
            //}
            /////////////
            //patternArr = new char[][]
            //{
            //                new[] {'A', 'G', 'T', 'C'}, new[] {'G', 'T', 'C', 'T'},
            //                new[] {'T', 'A', 'G', 'A'} ,new[] {'T', 'C', 'T', 'A'}
            //};
            //            patternArr = new char[][]
            //{
            //                            new[] {'A', 'A', 'C', 'G'}, new[] {'A', 'C', 'G', 'T'},
            //                            new[] {'C', 'A', 'A', 'C'} ,new[] {'G', 'T', 'T', 'G'},new[] {'T', 'G', 'C', 'A'},
            //};
            //var d1 = DateTime.Now;


            var kmerStorage = GetKmerStorageWithEdges(patternArr, k - 1, patternCount);

            var validOut = new List<Vertice>();
            var validIn = new HashSet<Vertice>();
            foreach (var e in kmerStorage.SortedElementsList)
            {
                if (e.CountOuterEdges.Count > 1)
                {
                    validOut.Add(e);
                }
                if (e.CountInnerEdges.Count > 1)
                {
                    validIn.Add(e);
                }
            }

            var bubbleCounts = 0;
            foreach (var v in validOut)
            {
                var traverse = GetTraverse(v, kmerStorage.SortedElementArray.Length, validIn, t);

                foreach (var l in traverse)
                {
                    if (l.Value.SortedVerticeses.Count > 1)
                    {
                        var localListSet = new List<HashSet<Vertice>>();
                        var c = 0;
                        foreach (var e in l.Value.SortedVerticeses)
                        {
                            if (e.First() == e.Last())
                            {
                                continue;
                            }
                            var set = new HashSet<Vertice>();
                            for (int i = 1; i < e.Length - 1; i++)
                            {
                                set.Add(e[i]);
                            }

                            foreach (var localHash in localListSet)
                            {
                                var add = 1;
                                if (localHash.Any(x => set.Contains(x)))
                                {
                                    add = 0;
                                }
                                bubbleCounts += add;
                            }
                            localListSet.Add(set);
                        }
                    }
                }
            }
            Console.WriteLine(bubbleCounts);
            //Console.WriteLine((DateTime.Now - d1).TotalMilliseconds);
            Console.ReadLine();
        }

        private static Dictionary<Vertice, SortedVerticeList> GetTraverse(Vertice v, int verticeCount, HashSet<Vertice> validIn, int maxT)
        {
            var list = new List<Vertice>();
            var finalPoints = new Dictionary<Vertice, SortedVerticeList>();
            var visited = new HashSet<Vertice>();
            Dfs(v, visited, list, validIn, finalPoints, 0, maxT);
            return finalPoints;
        }

        private static void Dfs(Vertice vertice, HashSet<Vertice> set, List<Vertice> list, HashSet<Vertice> validIn, Dictionary<Vertice, SortedVerticeList> resList, int currT, int maxT)
        {
            list.Add(vertice);
            set.Add(vertice);
            if (validIn.Contains(vertice))
            {
                if (resList.ContainsKey(vertice))
                {
                    resList[vertice].Add(list.ToArray());
                }
                else
                {
                    resList.Add(vertice, new SortedVerticeList());
                    resList[vertice].Add(list.ToArray());
                }
            }
            if (currT < maxT)
            {
                foreach (var e in vertice.Edges)
                {
                    if (e.EndVertice != vertice && !set.Contains(e.EndVertice))
                    {
                        Dfs(e.EndVertice, set, list, validIn, resList, currT + 1, maxT);
                    }
                }
            }
            set.Remove(list.Last());
            list.RemoveAt(list.Count - 1);
        }


        private static SortedList GetKmerStorageWithEdges(char[][] patternArr, int overlapLength, int patternCount)
        {
            var kmersStorage = new SortedList();
            var edges = new List<Edge>();
            var id = 0;
            for (int i = 0; i < patternCount; i++)
            {
                Vertice prevVertice = null;
                for (int j = 0; j < patternArr[i].Length - overlapLength + 1; j++)
                {
                    //var kmer = new char[overlapLength];
                    //Array.Copy(patternArr[i], j, kmer, 0, overlapLength);
                    var s = kmersStorage.Add(patternArr[i], j, overlapLength);
                    if (prevVertice != null && !prevVertice.CountOuterEdges.Contains(s))
                    {
                        var e = new Edge() { StartVertice = prevVertice, EndVertice = s, Id = id };
                        if (s != prevVertice)
                        {
                            e.StartVertice.CountOuterEdges.Add(s);
                            e.EndVertice.CountInnerEdges.Add(prevVertice);
                        }
                        prevVertice.Edges.Add(e);
                        edges.Add(e);
                        id++;
                    }
                    prevVertice = s;
                }
            }
            kmersStorage.Edges = edges;
            kmersStorage.UpdateIndexes();
            return kmersStorage;
        }

        private static int[] ToCodeChar(char[] chars)
        {
            var ints = new int[chars.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == 'A')
                {
                    ints[i] = 0;
                    continue;
                }
                if (chars[i] == 'C')
                {
                    ints[i] = 1;
                    continue;
                }
                if (chars[i] == 'G')
                {
                    ints[i] = 2;
                    continue;
                }
                ints[i] = 3;
            }
            return ints;
        }
        class SortedList
        {
            public List<Vertice> SortedElementsList = new List<Vertice>();

            public List<Edge> Edges;

            public Vertice[] SortedElementArray;

            private BWTComparer BwtComparer = new BWTComparer();

            public Vertice Add(char[] chars)
            {
                if (SortedElementsList.Count == 0)
                {
                    var s = new Vertice() { Chars = chars, CodeChars = ToCodeChar(chars), Edges = new List<Edge>() };
                    SortedElementsList.Add(s);
                    return s;
                }
                else
                {
                    var start = 0;
                    var end = SortedElementsList.Count;
                    while (end > start)
                    {
                        var n = start + ((end - start) / 2);
                        var r = BwtComparer.Compare(chars, SortedElementsList[n].Chars);
                        if (r == 1)
                        {
                            end = n;
                        }
                        if (r == -1)
                        {
                            start = n + 1;
                        }
                        if (r == 0)
                        {
                            start = n + 1;
                            //var s2 = new Vertice() { Chars = chars, CodeChars = ToCodeChar(chars), Edges = new List<Edge>() };
                            //SortedElementsList.Insert(start, s2);
                            //SortedElementsList.Insert(start, SortedElementsList[n]);
                            return SortedElementsList[n];
                        }
                    }
                    var s = new Vertice() { Chars = chars, CodeChars = ToCodeChar(chars), Edges = new List<Edge>() };
                    SortedElementsList.Insert(start, s);
                    return s;
                }
            }

            public Vertice Add(char[] chars, int startString, int lengthString)
            {
                if (SortedElementsList.Count == 0)
                {
                    var s = new Vertice() { Chars = chars, StartString = startString, LengthString = lengthString, Edges = new List<Edge>() };
                    SortedElementsList.Add(s);
                    return s;
                }
                else
                {
                    var start = 0;
                    var end = SortedElementsList.Count;
                    while (end > start)
                    {
                        var n = start + ((end - start) / 2);
                        var r = BwtComparer.Compare(chars, startString, lengthString, SortedElementsList[n]);
                        if (r == 1)
                        {
                            end = n;
                        }
                        if (r == -1)
                        {
                            start = n + 1;
                        }
                        if (r == 0)
                        {
                            start = n + 1;
                            //var s2 = new Vertice() { Chars = chars, CodeChars = ToCodeChar(chars), Edges = new List<Edge>() };
                            //SortedElementsList.Insert(start, s2);
                            //SortedElementsList.Insert(start, SortedElementsList[n]);
                            return SortedElementsList[n];
                        }
                    }
                    var s = new Vertice() { Chars = chars, StartString = startString, LengthString = lengthString, Edges = new List<Edge>() };
                    SortedElementsList.Insert(start, s);
                    return s;
                }
            }
            public void UpdateIndexes()
            {
                var c = 0;
                foreach (var e in SortedElementsList)
                {
                    e.Id = c;
                    c++;
                }
                SortedElementArray = SortedElementsList.ToArray();
            }
        }
        class SortedVerticeList
        {
            public List<Vertice[]> SortedVerticeses = new List<Vertice[]>();

            private VerticeArrayComparer VerticeArrayComparer = new VerticeArrayComparer();

            public bool Add(Vertice[] element)
            {
                if (SortedVerticeses.Count == 0)
                {
                    SortedVerticeses.Add(element);
                    return true;
                }
                else
                {
                    var start = 0;
                    var end = SortedVerticeses.Count;
                    while (end > start)
                    {
                        var n = start + ((end - start) / 2);
                        var r = VerticeArrayComparer.Compare(element, SortedVerticeses[n]);
                        if (r == 1)
                        {
                            end = n;
                        }
                        if (r == -1)
                        {
                            start = n + 1;
                        }
                        if (r == 0)
                        {
                            start = n + 1;
                            //var s2 = new Vertice() { Chars = chars, CodeChars = ToCodeChar(chars), Edges = new List<Edge>() };
                            //SortedElementsList.Insert(start, s2);
                            //SortedElementsList.Insert(start, SortedElementsList[n]);
                            return false;
                        }
                    }
                    SortedVerticeses.Insert(start, element);
                    return true;
                }
            }


        }
        class VerticeArrayComparer : IComparer<Vertice[]>
        {
            public int Compare(Vertice[] x, Vertice[] y)
            {
                var x_arr = x;
                var x_arr_l = x_arr.Length;
                var y_arr = y;
                var y_arr_l = y_arr.Length;
                var min = Math.Min(x_arr_l, y_arr_l);

                for (int i = 0; i < min; i++)
                {
                    if (x_arr[i].Id > y_arr[i].Id)
                    {
                        return -1;
                    }
                    if (x_arr[i].Id < y_arr[i].Id)
                    {
                        return 1;
                    }
                }
                if (y_arr_l == x_arr_l)
                {
                    return 0;
                }
                if (y_arr_l == min)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
        }
        class BWTComparer : IComparer<char[]>
        {
            public int Compare(char[] x, char[] y)
            {
                var x_arr = x;
                var x_arr_l = x_arr.Length;
                var y_arr = y;
                var y_arr_l = y_arr.Length;
                var min = Math.Min(x_arr_l, y_arr_l);

                for (int i = 0; i < min; i++)
                {
                    if (x_arr[i] > y_arr[i])
                    {
                        return -1;
                    }
                    if (x_arr[i] < y_arr[i])
                    {
                        return 1;
                    }
                }
                if (y_arr_l == x_arr_l)
                {
                    return 0;
                }
                if (y_arr_l == min)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }

            public int Compare(char[] chars, int startString, int lengthString, Vertice sortedElements)
            {
                var x_arr_l = lengthString;
                var y_arr_l = sortedElements.LengthString;
                var min = Math.Min(x_arr_l, y_arr_l);

                for (int i = 0; i < min; i++)
                {
                    if (chars[startString + i] > sortedElements.Chars[sortedElements.StartString + i])
                    {
                        return -1;
                    }
                    if (chars[startString + i] < sortedElements.Chars[sortedElements.StartString + i])
                    {
                        return 1;
                    }
                }
                if (y_arr_l == x_arr_l)
                {
                    return 0;
                }
                if (y_arr_l == min)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
        }
    }
}