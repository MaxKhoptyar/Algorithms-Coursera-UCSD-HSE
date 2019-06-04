using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingToolbox
{
    class Request
    {
        public Request(int arrivalTime, int processTime)
        {
            ProcessTime = processTime;
            ArrivalTime = arrivalTime;
        }
        public int ProcessTime;
        public int ArrivalTime;
    }

    class Buffer
    {
        public Buffer(int size)
        {
            _size = size;
            _requests = new Queue<int>();
        }

        private Queue<int> _requests;
        private int _size;

        public int AddRequest(Request request)
        {
            while (_requests.Any() && request.ArrivalTime >= _requests.First())
            {
                _requests.Dequeue();
            }

            if (_requests.Count() < _size)
            {
                if (!_requests.Any())
                {
                    _requests.Enqueue(request.ArrivalTime + request.ProcessTime);
                    return request.ArrivalTime;
                }
                else
                {
                    var startTime = request.ArrivalTime;
                    if (_requests.Last() > startTime)
                    {
                        startTime = _requests.Last();
                    }
                    else if (_requests.Last() == startTime)
                    {
                        startTime = _requests.Last() + 1;
                    }

                    _requests.Enqueue(startTime + request.ProcessTime);
                    return startTime;
                }
            }
            else
            {
                return -1;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var arr = input.Split();

            var s = int.Parse(arr[0]);
            var n = int.Parse(arr[1]);

            var buffer = new Buffer(s);
            var sb = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                input = Console.ReadLine();
                arr = input.Split();

                var t = int.Parse(arr[0]);
                var p = int.Parse(arr[1]);

                var request = new Request(t, p);
                var response = buffer.AddRequest(request);

                sb.Append(string.Format("{0}\n", response));
            }
            Console.WriteLine(sb);
            Console.ReadLine();
        }
    }
}
