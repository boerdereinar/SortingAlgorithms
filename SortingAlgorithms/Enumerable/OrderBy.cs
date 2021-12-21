using System;
using System.Collections.Generic;
using System.Linq;

namespace SortingAlgorithms.Enumerable
{
    public static partial class Enumerable
    {
        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            SortingAlgorithm algorithm)
        {
            return OrderBy(source, keySelector, Comparer<TKey>.Default, algorithm);
        }
        
        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer,
            SortingAlgorithm algorithm)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));
            comparer ??= Comparer<TKey>.Default;

            return new OrderedEnumerable<TSource, TKey>(source, keySelector, comparer, algorithm);
        }
    }
}