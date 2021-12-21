using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.HybridSorts
{
    internal class TimSort<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        private const int Run = 32;
        
        public TimSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
        }

        protected override IEnumerator<TSource> Sort()
        {
            for (int i = 0; i < Source.Length; i += Run)
                InsertionSort(i, Math.Min(i + Run - 1, Source.Length - 1));
            
            for (int size = Run; size < Source.Length; size *= 2)
                for (int start = 0; start < Source.Length; start += 2 * size)
                {
                    int middle = start + size - 1;
                    int end = Math.Min(start + 2 * size - 1, Source.Length - 1);

                    if (middle < end)
                        Merge(start, middle, end);
                }

            for (int i = 0; i < Source.Length; i++)
                yield return Source[Indexes[i]];
        }

        private void InsertionSort(int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                int temp = Indexes[i + 1];
                int j = i;
                for (; j >= start && Comparer.Compare(Keys[Indexes[j]], Keys[temp]) > 0; j--)
                    Indexes[j + 1] = Indexes[j];
                Indexes[j + 1] = temp;
            }
        }

        private void Merge(int start, int middle, int end)
        {
            int len1 = middle - start + 1;
            int len2 = end - middle;
            int[] left = Indexes[start..(middle + 1)];
            int[] right = Indexes[(middle + 1)..(end + 1)];

            int i = 0, j = 0, k = start;

            for (; i < len1 && j < len2; k++)
            {
                if (Comparer.Compare(Keys[left[i]], Keys[right[j]]) <= 0)
                    Indexes[k] = left[i++];
                else
                    Indexes[k] = right[j++];
            }

            for (; i < len1; k++, i++)
                Indexes[k] = left[i];
            for (; j < len2; k++, j++)
                Indexes[k] = right[j];
        }
    }
}