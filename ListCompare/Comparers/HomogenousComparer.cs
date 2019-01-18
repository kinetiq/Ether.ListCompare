using System.Collections.Generic;
using System.Linq;

namespace ListCompare.Comparers
{
    /// <summary>
    /// Helper for comparing two lists of the same type.
    /// </summary>
    /// <typeparam name="T">Type to compare.</typeparam>
    public class HomogenousComparer<T>
    {
        public IEnumerable<T> Left;
        public IEnumerable<T> Right;
        public IEqualityComparer<T> Comparer;

        /// <param name="left">Left-hand side of the comparison.</param>
        /// <param name="right">Right-hand side of the comparison.</param>
        /// <param name="comparer">Specifying an IEqualityComparer enables quite a few advanced scenarios, but hopefully we will
        /// end up with easier solutions for them.</param>
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