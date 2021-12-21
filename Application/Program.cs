using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SortingAlgorithms;
using SortingAlgorithms.Enumerable;
using Enumerable = System.Linq.Enumerable;

namespace Application
{
    class Program
    {
        static void Main()
        {
            Random r = new Random(42);
            IEnumerable<int> toSort = Enumerable.Range(-100, 200).OrderBy(x => r.Next());
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            IEnumerable<string> toSortString = alphabet.SelectMany(x => alphabet.Select(y => x.ToString() + y)).OrderBy(x => r.Next());

            Stopwatch stopwatch = Stopwatch.StartNew();

            int[] sorted = toSort.OrderBy(x => x, SortingAlgorithm.MsdRadixSort).ToArray();

            stopwatch.Stop();
            Console.WriteLine($"Sorting took: {stopwatch.Elapsed}");
            Console.WriteLine(string.Join(", ", sorted));

            /*bool isSorted = true;
            for (int i = 0; i < sorted.Length - 1; i++)
                isSorted = isSorted && sorted[i] < sorted[i + 1];
            
            Console.WriteLine(isSorted);*/
        }
    }
}