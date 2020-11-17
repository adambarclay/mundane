using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Routing
{
	[ExcludeFromCodeCoverage]
	public static class DefaultNotFoundResponse_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_Request_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() => new Routing(o => { }).DefaultNotFoundResponse(null!));

			Assert.Equal("request", exception.ParamName);
		}
	}
}
