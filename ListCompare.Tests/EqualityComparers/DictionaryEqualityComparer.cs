using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ListComparison.Tests.EqualityComparers;

public class DictionaryEqualityComparer : IEqualityComparer<Dictionary<string, object>>
{
    public bool Equals(Dictionary<string, object> x, Dictionary<string, object> y)
    {
        if (ReferenceEquals(x, y))
            return true;

        if (x == null || y == null)
            return false;

        if (x.Count != y.Count)
            return false;

        foreach (var kvp in x)
        {
            if (!y.TryGetValue(kvp.Key, out var yValue))
                return false;

            if (!AreValuesEqual(kvp.Value, yValue))
                return false;
        }

        return true;
    }

    public int GetHashCode(Dictionary<string, object> dict)
    {
        if (dict == null)
            return 0;

        unchecked
        {
            int hash = 17;
            foreach (var kvp in dict.OrderBy(k => k.Key)) // sort to ensure consistent hash
            {
                hash = hash * 23 + (kvp.Key?.GetHashCode() ?? 0);
                hash = hash * 23 + GetDeepHashCode(kvp.Value);
            }
            return hash;
        }
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

