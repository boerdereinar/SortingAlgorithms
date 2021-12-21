using System.Collections.Generic;

namespace SortingAlgorithms
{
    internal class CompositeKey<TPrimary, TSecondary>
    {
        internal readonly TPrimary Primary;
        internal readonly TSecondary Secondary;

        internal CompositeKey(TPrimary primary, TSecondary secondary)
        {
            Primary = primary;
            Secondary = secondary;
        }

        internal sealed class Comparer : IComparer<CompositeKey<TPrimary, TSecondary>>
        {
            internal readonly IComparer<TPrimary> PrimaryComparer;
            internal readonly IComparer<TSecondary> SecondaryComparer;
            
            internal Comparer(IComparer<TPrimary> primaryComparer, IComparer<TSecondary> secondaryComparer)
            {
                PrimaryComparer = primaryComparer;
                SecondaryComparer = secondaryComparer;
            }
            
            public int Compare(CompositeKey<TPrimary, TSecondary> x, CompositeKey<TPrimary, TSecondary> y)
            {
                int primaryResult = PrimaryComparer.Compare(x.Primary, y.Primary);
                return primaryResult != 0 ? primaryResult : SecondaryComparer.Compare(x.Secondary, y.Secondary);
            }
        }
    }
}