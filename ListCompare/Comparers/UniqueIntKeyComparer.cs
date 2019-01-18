using System;
using System.Collections.Generic;
using System.Linq;
using ListCompare.Extensions;

namespace ListCompare.Comparers
{
    /// <summary>
    /// List comparer for heterogeneous (different) types that have a common unique int key.
    /// This will throw if a key appears more than once on either side. 
    /// </summary>
    /// <typeparam name="TLeft">Type of the left-hand list</typeparam>
    /// <typeparam name="TRight">Type of the right-hand list</typeparam>
    public class UniqueIntKeyComparer<TLeft, TRight>
    {
        private readonly Dictionary<int, TLeft> Left;
        private readonly Dictionary<int, TRight> Right;
        
        private readonly HashSet<int> LeftKeys;
        private readonly HashSet<int> RightKeys;
 
        public UniqueIntKeyComparer(List<TLeft> left, Func<TLeft, int> leftKeySelector, List<TRight> right, Func<TRight, int> rightKeySelector)
        {
            // Convert our lists into dictionaries for quick key/value lookup. 
            Right = right.ToDictionary(rightKeySelector);
            Left = left.ToDictionary(leftKeySelector);
           
            // Also, turn our keys into HashSets for (in theory) fast intersect/except.
            LeftKeys = new HashSet<int>(Left.Keys);
            RightKeys = new HashSet<int>(Right.Keys);
        }

        /// <summary>
        /// Get items that appear in both lists.
        /// </summary>
        public List<TLeft> CommonItemsAsLeft()
        {          
            var commonKeys = LeftKeys.Intersect(RightKeys)
                .ToList();

            return Left.FindAny(commonKeys);
        }

        /// <summary>
        /// Get items that appear in both lists.
        /// </summary>
        public List<TRight> CommonItemsAsRight()
        {
            var commonKeys = LeftKeys.Intersect(RightKeys)
                .ToList();

            return Right.FindAny(commonKeys);
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
        /// Get items that only appear in one list. 
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
        private List<object> SearchDictionaries(List<int> keys)
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