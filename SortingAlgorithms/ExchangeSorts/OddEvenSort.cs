using System;
using System.Collections.Generic;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.ExchangeSorts
{
    internal class OddEvenSort<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        public OddEvenSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
        }

        protected override IEnumerator<TSource> Sort()
        {
            bool sorted = false;
            while (!sorted)
            {
                sorted = true;
                for (int i = 1; i < Source.Length - 1; i += 2)
                    if (Comparer.Compare(Keys[Indexes[i]], Keys[Indexes[i + 1]]) > 0)
                    {
                        (Indexes[i], Indexes[i + 1]) = (Indexes[i + 1], Indexes[i]);
                        sorted = false;
                    }
                
                for (int i = 0; i < Source.Length - 1; i += 2)
                    if (Comparer.Compare(Keys[Indexes[i]], Keys[Indexes[i + 1]]) > 0)
                    {
                        (Indexes[i], Indexes[i + 1]) = (Indexes[i + 1], Indexes[i]);
                        sorted = false;
                    }
            }

            for (int i = 0; i < Source.Length; i++)
                yield return Source[Indexes[i]];
        }
    }
}