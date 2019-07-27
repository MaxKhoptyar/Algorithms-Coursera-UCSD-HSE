using System;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var pattern = Console.ReadLine();
            var text = Console.ReadLine();

            var patternArr = pattern.ToCharArray();
            var patternArrLength = patternArr.Length;
            var textArr = text.ToCharArray();
            var textArrLen = textArr.Length;
            var length = patternArrLength + textArrLen + 1;

            var arr = new char[length];
            var arrPrefix = new int[length];
            for (int i = 0; i < patternArrLength; i++)
            {
                arr[i] = patternArr[i];
            }

            arr[patternArrLength] = '$';
            for (int i = 0; i < textArrLen; i++)
            {
                arr[i + patternArrLength + 1] = textArr[i];
            }

            arrPrefix[0] = 0;
            var border = 0;
            for (int i = 1; i < length; i++)
            {
                while ((border > 0) && (arr[i] != arr[border]))
                {
                    border = arrPrefix[border - 1];
                }

                if (arr[i] == arr[border])
                {
                    border = border + 1;
                }
                else
                {
                    border = 0;
                }

                arrPrefix[i] = border;
            }

            var sb = new StringBuilder();
            for (int i = patternArrLength; i < length; i++)
            {
                if (arrPrefix[i] == patternArrLength)
                {
                    sb.Append(String.Format("{0} ", i - 2 * patternArrLength));
                }
            }

            Console.WriteLine(sb);
            Console.ReadLine();
        }
    }
}