using System;
using System.Collections.Generic;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.SelectionSorts
{
    internal class SelectionSort<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        public SelectionSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
        }

        protected override IEnumerator<TSource> Sort()
        {
            for (int i = 0; i < Source.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < Source.Length; j++)
                    if (Comparer.Compare(Keys[Indexes[j]], Keys[Indexes[min]]) < 0)
                        min = j;
                
                yield return Source[Indexes[min]];
                
                if (min != i)
                    Indexes[min] = Indexes[i];
            }

            yield return Source[^1];
        }
    }
}