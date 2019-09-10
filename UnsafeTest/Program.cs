using System;
using System.Diagnostics;

namespace UnsafeTest
{
    class Program
    {
        const int size = 1000;
        static UnsafeInteger uInteger = new UnsafeInteger(size);
        static void Main(string[] args)
        {
            var watch = Stopwatch.StartNew();
            watch.Restart();
            for (var i = 0; i < 100000; i++)
                Measure();
            watch.Stop();
            Console.WriteLine($"Measure: {watch.Elapsed.TotalMilliseconds}");
        }

        static void Measure()
        {
            for (var i = 0; i < size; i++)
            {
                var val = uInteger.getValue(i);
                val.Value = i;
            }
        }
    }
}
