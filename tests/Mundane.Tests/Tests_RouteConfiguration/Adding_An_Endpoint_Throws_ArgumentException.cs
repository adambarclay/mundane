using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_RouteConfiguration;

[ExcludeFromCodeCoverage]
public static class Adding_An_Endpoint_Throws_ArgumentException
{
	[Fact]
	public static void When_The_Method_Parameter_Is_Empty()
	{
		var noParamsSync = (MundaneEndpointNoParametersSync)Response.Ok;
		var endpointSync = (MundaneEndpointSync)(_ => Response.Ok());
		var noParams = (MundaneEndpointNoParameters)(() => ValueTask.FromResult(Response.Ok()));
		var endpoint = (MundaneEndpoint)(_ => ValueTask.FromResult(Response.Ok()));

		static void Test(RouteConfigurationBuilder routeConfigurationBuilder)
		{
			var exception = Assert.ThrowsAny<ArgumentException>(() => new Routing(routeConfigurationBuilder));

			Assert.Equal("method", exception.ParamName!);
			Assert.StartsWith("Method must have a value.", exception.Message, StringComparison.Ordinal);
		}

		Test(o => o.Endpoint(string.Empty, "/", noParamsSync));
		Test(o => o.Endpoint(string.Empty, "/", endpointSync));
		Test(o => o.Endpoint(string.Empty, "/", noParams));
		Test(o => o.Endpoint(string.Empty, "/", endpoint));
	}

	[Theory]
	[InlineData("")]
	[InlineData(" ")]
	[InlineData("\\Blah")]
	[InlineData("Blah")]
	public static void When_The_Route_Parameter_Does_Not_Begin_With_A_Forward_Slash(string route)
	{
		var noParamsSync = (MundaneEndpointNoParametersSync)Response.Ok;
		var endpointSync = (MundaneEndpointSync)(_ => Response.Ok());
		var noParameters = (MundaneEndpointNoParameters)(() => ValueTask.FromResult(Response.Ok()));
		var endpoint = (MundaneEndpoint)(_ => ValueTask.FromResult(Response.Ok()));

		static void Test(RouteConfigurationBuilder routeConfigurationBuilder)
		{
			var exception = Assert.ThrowsAny<ArgumentException>(() => new Routing(routeConfigurationBuilder));

			Assert.Equal("route", exception.ParamName!);
			Assert.Equal("Route must start with a forward slash \"/\". (Parameter 'route')", exception.Message);
		}

		Test(o => o.Delete(route, noParamsSync));
		Test(o => o.Delete(route, endpointSync));
		Test(o => o.Delete(route, noParameters));
		Test(o => o.Delete(route, endpoint));
		Test(o => o.Endpoint("m", route, noParamsSync));
		Test(o => o.Endpoint("m", route, endpointSync));
		Test(o => o.Endpoint("m", route, noParameters));
		Test(o => o.Endpoint("m", route, endpoint));
		Test(o => o.Get(route, noParamsSync));
		Test(o => o.Get(route, endpointSync));
		Test(o => o.Get(route, noParameters));
		Test(o => o.Get(route, endpoint));
		Test(o => o.Post(route, noParamsSync));
		Test(o => o.Post(route, endpointSync));
		Test(o => o.Post(route, noParameters));
		Test(o => o.Post(route, endpoint));
		Test(o => o.Put(route, noParamsSync));
		Test(o => o.Put(route, endpointSync));
		Test(o => o.Put(route, noParameters));
		Test(o => o.Put(route, endpoint));
	}
}
