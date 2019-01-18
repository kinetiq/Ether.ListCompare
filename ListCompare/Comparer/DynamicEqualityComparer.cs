using System;
using System.Collections.Generic;
using System.Text;

namespace ListCompare.Comparer
{
    public class DynamicEqualityComparer<X, Y> : IEqualityComparer<X>
    {
        public bool Equals(X x, X y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(X obj)
        {
            throw new NotImplementedException();
        }
    }
}
