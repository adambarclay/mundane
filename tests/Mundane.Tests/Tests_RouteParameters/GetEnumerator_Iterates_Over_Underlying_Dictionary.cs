using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_RouteParameters;

[ExcludeFromCodeCoverage]
public static class GetEnumerator_Iterates_Over_Underlying_Dictionary
{
	[Fact]
	public static void When_Called_As_Generic_IEnumerable()
	{
		var dictionary = new Dictionary<string, string>
		{
			{ Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
			{ Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
			{ Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
		};

		var enumerable = (IEnumerable<KeyValuePair<string, string>>)new RouteParameters(dictionary);

		foreach ((var key, var value) in enumerable)
		{
			Assert.Equal(dictionary[key], value);
		}
	}

	[Fact]
	public static void When_Called_As_IEnumerable()
	{
		var dictionary = new Dictionary<string, string>
		{
			{ Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
			{ Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
			{ Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
		};

		var enumerable = (IEnumerable)new RouteParameters(dictionary);

		foreach (var item in enumerable)
		{
			(var key, var value) = (KeyValuePair<string, string>)item!;

			Assert.Equal(dictionary[key], value);
		}
	}

	[Fact]
	public static void When_Called_Directly()
	{
		var dictionary = new Dictionary<string, string>
		{
			{ Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
			{ Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
			{ Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
		};

		var enumerable = new RouteParameters(dictionary);

		foreach ((var key, var value) in enumerable)
		{
			Assert.Equal(dictionary[key], value);
		}
	}
}
