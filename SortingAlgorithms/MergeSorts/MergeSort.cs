using System;
using System.Collections.Generic;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.MergeSorts
{
    internal class MergeSort<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        public MergeSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
        }

        protected override IEnumerator<TSource> Sort()
        {
            int[] clone = (int[]) Indexes.Clone();
            SplitMerge(Indexes, clone, 0,  Source.Length);

            for (int i = 0; i < Source.Length; i++)
                yield return Source[Indexes[i]];
        }

        private void SplitMerge(int[] a, int[] b, int begin, int end)
        {
            if (end - begin <= 1) return;

            int middle = (begin + end) / 2;
            SplitMerge(b, a, begin, middle);
            SplitMerge(b, a, middle, end);
            
            Merge(b, a, begin, middle, end);
        }

        private void Merge(int[] a, int[] b, int begin, int middle, int end)
        {
            int i = begin;
            int j = middle;

            for (int k = begin; k < end; k++)
            {
                if (i < middle && (j >= end || Comparer.Compare(Keys[a[i]], Keys[a[j]]) <= 0))
                    b[k] = a[i++];
                else
                    b[k] = a[j++];
            }
        }
    }
}