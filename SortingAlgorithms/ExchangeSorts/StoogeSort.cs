using System;
using System.Collections.Generic;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.ExchangeSorts
{
    internal class StoogeSort<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        public StoogeSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
        }

        protected override IEnumerator<TSource> Sort()
        {
            Sort(0, Source.Length - 1);
            for (int i = 0; i < Source.Length; i++)
                yield return Source[Indexes[i]];
        }

        private void Sort(int i, int j)
        {
            if (Comparer.Compare(Keys[Indexes[i]], Keys[Indexes[j]]) > 0)
                (Indexes[i], Indexes[j]) = (Indexes[j], Indexes[i]);
            if (j - i + 1 > 2)
            {
                int t = (j - i + 1) / 3;
                Sort(i, j - t);
                Sort(i + t, j);
                Sort(i, j - t);
            }
        }
    }
}