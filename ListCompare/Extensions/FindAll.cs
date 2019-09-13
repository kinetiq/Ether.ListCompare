using System;
using System.Collections.Generic;
using System.Text;

namespace ListCompare.Extensions
{
    public static class FindAnyExtension
    {
        /// <summary>
        /// Searches dictionary for any keys in the keys collection, and returns whatever it can find.
        /// </summary>
        public static List<TValue> FindValuesFor<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, List<TKey> keys)
        {
            var result = new List<TValue>();

            foreach (var key in keys)
            {
                if (dictionary.ContainsKey(key))
                    result.Add(dictionary[key]);
            }

            return result;
        }
    }
}