using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue
{
	[ExcludeFromCodeCoverage]
	public static class Location_Returns_The_Correct_Value
	{
		[Fact]
		public static void When_The_Location_Has_Been_Supplied()
		{
			var location = Guid.NewGuid().ToString();

			var header = HeaderValue.Location(location);

			Assert.Equal("location", header.Name);
			Assert.Equal(location, header.Value);
		}
	}
}
