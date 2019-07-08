using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Vertice
    {
        public int Start;
        public int Length;
        public char[] Src;
        public List<Vertice> Childs;

        public Vertice(int start, int length, char[] src)
        {
            Start = start;
            Length = length;
            Src = src;
            Childs = new List<Vertice>();
        }

        public string Value
        {
            get
            {
                return new string(Src, Start, Length);
            }
        }
    }

    class Program
    {
        static char[] Arr;

        static void Main(string[] args)
        {
            var input = Console.ReadLine();

            var root = CreateSuffixTree(input);

            var listVertices = root.Childs;
            var sb = new StringBuilder();
            while (listVertices.Any())
            {
                var newListVertices = new List<Vertice>();

                foreach (var v in listVertices)
                {
                    sb.Append(string.Format("{0}\n", v.Value));
                    foreach (var c in v.Childs)
                    {
                        newListVertices.Add(c);
                    }
                }

                listVertices = newListVertices;
            }
            Console.WriteLine(sb);
            //Console.ReadLine();
        }

        private static Vertice CreateSuffixTree(string input)
        {
            var root = new Vertice(0, 0, new char[0]);

            var arrChar = input.ToCharArray();

            for (int i = 0; i < arrChar.Length; i++)
            {
                AddToSuffixTree(root, arrChar, i, arrChar.Length - i);
            }

            return root;
        }

        private static void AddToSuffixTree(Vertice root, char[] src, int start, int length)
        {
            var vertice = root;
            var currentCharPattern = start;
            var lastCharPattern = start + length;

            while (currentCharPattern < lastCharPattern)
            {
                var searchVertice = vertice.Childs.FirstOrDefault(x => src[currentCharPattern] == x.Src[x.Start]);

                if (searchVertice == null)
                {
                    var newVertice = new Vertice(currentCharPattern, length - (currentCharPattern - start), src);
                    vertice.Childs.Add(newVertice);
                    return;
                }

                var currentCharTree = searchVertice.Start;
                var lastCharTree = currentCharTree + searchVertice.Length;

                while (currentCharTree < lastCharTree && currentCharPattern < lastCharPattern)
                {
                    if (src[currentCharPattern] == searchVertice.Src[currentCharTree])
                    {
                        currentCharTree++;
                        currentCharPattern++;
                        continue;
                    }
                    break;
                }

                if (currentCharTree == lastCharTree && currentCharPattern < lastCharPattern)
                {
                    vertice = searchVertice;
                }
                else
                {
                    var newVertice = new Vertice(currentCharTree, searchVertice.Length - currentCharTree - searchVertice.Start, src);
                    searchVertice.Length = currentCharTree - searchVertice.Start;

                    newVertice.Childs = searchVertice.Childs;
                    searchVertice.Childs = new List<Vertice>();
                    searchVertice.Childs.Add(newVertice);
                    vertice = newVertice;
                }
            }
        }
    }
}