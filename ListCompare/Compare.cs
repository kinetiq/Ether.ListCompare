using System.Collections.Generic;
using System.Linq;
using System.Text;
using ListCompare.Builder;

namespace ListCompare
{
    // Make a version where both are the same. Allow an IEqualityComparer.
    // There are a couple of options: (1) create our own dynamic IEqualityComparer. 
    //                                ... yeah that ain't bad. 
    // Comparison.ListsOf<T>(left, right)
    //               .WithEqualityComparer(comparer) 
    //               .Go();
    //
    // Comparison.LeftList<Left>(left, x => x.ID)
    //           .RightList<Right>(right, x => x.ID)

    //        // var lc = ListCompare.Compare<X>().
    //                     .HavingKey(x => x.ID)
    //                     .Against<Z>().
    //                     .HavingKey(x => x.ID)
    //                     .Go();

     
    public static class Compare
    {
        public static HomogeneousBuilder<T> Lists<T>(IEnumerable<T> left, IEnumerable<T> right)
        {
            return new HomogeneousBuilder<T>(left, right);
        }

        public static HeterogeneousBuilder<TLeft, TRight> Lists<TLeft, TRight>(IEnumerable<TLeft> left, IEnumerable<TRight> right)
        {
            return new HeterogeneousBuilder<TLeft, TRight>(left, right);
        }
    }
}
