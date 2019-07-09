using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication15
{


    class Program
    {
        static int _positionLattice;
        static int _minStringStart;
        static int _minStringLength;

        class Vertice
        {
            public int Start;
            public int Length;
            public int SuffixLength;
            public List<Vertice> Childs;
            public bool IsCandidate;
            public char[] Src;

            public Vertice(int start, int length, char[] src)
            {
                Start = start;
                Length = length;
                Src = src;
                Childs = new List<Vertice>();
            }

            public string Content
            {
                get
                {
                    return new string(Src,Start,Length);
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
            var arrChar = new char[length];
            Array.Copy(firstArr, 0, arrChar, 0, firstArrLength);
            Array.Copy(secondArr, 0, arrChar, firstArrLength + 1, firstArrLength);
            arrChar[firstArrLength] = '#';
            arrChar[length - 1] = '$';

            _positionLattice = firstArrLength;
            _minStringLength = int.MaxValue;
            var root = CreateSuffixTree(arrChar);

            Dfs(root, 0);
            Console.WriteLine(new string(arrChar,_minStringStart,_minStringLength));
            Console.ReadLine();
        }

        private static Vertice CreateSuffixTree(char[] arrChar)
        {
            var root = new Vertice(0, 0, new char[0]);

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
                    var newVertice = new Vertice(currentCharTree, searchVertice.Length - (currentCharTree - searchVertice.Start), src);
                    searchVertice.Length = currentCharTree - searchVertice.Start;

                    newVertice.Childs = searchVertice.Childs;
                    searchVertice.Childs = new List<Vertice>();
                    searchVertice.Childs.Add(newVertice);
                    vertice = searchVertice;
                }
            }
        }

        static void Dfs(Vertice vertice, int suffixStart)
        {
            if (vertice.Childs.Any())
            {
                vertice.SuffixLength = suffixStart + vertice.Length;

                foreach (var child in vertice.Childs)
                {
                    Dfs(child, vertice.SuffixLength);
                }

                if (vertice.Childs.All(c => c.IsCandidate))
                {
                    vertice.IsCandidate = true;
                    if (vertice.SuffixLength < _minStringLength)
                    {
                        _minStringStart = vertice.Start - suffixStart;
                        _minStringLength = vertice.SuffixLength;
                    }
                }
            }
            else
            {
                if (vertice.Start <= _positionLattice && _positionLattice < vertice.Start + vertice.Length)
                {
                    vertice.IsCandidate = true;

                    if (vertice.Src[vertice.Start] != '#')
                    {
                        vertice.SuffixLength = suffixStart + 1;
                        if (vertice.SuffixLength < _minStringLength)
                        {
                            _minStringStart = vertice.Start - suffixStart;
                            _minStringLength = vertice.SuffixLength;
                        }
                    }
                }
            }
        }
    }
}