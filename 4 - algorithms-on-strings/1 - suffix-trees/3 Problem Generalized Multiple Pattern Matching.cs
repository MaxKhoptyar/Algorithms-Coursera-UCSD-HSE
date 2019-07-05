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
        public bool IsEndPattern;
        public List<Vertice> Childs;

        public Vertice(int id,char c,bool isEndPattern = false)
        {
            Id = id;
            Value = c;
            IsEndPattern = isEndPattern;
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

            var root =  CreateSuffixTree(arrPatterns);
            var suffixPositions = GetSuffixPositions(text,root);

            Console.WriteLine(suffixPositions);
        }

        private static string GetSuffixPositions(string text, Vertice root)
        {
            var sb = new StringBuilder();
            var textArr = text.ToCharArray();

            for (int i = 0; i < textArr.Length; i++)
            {
                Vertice vertice = root;

                var nextIndex = i;
                while (vertice != null)
                {
                    Vertice nextVertice = null;
                    if (vertice.IsEndPattern)
                    {
                        sb.Append(String.Format("{0} ", i));
                        break;
                    }
                    if (nextIndex < textArr.Length)
                    {
                        nextVertice = vertice.Childs.FirstOrDefault(c => c.Value == textArr[nextIndex]);
                    }
                    nextIndex++;
                    vertice = nextVertice;
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
                var vertice = root;
                var inputArr = arrPatterns[i].ToCharArray();
                for (int j = 0; j < inputArr.Length; j++)
                {
                    var c = inputArr[j];
                    var searchElement = vertice.Childs.FirstOrDefault(x => x.Value == c);
                    if (searchElement == null)
                    {
                        var newVertice = new Vertice(counter,c);
                        counter++;
                        vertice.Childs.Add(newVertice);
                        if (j == inputArr.Length - 1)
                        {
                            newVertice.IsEndPattern = true;
                        }
                        vertice = newVertice;
                    }
                    else
                    {
                        if (j == inputArr.Length - 1)
                        {
                            searchElement.IsEndPattern = true;
                        }
                        vertice = searchElement;
                    }
                }
            }

            return root;
        }
    }
}
