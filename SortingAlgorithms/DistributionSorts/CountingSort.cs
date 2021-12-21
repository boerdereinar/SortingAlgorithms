using System;
using System.Collections.Generic;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.DistributionSorts
{
    internal class CountingSort<TSource, TKey> : IntegerEnumerableSorter<TSource, TKey>
    {
        public CountingSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
        }

        protected override IEnumerator<TSource> Sort()
        {
            dynamic min = Keys[0], max = Keys[0];
            for (int i = 1; i < Keys.Length; i++)
            {
                if (Comparer.Compare(Keys[i], min) <= 0)
                    min = Keys[i];
                if (Comparer.Compare(Keys[i], max) >= 0)
                    max = Keys[i];
            }
            
            int range = Math.Abs(max - min) + 1;
            int[] count = new int[range];

            for (int i = 0; i < Source.Length; i++)
                count[Math.Abs(Keys[i] - min)]++;

            for (int i = 1; i < range; i++)
                count[i] += count[i - 1];

            for (int i = Source.Length - 1; i >= 0; i--)
            {
                int index = Math.Abs(Keys[i] - min);
                Indexes[count[index] - 1] = i;
                count[index]--;
            }

            for (int i = 0; i < Source.Length; i++)
                yield return Source[Indexes[i]];
        }
    }
}