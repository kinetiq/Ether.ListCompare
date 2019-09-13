using System;
using System.Collections.Generic;

namespace ListCompare.Builder
{
    /// <summary>
    /// Part of the fluent interface for setting up comparisons.
    /// </summary>
    public class HeterogeneousBuilder<TLeft, TRight>
    {
        protected internal IEnumerable<TLeft> Left;
        protected internal IEnumerable<TRight> Right;

        public HeterogeneousBuilder(IEnumerable<TLeft> left, IEnumerable<TRight> right)
        {
            Right = right;
            Left = left;
        }

        /// <summary>
        /// Provides an easy way to compare list of differing types, as long a both lists contain an integer key, and
        /// that key is unique within each respective list. In other words, 2 can appear in both lists, but it can only
        /// appear within each list once.
        /// </summary>
        /// <param name="leftKeySelector">Expression specifying the key field for the left-hand list.</param>
        /// <param name="rightKeySelector">Expression specifying the key field for the right-hand list.</param>
        public UniqueIntKeyBuilder<TLeft, TRight> ByIntegerKey(Func<TLeft, int> leftKeySelector, Func<TRight, int> rightKeySelector)
        {
            return new UniqueIntKeyBuilder<TLeft, TRight>(Left, leftKeySelector, Right, rightKeySelector);
        } 
    }
}