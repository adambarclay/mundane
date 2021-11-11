using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue;

[ExcludeFromCodeCoverage]
public static class Object_Equals_Returns_False
{
	[Fact]
	public static void When_The_Objects_Have_Different_Names_And_Different_Values()
	{
		var first = new HeaderValue("one", "two");
		var second = new HeaderValue("three", "four");

		Assert.False(first.Equals((object)second));
	}

	[Fact]
	public static void When_The_Objects_Have_The_Same_Name_But_Different_Values()
	{
		var first = new HeaderValue("name", "one");
		var second = new HeaderValue("name", "two");

		Assert.False(first.Equals((object)second));
	}

	[Fact]
	public static void When_The_Objects_Have_The_Same_Value_But_Different_Names()
	{
		var first = new HeaderValue("one", "value");
		var second = new HeaderValue("two", "value");

		Assert.False(first.Equals((object)second));
	}

	[Fact]
	public static void When_The_Other_Object_Is_Not_A_HeaderValue()
	{
		var first = new HeaderValue("name", "value");

		var second = new
		{
			Name = "name",
			Value = "value"
		};

		Assert.False(first.Equals(second));
	}
}
