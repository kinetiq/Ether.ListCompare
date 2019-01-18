using System.Collections.Generic;
using System.Linq;
using System.Text;
using ListCompare.Builder;

namespace ListCompare
{

    // API brainstorming

    // var lc = Compare.Lists(list1, list2)
    //                 .WithKeys.Left(left => left.ID)
    //                          .Right(right => right.ID)
    //                 .Go();

    // var lc = Compare.Lists(list1, list2)
    //                 .Go();


    public static class Compare
    {
        /// <summary>
        /// Configures a Comparer for cases where both lists are of the same type.
        /// </summary>
        public static HomogeneousBuilder<T> Lists<T>(IEnumerable<T> left, IEnumerable<T> right)
        {
            return new HomogeneousBuilder<T>(left, right);
        }

        /// <summary>
        /// Configures a Comparer for cases where each list is of a different type.
        /// </summary>
        public static HeterogeneousBuilder<TLeft, TRight> Lists<TLeft, TRight>(IEnumerable<TLeft> left, IEnumerable<TRight> right)
        {
            return new HeterogeneousBuilder<TLeft, TRight>(left, right);
        }
    }
}
