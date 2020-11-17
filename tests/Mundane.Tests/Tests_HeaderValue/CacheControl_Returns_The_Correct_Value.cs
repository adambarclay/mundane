using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue
{
	[ExcludeFromCodeCoverage]
	public static class CacheControl_Returns_The_Correct_Value
	{
		[Fact]
		public static void When_The_Directives_Have_Been_Supplied()
		{
			var directives = Guid.NewGuid().ToString();

			var header = HeaderValue.CacheControl(directives);

			Assert.Equal("cache-control", header.Name);
			Assert.Equal(directives, header.Value);
		}
	}
}
