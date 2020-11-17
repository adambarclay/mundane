using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Routing
{
	[ExcludeFromCodeCoverage]
	public static class FindEndpoint_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_Method_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() => new Routing(o => { }).FindEndpoint(null!, "/"));

			Assert.Equal("method", exception.ParamName);
		}

		[Fact]
		public static void When_The_Path_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() => new Routing(o => { }).FindEndpoint(HttpMethod.Get, null!));

			Assert.Equal("path", exception.ParamName);
		}
	}
}
