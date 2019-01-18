using System.Collections.Generic;
using ListCompare.Comparers;

namespace ListCompare.Builder
{
    public class HomogeneousBuilder<T>
    {
        protected internal IEnumerable<T> Left;
        protected internal IEnumerable<T> Right;
        protected internal IEqualityComparer<T> EqualityComparer;

        public HomogeneousBuilder(IEnumerable<T> left, IEnumerable<T> right, IEqualityComparer<T> equalityComparer = null)
        {
            EqualityComparer = equalityComparer;
            Right = right;
            Left = left;
        }

        public HomogeneousBuilder<T> WithEqualityComparer(IEqualityComparer<T> equalityComparer)
        {
            EqualityComparer = equalityComparer;

            return this;
        }

        public HomogenousComparer<T> Go()
        {
            return new HomogenousComparer<T>(Left, Right, EqualityComparer);
        }
    }
}