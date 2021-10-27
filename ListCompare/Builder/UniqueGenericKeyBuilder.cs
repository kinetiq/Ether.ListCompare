using System;
using System.Collections.Generic;
using ListComparison.Comparers;

namespace ListComparison.Builder
{
    /// <summary>
    /// Part of the fluent interface for setting up comparisons.
    /// </summary>
    public class UniqueGenericKeyBuilder<TCommon, TLeft, TRight>
    {
        protected internal IEnumerable<TLeft> Left;
        protected internal IEnumerable<TRight> Right;
        protected internal Func<TLeft, TCommon> LeftKeySelector;
        protected internal Func<TRight, TCommon> RightKeySelector;

        public UniqueGenericKeyBuilder(IEnumerable<TLeft> left, Func<TLeft, TCommon> leftKeySelector, IEnumerable<TRight> right, Func<TRight, TCommon> rightKeySelector)
        {
            RightKeySelector = rightKeySelector;
            LeftKeySelector = leftKeySelector;
            Right = right;
            Left = left;
        }

        /// <summary>
        /// Returns the fully configured Comparer.
        /// </summary>
        public UniqueGenericKeyComparer<TCommon, TLeft, TRight> Go()
        {
            return new UniqueGenericKeyComparer<TCommon, TLeft, TRight>(Left, LeftKeySelector, Right, RightKeySelector);
        }
    }
}