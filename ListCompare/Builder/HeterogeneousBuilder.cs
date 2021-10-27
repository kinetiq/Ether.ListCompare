using System;
using System.Collections.Generic;

namespace ListComparison.Builder
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
        /// Compare lists of differing types, using the specified fields on both sides as the key.
        /// The values from both sides must be of TCommonType, and must be unique.
        /// </summary>
        /// <typeparam name="TCommonType">Type that both TLeft and TRight provide. For instance, TLeft and TRight could both be different types that each have a key of this type.</typeparam>
        /// <param name="leftKeySelector">Expression specifying the key field for the left-hand list.</param>
        /// <param name="rightKeySelector">Expression specifying the key field for the right-hand list.</param>
        public UniqueGenericKeyBuilder<TCommonType, TLeft, TRight> ByKey<TCommonType>(Func<TLeft, TCommonType> leftKeySelector, Func<TRight, TCommonType> rightKeySelector)
        {
            return new UniqueGenericKeyBuilder<TCommonType, TLeft, TRight>(Left, leftKeySelector, Right, rightKeySelector);
        }

        /// <summary>
        /// Compare a list of differing types, as long a both lists contain an integer key, and
        /// the key is unique within each respective list. In other words, 2 can appear in both lists, but it can only
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