using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Response
{
	[ExcludeFromCodeCoverage]
	public static class Json_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_BodyWriter_Parameter_Is_Null()
		{
			var exception1 = Assert.ThrowsAny<ArgumentNullException>(() => Response.Json(null!));

			Assert.Equal("bodyWriter", exception1.ParamName!);

			var exception2 = Assert.ThrowsAny<ArgumentNullException>(() => Response.Json(200, null!));

			Assert.Equal("bodyWriter", exception2.ParamName!);
		}
	}
}
