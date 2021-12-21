using System;
using System.Collections.Generic;
using System.Linq;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.SelectionSorts
{
    internal class TournamentSort<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        public TournamentSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
        }

        protected override IEnumerator<TSource> Sort()
        {
            Stack<Tree> trees = new Stack<Tree>(Indexes.Select(x => new Tree(x)));
            while (trees.Count > 0)
            {
                Tree tree = PlayTournament(trees);
                yield return Source[tree.Value];
                trees = tree.SubForest;
            }
        }

        private Tree PlayTournament(Stack<Tree> trees)
        {
            while (trees.Count > 1)
                trees = PlayRound(trees);
            return trees.Pop();
        }

        private Stack<Tree> PlayRound(Stack<Tree> trees)
        {
            Stack<Tree> result = new Stack<Tree>();
            while (trees.Count > 0)
                result.Push(trees.Count == 1 ? trees.Pop() : PlayGame(trees.Pop(), trees.Pop()));
            return result;
        }

        private Tree PlayGame(Tree tree1, Tree tree2)
        {
            if (Comparer.Compare(Keys[tree2.Value], Keys[tree1.Value]) >= 0)
                return Promote(tree1, tree2);
            return Promote(tree2, tree1);
        }

        private Tree Promote(Tree winner, Tree loser)
        {
            winner.SubForest.Push(loser);
            return winner;
        }

        private readonly struct Tree
        {
            public int Value { get; }
            public Stack<Tree> SubForest { get; }

            public Tree(int value)
            {
                Value = value;
                SubForest = new Stack<Tree>();
            }
        }
    }
}