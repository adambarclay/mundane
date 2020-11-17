using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Response
{
	[ExcludeFromCodeCoverage]
	public static class NotFound_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_BodyWriter_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(() => Response.NotFound(null!));

			Assert.Equal("bodyWriter", exception.ParamName);
		}
	}
}
