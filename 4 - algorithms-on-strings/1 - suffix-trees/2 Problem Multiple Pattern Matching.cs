using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Vertice
    {
        public int Id;
        public char Value;
        public List<Vertice> Childs;

        public Vertice(int id,char c)
        {
            Id = id;
            Value = c;
            Childs = new List<Vertice>();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var text = input;

            input = Console.ReadLine();
            var countPatterns = int.Parse(input);
            var arrPatterns = new string[countPatterns];
            for (int i = 0; i < countPatterns; i++)
            {
                arrPatterns[i] = Console.ReadLine();
            }

            var root = CreateSuffixTree(arrPatterns);
            var suffixPositions = GetSuffixPositions(text, root);

            Console.WriteLine(suffixPositions);
        }

        private static string GetSuffixPositions(string text, Vertice root)
        {
            var textArr = text.ToCharArray();
            var sb = new StringBuilder();

            for (int i = 0; i < textArr.Length; i++)
            {
                Vertice vertice = root;
                var nextIndex = i;

                while (vertice != null)
                {
                    Vertice nextVertice = null;
                    if (vertice.Childs.Count == 0)
                    {
                        sb.Append(string.Format("{0} ", i));
                        break;
                    }
                    else
                    {
                        if (nextIndex < textArr.Length)
                        {
                            nextVertice = vertice.Childs.FirstOrDefault(c => c.Value == textArr[nextIndex]);
                        }
                    }
                    vertice = nextVertice;
                    nextIndex++;
                }
            }

            return sb.ToString();
        }
        private static Vertice CreateSuffixTree(string[] arrPatterns)
        {
            var root = new Vertice(0, 'r');
            var counter = 1;

            for (int i = 0; i < arrPatterns.Length; i++)
            {
                var element = root;

                foreach (var c in arrPatterns[i])
                {
                    var searchElement = element.Childs.FirstOrDefault(x => x.Value == c);
                    if (searchElement == null)
                    {
                        var newEdge = new Vertice(counter, c);
                        element.Childs.Add(newEdge);
                        element = newEdge;
                        counter++;
                    }
                    else
                    {
                        element = searchElement;
                    }
                }
            }

            return root;
        }
    }
}