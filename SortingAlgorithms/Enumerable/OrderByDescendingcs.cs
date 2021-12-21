using System;
using System.Collections.Generic;
using System.Linq;

namespace SortingAlgorithms.Enumerable
{
    public static partial class Enumerable
    {
        public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            SortingAlgorithm algorithm)
        {
            return OrderByDescending(source, keySelector, Comparer<TKey>.Default, algorithm);
        }
        
        public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer,
            SortingAlgorithm algorithm)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));
            comparer = new ReverseComparer<TKey>(comparer ?? Comparer<TKey>.Default);

            return new OrderedEnumerable<TSource, TKey>(source, keySelector, comparer, algorithm);
        }
    }
}