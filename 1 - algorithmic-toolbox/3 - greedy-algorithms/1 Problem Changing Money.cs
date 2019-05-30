using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritm2
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var val = int.Parse(input);

            int sum = 0;
            var tenCount = val/10;
            sum = sum + tenCount;
            var div = val%10;
            if (div > 0)
            {
                var fiveCount = div/5;
                var divFive = div%5;
                sum += fiveCount + divFive;
            }

            Console.WriteLine(sum);
        }
    }
}
