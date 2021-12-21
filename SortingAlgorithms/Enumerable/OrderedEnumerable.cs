using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SortingAlgorithms.DistributionSorts;
using SortingAlgorithms.ExchangeSorts;
using SortingAlgorithms.HybridSorts;
using SortingAlgorithms.InsertionSorts;
using SortingAlgorithms.MergeSorts;
using SortingAlgorithms.SelectionSorts;
using static SortingAlgorithms.SortingAlgorithm;

namespace SortingAlgorithms.Enumerable
{
    internal class OrderedEnumerable<TSource, TCompositeKey> : IOrderedEnumerable<TSource>
    {
        private readonly IEnumerable<TSource> _source;
        private readonly Func<TSource, TCompositeKey> _keySelector;
        private readonly IComparer<TCompositeKey> _comparer;
        private readonly SortingAlgorithm _algorithm;

        internal OrderedEnumerable(IEnumerable<TSource> source,
            Func<TSource, TCompositeKey> keySelector,
            IComparer<TCompositeKey> comparer,
            SortingAlgorithm algorithm)
        {
            _source = source;
            _keySelector = keySelector;
            _comparer = comparer;
            _algorithm = algorithm;
        }
        
        public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer, bool descending)
        {
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            comparer ??= Comparer<TKey>.Default;
            if (descending)
                comparer = new ReverseComparer<TKey>(comparer);

            CompositeKey<TCompositeKey, TKey> NewKeySelector(TSource x) =>
                new CompositeKey<TCompositeKey, TKey>(_keySelector(x), keySelector(x));

            IComparer<CompositeKey<TCompositeKey, TKey>> newComparer =
                new CompositeKey<TCompositeKey, TKey>.Comparer(_comparer, comparer);

            return new OrderedEnumerable<TSource, CompositeKey<TCompositeKey, TKey>>(_source, NewKeySelector, newComparer, _algorithm);
        }

        public IEnumerator<TSource> GetEnumerator()
        {
            EnumerableSorter<TSource, TCompositeKey> sorter = _algorithm switch
            {
                // Exchange Sorts
                BogoSort => new BogoSort<TSource, TCompositeKey>(_keySelector, _comparer),
                BubbleSort => new BubbleSort<TSource, TCompositeKey>(_keySelector, _comparer),
                CircleSort => new CircleSort<TSource, TCompositeKey>(_keySelector, _comparer),
                CocktailShakerSort => new CocktailShakerSort<TSource, TCompositeKey>(_keySelector, _comparer),
                CombSort => new CombSort<TSource, TCompositeKey>(_keySelector, _comparer),
                ExchangeSort => new ExchangeSort<TSource, TCompositeKey>(_keySelector, _comparer),
                GnomeSort => new GnomeSort<TSource, TCompositeKey>(_keySelector, _comparer),
                OddEvenSort => new OddEvenSort<TSource, TCompositeKey>(_keySelector, _comparer),
                QuickSort => new QuickSort<TSource, TCompositeKey>(_keySelector, _comparer),
                SlowSort => new SlowSort<TSource, TCompositeKey>(_keySelector, _comparer),
                StoogeSort => new StoogeSort<TSource, TCompositeKey>(_keySelector, _comparer),
                // Selection Sorts
                HeapSort => new HeapSort<TSource, TCompositeKey>(_keySelector, _comparer),
                SelectionSort => new SelectionSort<TSource, TCompositeKey>(_keySelector, _comparer),
                TournamentSort => new TournamentSort<TSource, TCompositeKey>(_keySelector, _comparer),
                // Insertion Sorts
                InsertionSort => new InsertionSort<TSource, TCompositeKey>(_keySelector, _comparer),
                PatienceSort => new PatienceSort<TSource, TCompositeKey>(_keySelector, _comparer),
                ShellSort => new ShellSort<TSource, TCompositeKey>(_keySelector, _comparer),
                TreeSort => new TreeSort<TSource, TCompositeKey>(_keySelector, _comparer),
                // Merge Sorts
                MergeSort => new MergeSort<TSource, TCompositeKey>(_keySelector, _comparer),
                StrandSort => new StrandSort<TSource, TCompositeKey>(_keySelector, _comparer),
                // Distribution Sorts
                BucketSort => new BucketSort<TSource, TCompositeKey>(_keySelector, _comparer),
                BurstSort => new BurstSort<TSource, TCompositeKey>(_keySelector, _comparer),
                CountingSort => new CountingSort<TSource, TCompositeKey>(_keySelector, _comparer),
                PigeonholeSort => new PigeonHoleSort<TSource, TCompositeKey>(_keySelector, _comparer),
                LsdRadixSort => new LsdRadixSort<TSource, TCompositeKey>(_keySelector, _comparer),
                MsdRadixSort => new MsdRadixSort<TSource, TCompositeKey>(_keySelector, _comparer),
                // Concurrent Sorts
                // Hybrid Sorts
                TimSort => new TimSort<TSource, TCompositeKey>(_keySelector, _comparer),
                // Other Sorts
                // Not Yet Implemented Sorts
                _ => throw new NotImplementedException()
            };
            return sorter.Sort(_source);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}