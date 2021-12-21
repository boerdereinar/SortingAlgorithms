using System;
using System.Collections.Generic;
using System.Linq;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.MergeSorts
{
    internal class StrandSort<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        public StrandSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
        }

        protected override IEnumerator<TSource> Sort()
        {
            LinkedList<int> indexes = new LinkedList<int>(Indexes);
            List<int> sorted = new List<int>(0);

            while (indexes.First != null)
            {
                List<int> temp = new List<int>(Source.Length) { indexes.First.Value };
                indexes.RemoveFirst();

                for (LinkedListNode<int> n = indexes.First; n != null; n = n.Next)
                {
                    if (Comparer.Compare(Keys[n.Value], Keys[temp.Last()]) > 0)
                    {
                        temp.Add(n.Value);
                        indexes.Remove(n);
                    }
                }

                sorted = Merge(sorted, temp);
            }

            for (int i = 0; i < Source.Length; i++)
                yield return Source[sorted[i]];
        }

        private List<int> Merge(List<int> a, List<int> b)
        {
            int n = a.Count + b.Count;
            List<int> merged = new List<int>(n);

            for (int i = 0, j = 0; i + j < n;)
            {
                if (j == b.Count || i < a.Count && Comparer.Compare(Keys[b[j]], Keys[a[i]]) > 0)
                    merged.Add(a[i++]);
                else
                    merged.Add(b[j++]);
            }

            return merged;
        }
    }
}