using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingToolbox
{
    class Threads
    {
        public int Id;
        public int FreeTime;
    }

    class PriorityQueue
    {
        private List<Threads> _threads;

        public PriorityQueue(int countThreads)
        {
            _threads = new List<Threads>();
            for (int i = 0; i < countThreads; i++)
            {
                _threads.Add(new Threads() { Id = i, FreeTime = 0 });
            }
        }

        public Threads GetFreeThread()
        {
            return _threads.First();
        }

        public void PlaneTask(int timeToComplete)
        {
            var freeThread = _threads.First();
            _threads.RemoveAt(0);

            var planeTimeComplete = freeThread.FreeTime + timeToComplete;
            freeThread.FreeTime = planeTimeComplete;

            var start = 0;
            var end = _threads.Count;
            while (end > start)
            {
                var mid = (start + end) / 2;
                if (_threads[mid].FreeTime > planeTimeComplete)
                {
                    end = mid;
                }
                else
                {
                    start = mid + 1;
                }
            }

            _threads.Insert(start, freeThread);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var arr = input.Split();

            var n = int.Parse(arr[0]);
            var m = int.Parse(arr[1]);

            input = Console.ReadLine();
            arr = input.Split();

            var priorityQueue = new PriorityQueue(n);
            var sb = new StringBuilder();
            for (int i = 0; i < m; i++)
            {
                var t = int.Parse(arr[i]);
                var thread = priorityQueue.GetFreeThread();

                sb.Append(string.Format("{0} {1}\n", thread.Id, thread.FreeTime));

                priorityQueue.PlaneTask(t);

            }
            Console.WriteLine(sb);
            Console.ReadLine();
        }
    }
}
