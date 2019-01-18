using System;
using System.Collections.Generic;

namespace ListCompare.Builder
{
    public class HeterogeneousBuilder<TLeft, TRight>
    {
        protected internal IList<TLeft> Left;
        protected internal IList<TRight> Right;

        public HeterogeneousBuilder(IList<TLeft> left, IList<TRight> right)
        {
            Right = right;
            Left = left;
        }

        public UniqueIntKeyBuilder<TLeft, TRight> WithUniqueIntegerKey(Func<TLeft, int> leftKeySelector, Func<TRight, int> rightKeySelector)
        {
            return new UniqueIntKeyBuilder<TLeft, TRight>(Left, leftKeySelector, Right, rightKeySelector);
        } 
    }
}