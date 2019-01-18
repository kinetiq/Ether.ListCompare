using System.Collections.Generic;
using System.Linq;

namespace ListCompare.Comparer
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

    /// <summary>
    /// Helper for comparing two lists of the same type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HomogenousComparer<T>
    {
        public List<T> Left;
        public List<T> Right;
        public IEqualityComparer<T> Comparer;

        /// <param name="left">Left-hand side of the comparison.</param>
        /// <param name="right">Right-hand side of the comparison.</param>
        /// <param name="comparer">Note that we use a lot of LINQ expressions, and they 
        /// use GetHashCode, and only hit Equals if there's a HashCode conflict. So, you can just 
        /// return 0 from GetHashCode.
        /// </param>
        public HomogenousComparer(IEnumerable<T> left,
                           IEnumerable<T> right,
                           IEqualityComparer<T> comparer = null)
        {
            if (comparer == null)
                comparer = EqualityComparer<T>.Default;

            Comparer = comparer;
            Left = left.Distinct(Comparer)
                       .ToList();

            Right = right.Distinct(Comparer)
                         .ToList();
        }

        /// <summary>
        /// Get items that appear in both lists.
        /// </summary>
        public List<T> CommonItems()
        {
            return Left.Intersect(Right, Comparer)
                       .ToList();
        }

        /// <summary>
        /// Get items that only appear in one list. 
        /// </summary>
        public List<T> MissingItems()
        {
            return Left.Union(Right, Comparer)
                       .Except(Left.Intersect(Right, Comparer), Comparer)
                       .ToList();
        }

        /// <summary>
        /// Get items that are in the Right list, but not the Left list.
        /// </summary>
        public List<T> MissingFromLeft()
        {
            return Right.Except(Left, Comparer)
                        .ToList();
        }

        /// <summary>
        /// Get items that are in the Left list, but not the Right list.
        /// </summary>
        public List<T> MissingFromRight()
        {
            return Left.Except(Right, Comparer).ToList();
        }
    }
}