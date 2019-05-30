using System;
using System.Collections.Generic;
using System.Text;

namespace Algoritm2
{
    class Program
    {
        public class Segment
        {
            public int Id;
            public int Start;
            public int End;
            public bool Cover;
        }

        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var n = int.Parse(input);

            var listAns = new List<long>();
            var segmentsArr = new Segment[n];
            var endArr = new int[n];

            for (int i = 0; i < n; i++)
            {
                var inp = Console.ReadLine();
                var inpArr = inp.Split(' ');

                var s = new Segment();
                s.Start = int.Parse(inpArr[0]);
                s.End = int.Parse(inpArr[1]);
                s.Id = i;
                endArr[i] = s.End;
                segmentsArr[i] = s;
            }

            //Array.Sort(endArr,segmentsArr);
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (segmentsArr[i].End > segmentsArr[j].End)
                    {
                        var o = segmentsArr[i];
                        segmentsArr[i] = segmentsArr[j];
                        segmentsArr[j] = o;
                    }
                }
            }

            long point = segmentsArr[0].End;
            listAns.Add(point);

            for (int i = 1; i < segmentsArr.Length; ++i)
            {
                if (point < segmentsArr[i].Start || point > segmentsArr[i].End)
                {
                    point = segmentsArr[i].End;
                    listAns.Add(point);
                }
            }

            var sb = new StringBuilder();
            sb.Append(string.Format("{0}\n", listAns.Count));
            foreach (var e in listAns)
            {
                sb.Append(string.Format("{0} ", e));
            }
            Console.WriteLine(sb);
            Console.ReadLine();
        }
    }
}
