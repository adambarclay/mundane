using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_Routing
{
	[ExcludeFromCodeCoverage]
	public static class Constructor_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_MundaneEndpoint_NotFoundEndpoint_Parameter_Is_Null()
		{
			var exception =
				Assert.ThrowsAny<ArgumentNullException>(() => new Routing(_ => { }, (MundaneEndpoint)null!));

			Assert.Equal("notFoundEndpoint", exception.ParamName!);
		}

		[Fact]
		public static void When_The_MundaneEndpointNoParameters_NotFoundEndpoint_Parameter_Is_Null()
		{
			var exception =
				Assert.ThrowsAny<ArgumentNullException>(
					() => new Routing(_ => { }, (MundaneEndpointNoParameters)null!));

			Assert.Equal("notFoundEndpoint", exception.ParamName!);
		}

		[Fact]
		public static void When_The_MundaneEndpointNoParametersSync_NotFoundEndpoint_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() => new Routing(_ => { }, (MundaneEndpointNoParametersSync)null!));

			Assert.Equal("notFoundEndpoint", exception.ParamName!);
		}

		[Fact]
		public static void When_The_MundaneEndpointSync_NotFoundEndpoint_Parameter_Is_Null()
		{
			var exception =
				Assert.ThrowsAny<ArgumentNullException>(() => new Routing(_ => { }, (MundaneEndpointSync)null!));

			Assert.Equal("notFoundEndpoint", exception.ParamName!);
		}

		[Fact]
		public static void When_The_RouteConfigurationBuilder_Parameter_Is_Null()
		{
			var exception1 = Assert.ThrowsAny<ArgumentNullException>(() => new Routing(null!));

			Assert.Equal("routeConfigurationBuilder", exception1.ParamName!);

			var exception2 = Assert.ThrowsAny<ArgumentNullException>(
				() => new Routing(null!, _ => ValueTask.FromResult(Response.Ok())));

			Assert.Equal("routeConfigurationBuilder", exception2.ParamName!);

			var exception3 = Assert.ThrowsAny<ArgumentNullException>(
				() => new Routing(null!, () => ValueTask.FromResult(Response.Ok())));

			Assert.Equal("routeConfigurationBuilder", exception3.ParamName!);

			var exception4 = Assert.ThrowsAny<ArgumentNullException>(() => new Routing(null!, _ => Response.Ok()));

			Assert.Equal("routeConfigurationBuilder", exception4.ParamName!);

			var exception5 = Assert.ThrowsAny<ArgumentNullException>(() => new Routing(null!, Response.Ok));

			Assert.Equal("routeConfigurationBuilder", exception5.ParamName!);
		}
	}
}
