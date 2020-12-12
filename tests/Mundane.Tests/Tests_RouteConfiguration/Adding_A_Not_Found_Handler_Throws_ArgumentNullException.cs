using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_RouteConfiguration
{
	[ExcludeFromCodeCoverage]
	public static class Adding_A_Not_Found_Handler_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_Endpoint_Is_Null()
		{
			static T NullEndpointDelegate<T>()
				where T : class =>
				null!;

			var noParametersSync = NullEndpointDelegate<MundaneEndpointDelegateNoParametersSync>();
			var endpointSync = NullEndpointDelegate<MundaneEndpointDelegateSync>();
			var noParameters = NullEndpointDelegate<MundaneEndpointDelegateNoParameters>();
			var endpoint = NullEndpointDelegate<MundaneEndpointDelegate>();

			static void Test(RouteConfigurationBuilder routeConfigurationBuilder)
			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(() => new Routing(routeConfigurationBuilder));

				Assert.Equal("endpoint", exception.ParamName!);
			}

			Test(o => o.NotFound(noParametersSync));
			Test(o => o.NotFound(endpointSync));
			Test(o => o.NotFound(noParameters));
			Test(o => o.NotFound(endpoint));
		}
	}
}
