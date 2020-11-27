What's this?
==============

ListCompare is great for any situation where you need to compare two lists and do something useful with the result. 

Let's say a screen on your site has a grid of monkeys, where a Monkey is defined thusly: 

    public class Monkey
    {
        public int MonkeyId { get; set; }
        public string Name { get; set; }
    }

Users add and remove monkeys from the grid, and then when they're done, they hit save to submit the entire list. 

As the developer, you are left with the annoying and error-prone task of resolving the lists: saving monkeys that exist in the grid but do not exist in the database, and deleting monkeys that do exist in the database but do not exist in the grid.

**Reference Equality Comparison**

ListCompare makes this a little easier. For the sake of the example, let's hand-create our monkey lists and do a naive reference comparison:  

	var fred = new Monkey() { Name = "Fred",  MonkeyId = 1 };
	var jim = new Monkey() { Name = "Jim", MonkeyId = 2 };
	var samson = new Monkey() { Name = "Sam", MonkeyId = 3 };
	var marvin = new Monkey() { Name = "Marvin", MonkeyId = 4 };

	var gridMonkey = new List<Monkey>() { fred, jim, samson }; 
	var dbMonkeys = new List<Monkey>() { jim, marvin };

And here's the comparison code:

	var comparer = ListComparison.ListCompare
		.Compare<Monkey>(gridMonkeys, dbMonkeys)
		.Go();

	foreach(var monkey in comparer.MissingFromLeft()) {
		// Delete monkeys that exist in the DB but don't exist in the grid.
		// This will contain one item: marvin. 
	}

	foreach(var monkey in comparer.MissingFromRight()) {
		// Create monkeys that exist in the grid but don't exist in the DB.
		// This will contain two items: fred and samson. 	
	}

There you have it. Now, the astute among you may be wondering if that was a reference equality check, and indeed, it was. That can be useful in cases where you have the luxury of reference equality, but often we are forced to do comparisons on dissimilar lists, so let's move on to a more realistic example. Don't worry, it's almost as easy.

**Key Comparison**

In the real world you might have a list of integers on one side and a list of EF entities on the other, a bit more like this:

	var gridMonkeys = new List<int> { 1, 2, 3 };
	var dbMonkeys = new List<Monkey>() { jim, marvin };

	// You can compare lists of dissimilar types, as long as they have an integer key. Note the .ByIntegerKey line.
	var comparer = ListComparison.ListCompare
		.Compare(gridMonkeys, dbMonkeys)
		.ByIntegerKey(leftKeySelector: left => left, rightKeySelector: right => right.MonkeyId)  
		.Go();

	foreach(Monkey monkey in comparer.MissingFromLeft()) {
		// Delete monkeys that exist in the DB but don't exist in the grid. Note that MissingFromLeft() returns 
		// a list of Monkey, since the right list is a List<Monkey>.
		// This will contain one item: marvin. 
	}

	foreach(int monkeyId in comparer.MissingFromRight()) {
		// Create monkeys that exist in the grid but don't exist in the DB. Note that MissingFromRight returns
		// a list of int, since the left list is a List<int>.
		// This will contain two items: 1 and 3.
    
                // Okay, there is a bit of a flaw in this example since, having only the key, we lack some fields we would probably need  to 
                // create a full-fledged monkey in the database. The point is to show you how to do key comparison, and for that, I declare victory
                // and must now go make my kids dinner.
	}

How is performance?
==============
It's certainly fine for small lists. It is definitely not currently optimized for huge datasets.

Currently, this relies a number of LINQ expressions, and I assume there is plenty of room for optimization for huge lists. If you are interested in contributing, this would be a great place to jump in!

Where can I get it?
==============

First, <a href="http://docs.nuget.org/docs/start-here/installing-nuget">install NuGet</a>. Then, install Ether.ListCompare from the package manager console:

>PM> Install-Package Ether.ListCompare

ListCompare is Copyright © 2019 Brian MacKay, <a href="getkinetiq.com">Kinetiq</a>, and other contributors under the MIT license.
