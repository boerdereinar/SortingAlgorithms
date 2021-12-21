using System;
using System.Collections.Generic;
using System.Linq;
using SortingAlgorithms.Extensions;

namespace SortingAlgorithms.Enumerable
{
    internal abstract class EnumerableSorter<TSource, TKey>
    {
        protected readonly Func<TSource, TKey> KeySelector;
        protected readonly IComparer<TKey> Comparer;

        protected TSource[] Source;
        protected TKey[] Keys;
        protected int[] Indexes;

        internal EnumerableSorter(Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            KeySelector = keySelector;
            Comparer = comparer;
        }
        
        internal IEnumerator<TSource> Sort(IEnumerable<TSource> source)
        {
            Source = source.ToArray();
            if (Source.Length <= 1)
                return (IEnumerator<TSource>)Source.GetEnumerator();
            
            GenerateKeys();
            GenerateIndexes();
            
            return Sort();
        }

        private void GenerateKeys()
        {
            Keys = new TKey[Source.Length];
            for (int i = 0; i < Source.Length; i++)
                Keys[i] = KeySelector(Source[i]);
        }

        private void GenerateIndexes()
        {
            Indexes = new int[Source.Length];
            for (int i = 0; i < Source.Length; i++)
                Indexes[i] = i;
        }

        protected abstract IEnumerator<TSource> Sort();
    }
    
    internal abstract class NumericEnumerableSorter<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        protected NumericEnumerableSorter(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
            if (!typeof(TKey).IsNumeric())
                throw new ArgumentException();
        }
    }

    internal abstract class IntegerEnumerableSorter<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        protected IntegerEnumerableSorter(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
            if (!typeof(TKey).IsInteger())
                throw new ArgumentException();
        }
    }

    internal abstract class StringEnumerableSorter<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        protected StringEnumerableSorter(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
            if (typeof(TKey) != typeof(string))
                throw new ArgumentException();
        }
    }

    internal abstract class StringIntegerEnumerableSorter<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        protected StringIntegerEnumerableSorter(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
            if (typeof(TKey) != typeof(string) && !typeof(TKey).IsInteger())
                throw new ArgumentException();
        }

        protected sealed override IEnumerator<TSource> Sort()
        {
            return typeof(TKey) == typeof(string) ? StringSort() : IntegerSort();
        }

        protected abstract IEnumerator<TSource> StringSort();

        protected abstract IEnumerator<TSource> IntegerSort();
    }
}