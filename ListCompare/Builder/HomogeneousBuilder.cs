using System.Collections.Generic;
using ListCompare.Comparers;

namespace ListCompare.Builder
{
    /// <summary>
    /// Part of the fluent interface for setting up comparisons.
    /// </summary>
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

        /// <summary>
        /// Allows you to specify an IEqualityComparer, which can change the way objects are compared.
        /// </summary>
        public HomogeneousBuilder<T> WithEqualityComparer(IEqualityComparer<T> equalityComparer)
        {
            EqualityComparer = equalityComparer;

            return this;
        }

        /// <summary>
        /// Returns the fully configured Comparer.
        /// </summary>
        public HomogenousComparer<T> Go()
        {
            return new HomogenousComparer<T>(Left, Right, EqualityComparer);
        }
    }
}