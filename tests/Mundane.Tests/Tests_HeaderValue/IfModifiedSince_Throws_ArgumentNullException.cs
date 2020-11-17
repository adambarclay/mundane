using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue
{
	[ExcludeFromCodeCoverage]
	public static class IfModifiedSince_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_Request_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(() => HeaderValue.IfModifiedSince(null!));

			Assert.Equal("request", exception.ParamName);
		}
	}
}
