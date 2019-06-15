using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StructureToolbox
{
    class Program
    {
        public class TreeElement
        {
            public int ParentIn;
            public int Data;
            public int LeftIn;
            public int RightIn;
            public TreeElement LeftEl;
            public TreeElement RightEl;
        }


        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var m = int.Parse(input);

            var arr = new TreeElement[m];
            for (int i = 0; i < m; i++)
            {
                input = Console.ReadLine();
                var splitted = input.Split();

                TreeElement elemnent;
                if (arr[i] != null)
                {
                    elemnent = arr[i];
                }
                else
                {
                    elemnent = new TreeElement();
                }

                elemnent.Data = int.Parse(splitted[0]);
                var leftIn = int.Parse(splitted[1]);
                elemnent.LeftIn = leftIn;
                var rightIn = int.Parse(splitted[2]);
                elemnent.RightIn = rightIn;
                if (leftIn > 0)
                {
                    var leftEl = new TreeElement();
                    elemnent.LeftEl = leftEl;
                    arr[leftIn] = leftEl;
                }
                if (rightIn > 0)
                {
                    var rightEl = new TreeElement();
                    elemnent.RightEl = rightEl;
                    arr[rightIn] = rightEl;
                }
                arr[i] = elemnent;
            }

            var isCorrect = true;
            if (arr.Length > 0)
            {
                isCorrect = IsBstUtil(arr[0], int.MinValue, int.MaxValue);
            }
            if (isCorrect)
            {
                Console.WriteLine("CORRECT");
            }
            else
            {
                Console.WriteLine("INCORRECT");
            }
        }

        public static bool IsBstUtil(TreeElement element, int min, int max)
        {
            if (element == null)
            {
                return true;
            }
            if (element.Data < min || element.Data > max)
            {
                return false;
            }
            bool left = true;
            bool right = true;

            return IsBstUtil(element.LeftEl, min, element.Data - 1) && IsBstUtil(element.RightEl, element.Data + 1, max);
        }
    }
}
