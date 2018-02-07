using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SilverAtom
{
    class Test
    {
        static void Main(string[] args)
        {
            Console.WriteLine("All positive numbers are consecutive");
            Console.WriteLine("All negative numbers are consecutive");

            var atomic = new Atomic();
            var list = new List<int>();
            var tasks = new List<Task<int>>();
            var range = Enumerable.Range(0, 100);

            foreach (var number in range)
            {
                var key = "abc" + (number % 2 == 0 ? "1" : "-1");

                var task = atomic.Enqueue(key, () =>
                {
                    if (number % 19 == 1)
                    {
                        Task.Delay(1000).Wait();

                        list.Add(number * (number % 2 == 0 ? 1 : -1));
                    }
                    else
                    {
                        list.Add(number * (number % 2 == 0 ? 1 : -1));
                    }

                    return number;
                });

                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
    }
}
