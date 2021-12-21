using System;
using System.Collections.Generic;
using System.Linq;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.DistributionSorts
{
    internal class BurstSort<TSource, TKey> : StringEnumerableSorter<TSource, TKey>
    {
        private const int BucketInitialSize = 4;
        private const int BucketThreshold = 32;
        private const int BucketGrowthFactor = 4;
        
        public BurstSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
        }

        protected override IEnumerator<TSource> Sort()
        {
            Trie trie = new Trie(Keys.Cast<string>().ToArray());
            trie.Insert(Indexes);
            trie.Traverse();
            
            yield return Source[0];
        }

        private class Trie
        {
            private readonly string[] _keys; 
            private readonly Bucket[] _buckets;
            private readonly int _depth;
            
            internal Trie(string[] keys, int depth = 0)
            {
                _keys = keys;
                _buckets = new Bucket[27];
                _depth = depth;
            }

            internal void Insert(int[] indexes)
            {
                foreach (int i in indexes)
                    Insert(i);
            }

            internal void Insert(int index)
            {
                int i = CharAtIndex(index, _depth);

                _buckets[i] ??= new Bucket(_keys, _depth);
                _buckets[i].Insert(index);
            }

            internal void Traverse()
            {
                foreach (Bucket bucket in _buckets)
                    bucket?.Traverse();
            }

            private int CharAtIndex(int keyIndex, int index)
            {
                string key = _keys[keyIndex];
                if (index >= key.Length || !char.IsLetter(key[index]))
                    return 0;
                return char.ToUpperInvariant(key[index]) - 'A' + 1;
            }
        }

        private class Bucket
        {
            private readonly string[] _keys;
            private readonly int _depth;
            
            private int[] _indexes;
            private int _pointer;
            private Trie _root;
            
            internal Bucket(string[] keys, int depth)
            {
                _keys = keys;
                _depth = depth;
                _indexes = new int[BucketInitialSize];
                _pointer = 0;
                _root = null;
            }

            internal void Insert(int index)
            {
                if (_pointer < _indexes.Length)
                {
                    _indexes[_pointer++] = index;
                }
                else if (_indexes.Length < BucketThreshold)
                {
                    Array.Resize(ref _indexes, _indexes.Length * BucketGrowthFactor);
                    _indexes[_pointer++] = index;
                }
                else
                {
                    _root = new Trie(_keys);
                    _root.Insert(_indexes);
                }
            }

            internal void Traverse()
            {
                if (_root != null)
                    _root.Traverse();
                else
                    Sort(0, _pointer - 1, _depth);
            }

            private void Sort(int lo, int hi, int d)
            {
                if (hi - lo < 1 || d > 1)
                    return;
                
                (int i, int j) = Partition(lo, hi, d);
                
                Sort(lo, i - 1, d);
                Sort(i, j, d + 1);
                Sort(j + 1, hi, d);
            }

            private (int, int) Partition(int lo, int hi, int d)
            {
                int i = 0, j = 0, k = hi - lo;

                int p = CharAtIndex(lo, d);
                while (j <= k)
                {
                    int c = CharAtIndex(j, d);
                    if (c < p)
                        (_indexes[i], _indexes[j]) = (_indexes[j++], _indexes[i++]);
                    else if (c > p)
                        (_indexes[j], _indexes[k]) = (_indexes[k--], _indexes[j]);
                    else
                        j++;
                }

                return (i, j);
            }
            
            private int CharAtIndex(int index, int charIndex)
            {
                string key = _keys[_indexes[index]];
                if (charIndex >= key.Length || !char.IsLetter(key[charIndex]))
                    return 0;
                return char.ToUpperInvariant(key[charIndex]) - 'A' + 1;
            }
        }
    }
}