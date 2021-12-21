using System;
using System.Collections.Generic;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.DistributionSorts
{
    class LsdRadixSort<TSource, TKey> : IntegerEnumerableSorter<TSource, TKey>
    {
        public LsdRadixSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
        }

        protected override IEnumerator<TSource> Sort()
        {
            dynamic min = Keys[0], max = Keys[1];
            for (int i = 1; i < Keys.Length; i++)
            {
                if (Comparer.Compare(Keys[i], min) <= 0)
                    min = Keys[i];
                if (Comparer.Compare(Keys[i], max) >= 0)
                    max = Keys[i];
            }

            for (dynamic exponent = 1; (max - min) / exponent >= 1; exponent *= 10)
                CountSort(exponent, min);

            for (int i = 0; i < Source.Length; i++)
                yield return Source[Indexes[i]];
        }

        private void CountSort(dynamic exponent, dynamic min)
        {
            int[] buckets = new int[10];
            int[] output = new int[Source.Length];

            for (int i = 0; i < Source.Length; i++)
            {
                int index = (Keys[Indexes[i]] - min) / exponent % 10;
                buckets[index]++;
            }

            for (int i = 1; i < 10; i++)
                buckets[i] += buckets[i - 1];

            for (int i = Source.Length - 1; i >= 0; i--)
            {
                int index = ((dynamic)Keys[Indexes[i]] - min) / exponent % 10;
                output[--buckets[index]] = Indexes[i];
            }

            Indexes = output;
        }
    }
}