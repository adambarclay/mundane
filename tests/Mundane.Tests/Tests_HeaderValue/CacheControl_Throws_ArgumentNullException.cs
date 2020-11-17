using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue
{
	[ExcludeFromCodeCoverage]
	public static class CacheControl_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_Directives_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(() => HeaderValue.CacheControl(null!));

			Assert.Equal("directives", exception.ParamName);
		}
	}
}
