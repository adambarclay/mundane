using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Response
{
	[ExcludeFromCodeCoverage]
	public static class MethodNotAllowed_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_AllowedMethods_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(() => Response.MethodNotAllowed(null!));

			Assert.Equal("allowedMethods", exception.ParamName!);
		}
	}
}
