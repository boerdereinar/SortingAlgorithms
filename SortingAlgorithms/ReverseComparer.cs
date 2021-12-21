using System.Collections.Generic;

namespace SortingAlgorithms
{
    internal class ReverseComparer<T> : IComparer<T>
    {
        internal readonly IComparer<T> Comparer;
        
        internal ReverseComparer(IComparer<T> comparer)
        {
            Comparer = comparer;
        }
        
        public int Compare(T x, T y)
        {
            return -1 * Comparer.Compare(x, y);
        }
    }
}