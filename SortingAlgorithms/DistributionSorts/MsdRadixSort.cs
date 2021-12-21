using System;
using System.Collections.Generic;
using SortingAlgorithms.Enumerable;
using SortingAlgorithms.Extensions;

namespace SortingAlgorithms.DistributionSorts
{
    class MsdRadixSort<TSource, TKey> : StringIntegerEnumerableSorter<TSource, TKey>
    {
        public MsdRadixSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
        }

        protected override IEnumerator<TSource> StringSort()
        {
            CountStringSort(0, 0, Source.Length);

            for (int i = 0; i < Source.Length; i++)
                yield return Source[Indexes[i]];
        }

        private void CountStringSort(int position, int lo, int hi)
        {
            if (hi - lo <= 1)
                return;
            
            int[] buckets = new int[256 + 1];
            int[] output = new int[hi - lo];

            for (int i = lo; i < hi; i++)
            {
                int index = (Keys[Indexes[i]] as string).CharAt(position) + 1;
                buckets[index]++;
            }

            for (int i = 1; i < buckets.Length; i++)
                buckets[i] += buckets[i - 1];

            for (int i = hi - 1; i >= lo; i--)
            {
                int index = (Keys[Indexes[i]] as string).CharAt(position) + 1;
                output[--buckets[index]] = Indexes[i];
            }

            for (int i = lo; i < hi; i++)
                Indexes[i] = output[i - lo];
            
            for (int i = 1; i < buckets.Length; i++)
                CountStringSort(position + 1, buckets[i - 1], buckets[i]);
        }

        protected override IEnumerator<TSource> IntegerSort()
        {
            dynamic min = Keys[0], max = Keys[1];
            for (int i = 1; i < Keys.Length; i++)
            {
                if (Comparer.Compare(Keys[i], min) <= 0)
                    min = Keys[i];
                if (Comparer.Compare(Keys[i], max) >= 0)
                    max = Keys[i];
            }

            int n = (int)Math.Log10(max - min);
            dynamic exponent = 1;
            for (int i = 0; i < n; i++)
                exponent *= 10;
            
            CountIntegerSort(exponent, min, 0, Source.Length);

            for (int i = 0; i < Source.Length; i++)
                yield return Source[Indexes[i]];
        }

        private void CountIntegerSort(dynamic exponent, dynamic min, int lo, int hi)
        {
            if (hi - lo <= 1)
                return;

            int[] buckets = new int[10];
            int[] output = new int[hi - lo];
            
            for (int i = lo; i < hi; i++)
            {
                int index = (Keys[Indexes[i]] - min) / exponent % 10;
                buckets[index]++;
            }

            for (int i = 1; i < buckets.Length; i++)
                buckets[i] += buckets[i - 1];
            
            for (int i = hi - 1; i >= lo; i--)
            {
                int index = (Keys[Indexes[i]] - min) / exponent % 10;
                output[--buckets[index]] = Indexes[i];
            }
            
            for (int i = lo; i < hi; i++)
                Indexes[i] = output[i - lo];

            if (exponent <= 1) return;
            for (int i = 1; i < buckets.Length; i++)
                CountIntegerSort(exponent / 10, min, buckets[i - 1], buckets[i]);
        }
    }
}