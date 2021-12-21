using System;
using System.Collections.Generic;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.ExchangeSorts
{
    internal class CocktailShakerSort<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        public CocktailShakerSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
        }

        protected override IEnumerator<TSource> Sort()
        {
            bool swapped;
            do
            {
                swapped = false;
                for (int i = 0; i < Source.Length - 1; i++)
                    if (Comparer.Compare(Keys[Indexes[i]], Keys[Indexes[i + 1]]) > 0)
                    {
                        (Indexes[i], Indexes[i + 1]) = (Indexes[i + 1], Indexes[i]);
                        swapped = true;
                    }
                if (!swapped)
                    break;

                swapped = false;
                for (int i = Source.Length - 2; i >= 0; i--)
                    if (Comparer.Compare(Keys[Indexes[i]], Keys[Indexes[i + 1]]) > 0)
                    {
                        (Indexes[i], Indexes[i + 1]) = (Indexes[i + 1], Indexes[i]);
                        swapped = true;
                    }
            } while (swapped);

            for (int i = 0; i < Source.Length; i++)
                yield return Source[Indexes[i]];
        }
    }
}