using System;
using System.Collections.Generic;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.InsertionSorts
{
    internal class PatienceSort<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        private List<Stack<int>> _deck;
        
        public PatienceSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
        }

        protected override IEnumerator<TSource> Sort()
        {
            _deck = new List<Stack<int>>();
            for (int i = 0; i < Source.Length; i++)
            {
                int index = BinarySearch(Keys[Indexes[i]]);
                if (index < 0) index = ~index;
                if (index != _deck.Count)
                    _deck[index].Push(Indexes[i]);
                else
                    _deck.Add(new Stack<int>(new[] {Indexes[i]}));
            }

            while (_deck.Count > 0)
            {
                yield return Source[_deck[0].Pop()];
                if (_deck[0].Count == 0)
                {
                    _deck[0] = _deck[^1];
                    _deck.RemoveAt(_deck.Count - 1);
                }
                Balance();
            }
        }

        private int BinarySearch(TKey key)
        {
            if (_deck.Count == 0)
                return 0;
            
            int min = 0;
            int max = _deck.Count - 1;
            while (min <= max)
            {
                int m = (min + max) / 2;
                int c = Comparer.Compare(Keys[_deck[m].Peek()], key);
                if (c == 0)
                    return m;
                if (c < 0)
                    min = m + 1;
                else
                    max = m - 1;
            }

            return ~min;
        }

        private void Balance()
        {
            int root = 0;

            while (2 * root + 1 < _deck.Count)
            {
                int left = 2 * root + 1;
                int swap = root;

                if (Comparer.Compare(Keys[_deck[swap].Peek()], Keys[_deck[left].Peek()]) > 0)
                    swap = left;
                if (left + 1 < _deck.Count && Comparer.Compare(Keys[_deck[swap].Peek()], Keys[_deck[left + 1].Peek()]) > 0)
                    swap = left + 1;
                if (swap == root)
                    return;

                (_deck[root], _deck[swap]) = (_deck[swap], _deck[root]);
                root = swap;
            }
        }
    }
}