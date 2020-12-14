using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Routing
{
	[ExcludeFromCodeCoverage]
	public static class Constructor_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_NotFoundEndpoint_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(() => new Routing(_ => { }, null!));

			Assert.Equal("notFoundEndpoint", exception.ParamName!);
		}

		[Fact]
		public static void When_The_RouteConfigurationBuilder_Parameter_Is_Null()
		{
			var exception1 = Assert.ThrowsAny<ArgumentNullException>(() => new Routing(null!));

			Assert.Equal("routeConfigurationBuilder", exception1.ParamName!);

			var exception2 = Assert.ThrowsAny<ArgumentNullException>(
				() => new Routing(null!, MundaneEndpoint.Create(Response.NotFound)));

			Assert.Equal("routeConfigurationBuilder", exception2.ParamName!);
		}
	}
}
