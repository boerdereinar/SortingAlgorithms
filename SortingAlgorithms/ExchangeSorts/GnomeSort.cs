using System;
using System.Collections.Generic;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.ExchangeSorts
{
    internal class GnomeSort<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        public GnomeSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
        }

        protected override IEnumerator<TSource> Sort()
        {
            for (int i = 1; i < Source.Length; i++)
                for (int j = i; j > 0 && Comparer.Compare(Keys[Indexes[j - 1]], Keys[Indexes[j]]) > 0; j--)
                    (Indexes[j - 1], Indexes[j]) = (Indexes[j], Indexes[j - 1]);

            for (int i = 0; i < Source.Length; i++)
                yield return Source[Indexes[i]];
        }
    }
}