using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue
{
	[ExcludeFromCodeCoverage]
	public static class ContentType_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_ContentType_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(() => HeaderValue.ContentType(null!));

			Assert.Equal("contentType", exception.ParamName);
		}
	}
}
