using System;
using System.Collections.Generic;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.ExchangeSorts
{
    internal class BubbleSort<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        public BubbleSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
        }

        protected override IEnumerator<TSource> Sort()
        {
            int n = Source.Length;
            while (n > 1)
            {
                int newN = 0;
                for (int i = 1; i < n; i++)
                    if (Comparer.Compare(Keys[Indexes[i - 1]], Keys[Indexes[i]]) > 0)
                    {
                        (Indexes[i - 1], Indexes[i]) = (Indexes[i], Indexes[i - 1]);
                        newN = i;
                    }

                n = newN;
            }

            for (int i = 0; i < Source.Length; i++)
                yield return Source[Indexes[i]];
        }
    }
}