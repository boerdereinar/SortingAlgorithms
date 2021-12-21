using System;
using System.Collections.Generic;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.DistributionSorts
{
    internal class PigeonHoleSort<TSource, TKey> : IntegerEnumerableSorter<TSource, TKey>
    {
        public PigeonHoleSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
        }

        protected override IEnumerator<TSource> Sort()
        {
            dynamic min = Keys[0], max = Keys[0];
            for (int i = 1; i < Keys.Length; i++)
            {
                if (Comparer.Compare(Keys[i], min) <= 0)
                    min = Keys[i];
                if (Comparer.Compare(Keys[i], max) >= 0)
                    max = Keys[i];
            }

            int range = Math.Abs(max - min) + 1;
            List<int>[] pigeonHoles = new List<int>[range];
            for (int i = 0; i < range; i++)
                pigeonHoles[i] = new List<int>();

            for (int i = 0; i < Source.Length; i++)
                pigeonHoles[Math.Abs(Keys[i] - min)].Add(i);
            
            for (int i = 0; i < range; i++)
                foreach (int j in pigeonHoles[i])
                    yield return Source[j];
        }
    }
}