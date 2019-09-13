using System.Collections.Generic;
using System.Linq;
using System.Text;
using ListCompare.Builder;

namespace ListCompare
{
    public static class ListCompare
    {
        /// <summary>
        /// Configures a Comparer for cases where both lists are of the same type.
        /// </summary>
        public static HomogeneousBuilder<T> Compare<T>(IEnumerable<T> left, IEnumerable<T> right)
        {
            return new HomogeneousBuilder<T>(left, right);
        }

        /// <summary>
        /// Configures a Comparer for cases where the lists are of different types.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static HomogeneousBuilder<int> Compare(IEnumerable<int> left, IEnumerable<int> right)
        {
            return new HomogeneousBuilder<int>(left, right);
        }

        /// <summary>
        /// Configures a Comparer for cases where each list is of a different type.
        /// </summary>
        public static HeterogeneousBuilder<TLeft, TRight> Compare<TLeft, TRight>(IEnumerable<TLeft> left, IEnumerable<TRight> right)
        {
            return new HeterogeneousBuilder<TLeft, TRight>(left, right);
        }
    }
}