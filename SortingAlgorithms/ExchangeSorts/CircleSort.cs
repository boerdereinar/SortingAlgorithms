using System;
using System.Collections.Generic;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.ExchangeSorts
{
    internal class CircleSort<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        public CircleSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
        }

        protected override IEnumerator<TSource> Sort()
        {
            while (Sort(0, Source.Length - 1));
            for (int i = 0; i < Source.Length; i++)
                yield return Source[Indexes[i]];
        }

        private bool Sort(int lo, int hi)
        {
            bool swapped = false;
            if (lo == hi)
                return false;

            int i = lo, j = hi, m = (hi - lo) / 2;
            for (; i < j; i++, j--)
            {
                if (Comparer.Compare(Keys[Indexes[i]], Keys[Indexes[j]]) > 0)
                {
                    (Indexes[i], Indexes[j]) = (Indexes[j], Indexes[i]);
                    swapped = true;
                }
            }

            if (i == j && Comparer.Compare(Keys[Indexes[i]], Keys[Indexes[j + 1]]) > 0)
            {
                (Indexes[i], Indexes[j + 1]) = (Indexes[j + 1], Indexes[i]);
                swapped = true;
            }

            bool left = Sort(lo, lo + m);
            bool right = Sort(lo + m + 1, hi);

            return swapped || left || right;
        }
    }
}