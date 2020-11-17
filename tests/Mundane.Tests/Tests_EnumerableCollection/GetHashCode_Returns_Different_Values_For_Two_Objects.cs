using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_EnumerableCollection
{
	[ExcludeFromCodeCoverage]
	public static class GetHashCode_Returns_Different_Values_For_Two_Objects
	{
		[Fact]
		public static void When_The_Collections_Are_Different()
		{
			var first = new EnumerableCollection<string>(new List<string>());
			var second = new EnumerableCollection<string>(new List<string>());

			Assert.NotEqual(first.GetHashCode(), second.GetHashCode());
		}
	}
}
