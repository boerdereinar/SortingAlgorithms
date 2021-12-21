using System;
using System.Collections.Generic;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.ExchangeSorts
{
    internal class ExchangeSort<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        public ExchangeSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
        }

        protected override IEnumerator<TSource> Sort()
        {
            for (int i = 0; i < Source.Length - 1; i++)
                for (int j = i + 1; j < Source.Length; j++)
                    if (Comparer.Compare(Keys[Indexes[i]], Keys[Indexes[j]]) > 0)
                        (Indexes[i], Indexes[j]) = (Indexes[j], Indexes[i]);

            for (int i = 0; i < Source.Length; i++)
                yield return Source[Indexes[i]];
        }
    }
}