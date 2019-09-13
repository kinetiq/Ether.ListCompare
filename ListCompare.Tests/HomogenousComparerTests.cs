using System;
using System.Collections.Generic;
using ListCompare.Comparers;
using ListCompare.Tests.Models;
using Xunit;

namespace ListCompare.Tests
{
    public class HomogeneousComparerTests
    {
        [Fact]
        public void Basic_Comparison_Works()
        {
            var fred = new Monkey() { Name = "Fred",  MonkeyId = 1 };
            var jim = new Monkey() { Name = "Jim", MonkeyId = 2 };
            var samson = new Monkey() { Name = "Sam", MonkeyId = 3 };
            var marvin = new Monkey() { Name = "Marvin", MonkeyId = 4 };

            var leftMonkey = new List<Monkey>() { fred, jim, samson }; 
            var rightMonkeys = new List<Monkey>() { jim, marvin };
            
            var comparer = ListCompare
                .Compare<Monkey>(leftMonkey, rightMonkeys)
                .Go();
            
            // Check items that appear in both lists
            var common = comparer.CommonItems();

            Assert.True(common.Count == 1);
            Assert.Contains(jim, common);

            // Check items missing from either list
            var missing = comparer.MissingItems();

            Assert.True(missing.Count == 3);
            Assert.Contains(fred, missing);
            Assert.Contains(samson, missing);
            Assert.Contains(marvin, missing);

            // Check items missing from left list
            var missingLeft = comparer.MissingFromLeft();

            Assert.True(missingLeft.Count == 1);
            Assert.Contains(marvin, missingLeft);

            // Check items missing from right list
            var missingRight = comparer.MissingFromRight();

            Assert.True(missingRight.Count == 2);
            Assert.Contains(fred, missingRight);
            Assert.Contains(samson, missingRight);
        }
    }
}
