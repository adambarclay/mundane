using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_EnumerableCollection
{
	[ExcludeFromCodeCoverage]
	public static class Constructor_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_Collection_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(() => new EnumerableCollection<string>(null!));

			Assert.Equal("collection", exception.ParamName);
		}
	}
}
