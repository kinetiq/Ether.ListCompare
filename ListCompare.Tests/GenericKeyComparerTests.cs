using System.Collections.Generic;
using ListCompare.Tests.Models;
using ListComparison.Tests.EqualityComparers;
using ListComparison.Tests.Models;
using Xunit;

namespace ListCompare.Tests
{
    public class GenericKeyComparerTests
    {
        [Fact]
        public void UniqueGenericKeyComparer_Works()
        {            
            var roberts = new Pirate() { Name = "Dread Pirate Roberts", PirateId = 1 };
            var silver = new Pirate() { Name = "Long John Silver", PirateId = 2 };
            var blackbeard = new Pirate() { Name = "Blackbeard", PirateId = 3 };

            var pirateList = new List<string>() { "Blackbeard", "Fredrik Neij" };
            var pirates = new List<Pirate> {roberts, silver, blackbeard};

            var comparer = ListComparison.ListCompare.Compare(pirateList, pirates)
                .ByKey<string>(left => left, right => right.Name)
                .Go();

            // Common Tests
            var commonLeft = comparer.CommonItemsAsLeft();

            Assert.True(commonLeft.Count == 1);
            Assert.Contains("Blackbeard", commonLeft);

            var commonRight = comparer.CommonItemsAsRight();

            Assert.True(commonRight.Count == 1);
            Assert.Contains(blackbeard, commonRight);

            var common = comparer.CommonItems();

            Assert.True(common.Count == 1);
            Assert.True(common[0].LeftItem == "Blackbeard" && common[0].RightItem == blackbeard);

            var missing = comparer.MissingItems();

            Assert.True(missing.Count == 3);
            Assert.Contains(roberts, missing);
            Assert.Contains(silver, missing);
            Assert.Contains("Fredrik Neij", missing);

            var missingLeft = comparer.MissingFromLeft();

            Assert.True(missingLeft.Count == 2);
            Assert.Contains(roberts, missingLeft);
            Assert.Contains(silver, missingLeft);

            var missingRight = comparer.MissingFromRight();

            Assert.True(missingRight.Count == 1);
            Assert.Contains("Fredrik Neij", missingRight);
        }

        /// <summary>
        /// When comparing nested lists, by default the comparer will compare the references of the lists.
        /// Here we want to compare two lists that have the same values, but are different instances.
        /// This requires a custom comparer.
        /// </summary>
        [Fact]
        public void NestedListComparison_Works()
        {
            // Create two different lists that have the same values. We want these to be considered equal.
            var nestedList1 = new List<int> { 1, 2, 3, 4, 5 };
            var nestedList2 = new List<int> { 1, 2, 3, 4, 5 };

            var list1 = new List<Parameter>()
            {
                new() { Name = "p1", Value  = nestedList1 },
                new() { Name = "p1", Value  = "Pirate" },
                new() { Name = "p1", Value  = 2 }
            };

            var list2 = new List<Parameter>()
            {
                new() { Name = "p1", Value  = nestedList2 },
                new() { Name = "p1", Value  = "Pirate" },
                new() { Name = "p1", Value  = 2 }
            };


            var comparer = ListComparison.ListCompare.Compare<Parameter>(list1, list2)
                .WithEqualityComparer(new ParameterEqualityComparer())
                .Go();

            var result = comparer.CommonItems();
            Assert.True(result.Count == 3);
        }

        /// <summary>
        /// When comparing dictionaries, the items are actually key value pairs. The dictionary comparer
        /// used here will 
        /// </summary>
        [Fact]
        public void NestedListComparison_WithDictionaries_Works()
        {
            // Create two different lists that have the same values. We want these to be considered equal.
            var nestedList1 = new List<int> { 1, 2, 3, 4, 5 };
            var nestedList2 = new List<int> { 1, 2, 3, 4, 5 };

            var dict1 = new List<Dictionary<string, object>>()
            {
                new() { { "p1", nestedList1 } }, // matches! Because we use some special code that forces value comparison.
                new() { { "p2", "Pirate" } }, // matches! 
                new() { { "p3", 1 } },   // no match - not present in dict2
                new() { { "p4", 2 } }, // matches! 
                new() { { "p5", new Pirate() { Name = "Blackbeard", PirateId = 1 }  } }, // not a match; reference types like this will use the default .NET behavior which is reference comparison.
            };

            var dict2 = new List<Dictionary<string, object>>()
            {
                new() { { "p1", nestedList2 } },
                new() { { "p2", "Pirate" } },
                new() { { "p4", 2 } },
                new() { { "p5", new Pirate() { Name = "Blackbeard", PirateId = 1 }  } },
            };

            var comparer = ListComparison.ListCompare
                .Compare<Dictionary<string, object>>(dict1, dict2)
                .WithEqualityComparer(new DictionaryEqualityComparer())
                .Go();

            var result = comparer.CommonItems();
            Assert.True(result.Count == 3);
        }

    }
}
