using System;
using System.Collections.Generic;
using ListCompare.Comparers;

namespace ListCompare.Builder
{
    /// <summary>
    /// Part of the fluent interface for setting up comparisons.
    /// </summary>
    public class UniqueIntKeyBuilder<TLeft, TRight>
    {
        protected internal IEnumerable<TLeft> Left;
        protected internal IEnumerable<TRight> Right;
        protected internal Func<TLeft, int> LeftKeySelector;
        protected internal Func<TRight, int> RightKeySelector;

        public UniqueIntKeyBuilder(IEnumerable<TLeft> left, Func<TLeft, int> leftKeySelector, IEnumerable<TRight> right, Func<TRight, int> rightKeySelector)
        {
            RightKeySelector = rightKeySelector;
            LeftKeySelector = leftKeySelector;
            Right = right;
            Left = left;
        }

        /// <summary>
        /// Returns the fully configured Comparer.
        /// </summary>
        public UniqueIntKeyComparer<TLeft, TRight> Go()
        {
            return new UniqueIntKeyComparer<TLeft, TRight>(Left, LeftKeySelector, Right, RightKeySelector);
        }
    }
}