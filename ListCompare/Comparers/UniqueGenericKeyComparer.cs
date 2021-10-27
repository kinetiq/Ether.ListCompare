using System;
using System.Collections.Generic;
using System.Linq;
using ListComparison.Extensions;

namespace ListComparison.Comparers
{
    /// <summary>
    /// List comparer for heterogeneous (different) types that have a common unique key.
    /// This will throw if a key appears more than once on either side. 
    /// </summary>
    /// <typeparam name="TLeft">Type of the left-hand list</typeparam>
    /// <typeparam name="TRight">Type of the right-hand list</typeparam>
    /// <typeparam name="TCommon">Type that both TLeft and TRight provide. For instance, TLeft and TRight could both be different types that each have an integer key.</typeparam>
    public class UniqueGenericKeyComparer<TCommon, TLeft, TRight>
    {
        private readonly Dictionary<TCommon, TLeft> Left;
        private readonly Dictionary<TCommon, TRight> Right;
        
        private readonly HashSet<TCommon> LeftKeys;
        private readonly HashSet<TCommon> RightKeys;
 
        public UniqueGenericKeyComparer(IEnumerable<TLeft> left, Func<TLeft, TCommon> leftKeySelector, IEnumerable<TRight> right, Func<TRight, TCommon> rightKeySelector)
        {
            // Convert our lists into dictionaries for quick key/value lookup. 
            Right = right.ToDictionary(rightKeySelector);
            Left = left.ToDictionary(leftKeySelector);
           
            // Also, turn our keys into HashSets for (in theory) fast intersect/except.
            LeftKeys = new HashSet<TCommon>(Left.Keys);
            RightKeys = new HashSet<TCommon>(Right.Keys);
        }

        /// <summary>
        /// Returns true if there are no missing items in either list.
        /// </summary>
        public bool NoMissingItems()
        {
            return (MissingItems().Count == 0);
        }

        /// <summary>
        /// Get items that appear in both lists. Returns the versions from both lists as a CommonItem.
        /// </summary>
        public List<CommonItem<TLeft, TRight>> CommonItems()
        {
            var commonKeys = LeftKeys.Intersect(RightKeys)
                .ToList();

            var result = new List<CommonItem<TLeft, TRight>>();

            foreach (var key in commonKeys)
            {
                var item = new CommonItem<TLeft, TRight>(Left[key], Right[key]);

                result.Add(item);
            }


            return result;
        }

        /// <summary>
        /// Get items that appear in both lists. Because each list is of a different
        /// type, this specifies returning all items as the type of the left-hand list.
        /// </summary>
        public List<TLeft> CommonItemsAsLeft()
        {          
            var commonKeys = LeftKeys.Intersect(RightKeys)
                .ToList();

            return Left.FindValuesFor(commonKeys);
        }

        /// <summary>
        /// Get items that appear in both lists. Because each list is of a different
        /// type, this specifies returning all items as the type of the right-hand list.
        /// </summary>
        public List<TRight> CommonItemsAsRight()
        {
            var commonKeys = LeftKeys.Intersect(RightKeys)
                .ToList();

            return Right.FindValuesFor(commonKeys);
        }

        /// <summary>
        /// Get items that are in the Right list, but not the Left list.
        /// </summary>
        public List<TRight> MissingFromLeft()
        {
            var missingKeys = RightKeys.Except(LeftKeys)
                .ToList();

            var result = new List<TRight>();

            foreach(var key in missingKeys)
                result.Add(Right[key]);

            return result;
        }

        /// <summary>
        /// Get items that are in the Left list, but not the Right list.
        /// </summary>
        public List<TLeft> MissingFromRight()
        {
            var missingKeys = LeftKeys.Except(RightKeys)
                .ToList();

            var result = new List<TLeft>();

            foreach (var key in missingKeys)
                result.Add(Left[key]);

            return result; 
        }

        /// <summary>
        /// Get items that only appear in one list. Because each list is of a different
        /// type, and this can return types from both lists, this returns a List of Object.
        /// </summary>
        public List<object> MissingItems()
        {
            var commonKeys = LeftKeys.Union(RightKeys)
                .Except(LeftKeys.Intersect(RightKeys))
                .ToList();

            return SearchDictionaries(commonKeys);
        }

        /// <summary>
        /// Search for keys in both the left and the right list, return anything we find.
        /// </summary>
        private List<object> SearchDictionaries(List<TCommon> keys)
        {
            var result = new List<object>();

            foreach (var key in keys)
            {
                if (Left.ContainsKey(key))
                    result.Add(Left[key]);

                if (Right.ContainsKey(key))
                    result.Add(Right[key]);
            }

            return result;
        }
    }
}