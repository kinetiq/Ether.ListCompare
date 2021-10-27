using System;
using System.Collections.Generic;
using ListCompare.Tests.Models;
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
    }
}
