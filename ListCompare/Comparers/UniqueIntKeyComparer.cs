using System;
using System.Collections.Generic;
using System.Linq;
using ListComparison.Extensions;

namespace ListComparison.Comparers
{
    /// <summary>
    /// List comparer for heterogeneous (different) types that have a common unique int key.
    /// This will throw if a key appears more than once on either side. 
    /// </summary>
    /// <typeparam name="TLeft">Type of the left-hand list</typeparam>
    /// <typeparam name="TRight">Type of the right-hand list</typeparam>
    public class UniqueIntKeyComparer<TLeft, TRight> : UniqueGenericKeyComparer<int, TLeft, TRight>
    {
        // This is really just syntactic sugar, and barely that. It simply wraps UniqueGenericKeyComparer, and could easily be eliminated. 
        // It only exists now because it predates UniqueGenericKeyComparer and is in use.
        
        public UniqueIntKeyComparer(IEnumerable<TLeft> left, Func<TLeft, int> leftKeySelector, IEnumerable<TRight> right, Func<TRight, int> rightKeySelector) : base(left, leftKeySelector, right, rightKeySelector)
        {
        }
    }
}