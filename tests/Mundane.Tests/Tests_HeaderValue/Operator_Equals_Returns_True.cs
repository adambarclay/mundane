using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue;

[ExcludeFromCodeCoverage]
public static class Operator_Equals_Returns_True
{
	[Fact]
	public static void When_The_Objects_Have_The_Same_Names_And_The_Same_Values()
	{
		var first = new HeaderValue("name", "value");
		var second = new HeaderValue("name", "value");

		Assert.True(first == second);
	}
}
