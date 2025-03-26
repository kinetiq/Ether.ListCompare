using System;
using System.Collections;
using System.Collections.Generic;
using ListComparison.Tests.Models;

namespace ListComparison.Tests.EqualityComparers
{
    public class ParameterEqualityComparer : IEqualityComparer<Parameter>
    {
        public bool Equals(Parameter x, Parameter y)
        {
            if (x == null || y == null)
                return false;

            // Compare Name
            if (!string.Equals(x.Name, y.Name))
                return false;

            // Compare Value
            return AreValuesEqual(x.Value, y.Value);
        }

        public int GetHashCode(Parameter obj)
        {
            if (obj == null)
                return 0;

            int nameHash = obj.Name?.GetHashCode() ?? 0;
            int valueHash = GetDeepHashCode(obj.Value);
            return HashCode.Combine(nameHash, valueHash);
        }

        private bool AreValuesEqual(object a, object b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (a == null || b == null)
                return false;

            if (a is IEnumerable aEnum && b is IEnumerable bEnum && !(a is string && b is string))
            {
                return EnumerablesAreEqual(aEnum, bEnum);
            }

            if (a.GetType().IsEnum && b.GetType().IsEnum)
            {
                return a.Equals(b);
            }

            return a.Equals(b);
        }

        private bool EnumerablesAreEqual(IEnumerable a, IEnumerable b)
        {
            var enumA = a.GetEnumerator();
            var enumB = b.GetEnumerator();

            while (true)
            {
                bool hasA = enumA.MoveNext();
                bool hasB = enumB.MoveNext();

                if (hasA != hasB)
                    return false;

                if (!hasA) // both finished
                    break;

                object itemA = enumA.Current;
                object itemB = enumB.Current;

                if (!Equals(itemA, itemB))
                    return false;
            }

            return true;
        }

        private int GetDeepHashCode(object value)
        {
            if (value == null)
                return 0;

            if (value is string)
                return value.GetHashCode();

            if (value is IEnumerable enumerable && !(value is string))
            {
                unchecked
                {
                    int hash = 17;
                    foreach (var item in enumerable)
                    {
                        hash = hash * 31 + (item?.GetHashCode() ?? 0);
                    }
                    return hash;
                }
            }

            return value.GetHashCode();
        }
    }
}