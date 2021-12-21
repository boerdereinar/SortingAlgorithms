using System;
using System.Collections.Generic;
using SortingAlgorithms.Enumerable;

namespace SortingAlgorithms.InsertionSorts
{
    internal class TreeSort<TSource, TKey> : EnumerableSorter<TSource, TKey>
    {
        public TreeSort(Func<TSource, TKey> keySelector, IComparer<TKey> comparer) : base(keySelector, comparer)
        {
        }

        protected override IEnumerator<TSource> Sort()
        {
            Tree tree = new Tree(Indexes[0]);
            for (int i = 1; i < Source.Length; i++)
                Insert(tree, Indexes[i]);

            Stack<Tree> stack = new Stack<Tree>();
            while (tree != null || stack.Count > 0)
            {
                while (tree != null)
                {
                    stack.Push(tree);
                    tree = tree.Left;
                }

                tree = stack.Pop();
                yield return Source[tree.Value];
                tree = tree.Right;
            }
        }

        private void Insert(Tree tree, int i)
        {
            if (Comparer.Compare(Keys[tree.Value], Keys[i]) > 0)
            {
                if (tree.Left == null)
                    tree.Left = new Tree(i);
                else
                    Insert(tree.Left, i);
            }
            else
            {
                if (tree.Right == null)
                    tree.Right = new Tree(i);
                else
                    Insert(tree.Right, i);
            }
        }

        private class Tree
        {
            public int Value { get; }
            public Tree Left { get; set; }
            public Tree Right { get; set; }

            public Tree(int value)
            {
                Value = value;
                Left = null;
                Right = null;
            }
        }
    }
}