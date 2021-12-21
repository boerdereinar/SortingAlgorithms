using System;
using System.Collections.Generic;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.ExchangeSorts
{
    internal class CombSort<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        public CombSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
        }

        protected override IEnumerator<TSource> Sort()
        {
            int gap = Source.Length;
            double shrink = 1.3;
            bool sorted = false;

            while (!sorted)
            {
                gap = (int) (gap / shrink);
                if (gap <= 1)
                {
                    gap = 1;
                    sorted = true;
                }

                for (int i = 0; i + gap < Source.Length; i++)
                    if (Comparer.Compare(Keys[Indexes[i]], Keys[Indexes[i + gap]]) > 0)
                    {
                        (Indexes[i], Indexes[i + gap]) = (Indexes[i + gap], Indexes[i]);
                        sorted = false;
                    }
            }

            for (int i = 0; i < Source.Length; i++)
                yield return Source[Indexes[i]];
        }
    }
}