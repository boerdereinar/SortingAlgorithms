using System;
using System.Collections.Generic;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.InsertionSorts
{
    internal class ShellSort<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        private readonly int[] _gaps = {701, 301, 132, 57, 23, 10, 4, 1};
        
        public ShellSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
        }

        protected override IEnumerator<TSource> Sort()
        {
            foreach (int gap in _gaps)
                for (int i = gap; i < Source.Length; i++)
                {
                    int temp = Indexes[i];
                    int j = i;
                    for (; j >= gap && Comparer.Compare(Keys[Indexes[j - gap]], Keys[temp]) > 0; j -= gap)
                        Indexes[j] = Indexes[j - gap];
                    Indexes[j] = temp;
                }

            for (int i = 0; i < Source.Length; i++)
                yield return Source[Indexes[i]];
        }
    }
}