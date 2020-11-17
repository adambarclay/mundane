using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue
{
	[ExcludeFromCodeCoverage]
	public static class Location_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_Location_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(() => HeaderValue.Location(null!));

			Assert.Equal("location", exception.ParamName);
		}
	}
}
