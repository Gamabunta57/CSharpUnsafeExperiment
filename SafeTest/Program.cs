using System;
using System.Diagnostics;
using UnsafeTest;

namespace SafeTest
{
    class Program
    {
        const int size = 10000;
        static Stopwatch watch;
        static void Main(string[] args)
        {
            watch = Stopwatch.StartNew();
            var safeInteger = new SafeStdArray(size);
            watch.Stop();
            Console.WriteLine($"Measure alloc safe:  {watch.Elapsed.TotalMilliseconds}");

            watch = Stopwatch.StartNew();
            var uInteger = new UnsafeInteger(size);
            watch.Stop();
            Console.WriteLine($"Measure alloc:  {watch.Elapsed.TotalMilliseconds}");

            watch = Stopwatch.StartNew();
            for (var i = 0; i < 1000000; i++)
                MeasureSafe(ref safeInteger);
            watch.Stop();
            Console.WriteLine($"Measure safe: {watch.Elapsed.TotalMilliseconds}");

            watch = Stopwatch.StartNew();
            for (var i = 0; i < 1000000; i++)
                Measure(ref uInteger);
            watch.Stop();
            Console.WriteLine($"Measure: {watch.Elapsed.TotalMilliseconds}");
        }

        static void Measure(ref UnsafeInteger uInteger)
        {
            for (var i = 0; i < size; i++)
            {
                var val = uInteger.getValue(i);
                val.Value = i;
            }
        }
        static void MeasureSafe(ref SafeStdArray safeArray)
        {
            for (var i = 0; i < size; i++)
                safeArray.array[i] = i;
        }
    }

    struct SafeStdArray
    {
        public int[] array;

        public SafeStdArray(int size)
        {
            array = new int[size];
        }
    }
}
