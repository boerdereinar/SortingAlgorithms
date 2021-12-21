using System;
using System.Collections.Generic;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.InsertionSorts
{
    internal class InsertionSort<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        public InsertionSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
            
        }

        protected override IEnumerator<TSource> Sort()
        {
            for (int i = 1; i < Source.Length; i++)
            {
                int index = Indexes[i];
                int j = i - 1;
                for (; j >= 0 && Comparer.Compare(Keys[Indexes[j]], Keys[index]) > 0; j--)
                    Indexes[j + 1] = Indexes[j];
                Indexes[j + 1] = index;
            }

            for (int i = 0; i < Source.Length; i++)
                yield return Source[Indexes[i]];
        }
    }
}