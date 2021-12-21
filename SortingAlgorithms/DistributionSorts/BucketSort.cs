using System;
using System.Collections.Generic;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.DistributionSorts
{
    internal class BucketSort<TSource, TKey> : NumericEnumerableSorter<TSource, TKey>
    {
        private const int N = 10;
        
        public BucketSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
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
            double r = (max - min) / (double) N;
            long range = (long) (r < 0 ? Math.Floor(r) : Math.Ceiling(r));
            
            List<int>[] buckets = new List<int>[N];
            for (int i = 0; i < N; i++)
                buckets[i] = new List<int>();
            
            for (int i = 0; i < Source.Length; i++)
                buckets[(int) (Keys[i] - min) / range].Add(i);

            for (int i = 0; i < N; i++)
            {
                InsertionSort(buckets[i]);
                for (int j = 0; j < buckets[i].Count; j++)
                    yield return Source[buckets[i][j]];
            }
        }

        private void InsertionSort(List<int> a)
        {
            for (int i = 1; i < a.Count; i++)
            {
                int index = a[i];
                int j = i - 1;
                for (; j >= 0 && Comparer.Compare(Keys[a[j]], Keys[index]) > 0; j--)
                    a[j + 1] = a[j];
                a[j + 1] = index;
            }
        }
    }
}