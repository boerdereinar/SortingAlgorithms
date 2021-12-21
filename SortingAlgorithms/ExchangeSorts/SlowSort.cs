using System;
using System.Collections.Generic;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.ExchangeSorts
{
    internal class SlowSort<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        public SlowSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
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
            if (i >= j)
                return;

            int m = (i + j) / 2;
            Sort(i, m);
            Sort(m + 1, j);
            if (Comparer.Compare(Keys[Indexes[m]], Keys[Indexes[j]]) > 0)
                (Indexes[j], Indexes[m]) = (Indexes[m], Indexes[j]);
            Sort(i, j - 1);
        }
    }
}