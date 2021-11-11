using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_EnumerableCollection;

[ExcludeFromCodeCoverage]
public static class IEquatable_Equals_Returns_True
{
	[Fact]
	public static void When_The_Collections_Are_The_Same()
	{
		var collection = new List<string>();

		var first = new EnumerableCollection<string>(collection);
		var second = new EnumerableCollection<string>(collection);

		Assert.True(first.Equals(second));
	}
}
