using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue
{
	[ExcludeFromCodeCoverage]
	public static class Allow_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_AllowedMethods_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(() => HeaderValue.Allow(null!));

			Assert.Equal("allowedMethods", exception.ParamName!);
		}
	}
}
