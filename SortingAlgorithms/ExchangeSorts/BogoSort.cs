using System;
using System.Collections.Generic;
using System.Linq;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.ExchangeSorts
{
    internal class BogoSort<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        public BogoSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
            
        }

        protected override IEnumerator<TSource> Sort()
        {
            Random r = new Random(42);

            while (!IsSorted())
                Indexes = Indexes.OrderBy(x => r.Next()).ToArray();

            for (int i = 0; i < Indexes.Length; i++)
                yield return Source[Indexes[i]];
        }

        private bool IsSorted()
        {
            for (int i = 0; i < Indexes.Length - 1; i++)
                if (Comparer.Compare(Keys[Indexes[i]], Keys[Indexes[i + 1]]) > 0)
                    return false;
            return true;
        }
    }
}