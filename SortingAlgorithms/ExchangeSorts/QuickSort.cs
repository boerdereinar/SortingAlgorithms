using System;
using System.Collections.Generic;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.ExchangeSorts
{
    internal class QuickSort<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        public QuickSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
        }

        protected override IEnumerator<TSource> Sort()
        {
            Sort(0, Source.Length - 1);
            for (int i = 0; i < Source.Length; i++)
                yield return Source[Indexes[i]];
        }

        private void Sort(int lo, int hi)
        {
            if (lo >= 0 && hi >= 0 && lo < hi)
            {
                int pivot = Partition(lo, hi);
                Sort(lo, pivot - 1);
                Sort(pivot + 1, hi);
            }
        }

        private int Partition(int lo, int hi)
        {
            TKey pivot = Keys[Indexes[hi]];
            int i = lo - 1;
            for (int j = lo; j <= hi; j++)
                if (Comparer.Compare(pivot, Keys[Indexes[j]]) >= 0)
                {
                    i++;
                    (Indexes[i], Indexes[j]) = (Indexes[j], Indexes[i]);
                }

            return i;
        }
    }
}