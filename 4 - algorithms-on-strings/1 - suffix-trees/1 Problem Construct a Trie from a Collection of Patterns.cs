using System;
using System.Collections.Generic;
using System.Linq;

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
            var n = int.Parse(input);

            var arrPatterns = new string[n];
            for (int i = 0; i < n; i++)
            {
                arrPatterns[i] = Console.ReadLine();
            }

            var root = CreateSuffixTree(arrPatterns);

            foreach (var e in root.Childs)
            {
                Draw(root, e);
            }
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

        static void Draw(Vertice start, Vertice end)
        {
            Console.WriteLine(string.Format("{0}->{1}:{2}", start.Id, end.Id, end.Value));
            foreach (var e in end.Childs)
            {
                Draw(end, e);
            }
        }
    }
}
