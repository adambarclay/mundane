using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_EnumerableCollection
{
	[ExcludeFromCodeCoverage]
	public static class GetHashCode_Returns_The_Same_Value_For_Two_Objects
	{
		[Fact]
		public static void When_The_Collections_Are_The_Same()
		{
			var collection = new List<string>();

			var first = new EnumerableCollection<string>(collection);
			var second = new EnumerableCollection<string>(collection);

			Assert.Equal(first.GetHashCode(), second.GetHashCode());
		}
	}
}
