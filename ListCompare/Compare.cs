using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
