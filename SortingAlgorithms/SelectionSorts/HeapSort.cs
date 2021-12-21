using System;
using System.Collections.Generic;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.SelectionSorts
{
    internal class HeapSort<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        public HeapSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
        }

        protected override IEnumerator<TSource> Sort()
        {
            Heapify();

            for (int i = Source.Length - 1; i > 0;)
            {
                yield return Source[Indexes[0]];
                Indexes[0] = Indexes[i];
                i--;
                SiftDown(0, i);
            }
        }

        private void Heapify()
        {
            for (int i = (Source.Length - 2) / 2; i >= 0; i--)
                SiftDown(i, Source.Length - 1);
        }

        private void SiftDown(int i, int j)
        {
            int root = i;

            while (2 * root + 1 <= j)
            {
                int left = 2 * root + 1;
                int swap = root;

                if (Comparer.Compare(Keys[Indexes[swap]], Keys[Indexes[left]]) > 0)
                    swap = left;
                if (left + 1 <= j && Comparer.Compare(Keys[Indexes[swap]], Keys[Indexes[left + 1]]) > 0)
                    swap = left + 1;
                if (swap == root)
                    return;

                (Indexes[root], Indexes[swap]) = (Indexes[swap], Indexes[root]);
                root = swap;
            }
        }
    }
}