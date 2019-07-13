using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var inputArr = input.ToCharArray();
            var inputLength = inputArr.Length;

            var bucket = new CountingSort();
            for (int i = 0; i < inputLength; i++)
            {
                bucket.Add(inputArr[i]);
            }

            var inputSortedArr = bucket.GetSortedArray();

            var firstInner = bucket.FirstInner;
            var dictionaryCorresponding = new Dictionary<int, int>();
            for (int i = 0; i < inputLength; i++)
            {
                var c = inputArr[i];
                dictionaryCorresponding.Add(i, firstInner[c]);
                firstInner[c] += 1;
            }

            var start = 0;
            var arr = new char[inputLength];
            for (int i = inputLength; i > 0; i--)
            {
                arr[i - 1] = inputSortedArr[start];
                start = dictionaryCorresponding[start];
            }

            Console.WriteLine(new string(arr));
            //Console.ReadLine();
        }
    }

    class CountingSort
    {
        private int[] _storage;

        private int _counter;

        public Dictionary<char, int> FirstInner;

        public CountingSort()
        {
            _storage = new int[255];
            _counter = 0;
            for (int i = 0; i < 255; i++)
            {
                _storage[i] = 0;
            }
        }
        public void Add(char c)
        {
            _storage[(int)c] += 1;
            _counter++;
        }

        public char[] GetSortedArray()
        {
            FirstInner = new Dictionary<char, int>();
            var resArr = new char[_counter];
            var car = 0;
            for (int i = 0; i < 255; i++)
            {
                for (int j = 0; j < _storage[i]; j++)
                {
                    var c = (char)i;
                    resArr[car] = c;
                    if (!FirstInner.ContainsKey(c))
                    {
                        FirstInner.Add(c, car);
                    }
                    car++;
                }
            }
            return resArr;
        }
    }
}