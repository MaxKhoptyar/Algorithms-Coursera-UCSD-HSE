using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8
{
    class Program
    {
        public static int[] State;

        public static List<int>[] Graph;

        public static Stack<int> Stack;

        public static HashSet<int> Enter; 

        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var arr = input.Split();

            var edges = int.Parse(arr[0]);
            var verticies = int.Parse(arr[1]);

            Stack = new Stack<int>();
            Graph = new List<int>[edges];
            State = new int[edges];
            Enter = new HashSet<int>();

            for (int i = 0; i < edges; i++)
            {
                Graph[i] = new List<int>();
                State[i] = 0;
                Enter.Add(i);
            }

            for (int i = 0; i < verticies; i++)
            {
                input = Console.ReadLine();
                arr = input.Split();

                var edge1 = int.Parse(arr[0]) - 1;
                var edge2 = int.Parse(arr[1]) - 1;

                Graph[edge1].Add(edge2);
            }

            while (Enter.Count != 0)
            {
                var edge = Enter.First();
                Enter.Remove(edge);

                Dfs(edge);
            }

            var sb = new StringBuilder();
            while (Stack.Count != 0)
            {
                sb.Append(string.Format("{0} ", Stack.Pop()));
            }
            Console.WriteLine(sb);
        }

        public static void Dfs(int edge)
        {
            State[edge] = 1;
            foreach (var e in Graph[edge])
            {
                if (State[e] == 2)
                {
                    continue;
                }
                Enter.Remove(e);
                Dfs(e);
            }
            Stack.Push(edge + 1);
            State[edge] = 2;
        }
    }
}
