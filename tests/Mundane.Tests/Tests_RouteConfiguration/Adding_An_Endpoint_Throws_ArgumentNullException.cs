using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_RouteConfiguration
{
	[ExcludeFromCodeCoverage]
	public static class Adding_An_Endpoint_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_Endpoint_Parameter_Is_Null()
		{
			static T NullEndpointDelegate<T>()
				where T : class =>
				null!;

			var noParamsSync = NullEndpointDelegate<MundaneEndpointDelegateNoParametersSync>();
			var endpointSync = NullEndpointDelegate<MundaneEndpointDelegateSync>();
			var noParameters = NullEndpointDelegate<MundaneEndpointDelegateNoParameters>();
			var endpoint = NullEndpointDelegate<MundaneEndpointDelegate>();

			static void Test(RouteConfigurationBuilder routeConfigurationBuilder)
			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(() => new Routing(routeConfigurationBuilder));

				Assert.Equal("endpoint", exception.ParamName!);
			}

			Test(o => o.Delete("/", noParamsSync));
			Test(o => o.Delete("/", endpointSync));
			Test(o => o.Delete("/", noParameters));
			Test(o => o.Delete("/", endpoint));
			Test(o => o.Endpoint("m", "/", noParamsSync));
			Test(o => o.Endpoint("m", "/", endpointSync));
			Test(o => o.Endpoint("m", "/", noParameters));
			Test(o => o.Endpoint("m", "/", endpoint));
			Test(o => o.Get("/", noParamsSync));
			Test(o => o.Get("/", endpointSync));
			Test(o => o.Get("/", noParameters));
			Test(o => o.Get("/", endpoint));
			Test(o => o.Post("/", noParamsSync));
			Test(o => o.Post("/", endpointSync));
			Test(o => o.Post("/", noParameters));
			Test(o => o.Post("/", endpoint));
			Test(o => o.Put("/", noParamsSync));
			Test(o => o.Put("/", endpointSync));
			Test(o => o.Put("/", noParameters));
			Test(o => o.Put("/", endpoint));
		}

		[Fact]
		public static void When_The_Method_Parameter_Is_Null()
		{
			var noParamsSync = (MundaneEndpointDelegateNoParametersSync)Response.Ok;
			var endpointSync = (MundaneEndpointDelegateSync)(r => Response.Ok());

			var noParameters = (MundaneEndpointDelegateNoParameters)(() => ValueTask.FromResult(Response.Ok()));

			var endpoint = (MundaneEndpointDelegate)(r => ValueTask.FromResult(Response.Ok()));

			static void Test(RouteConfigurationBuilder routeConfigurationBuilder)
			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(() => new Routing(routeConfigurationBuilder));

				Assert.Equal("method", exception.ParamName!);
			}

			Test(o => o.Endpoint(null!, "/", noParamsSync));
			Test(o => o.Endpoint(null!, "/", endpointSync));
			Test(o => o.Endpoint(null!, "/", noParameters));
			Test(o => o.Endpoint(null!, "/", endpoint));
		}

		[Fact]
		public static void When_The_Route_Parameter_Is_Null()
		{
			var noParamsSync = (MundaneEndpointDelegateNoParametersSync)Response.Ok;
			var endpointSync = (MundaneEndpointDelegateSync)(r => Response.Ok());
			var noParameters = (MundaneEndpointDelegateNoParameters)(() => ValueTask.FromResult(Response.Ok()));
			var endpoint = (MundaneEndpointDelegate)(r => ValueTask.FromResult(Response.Ok()));

			static void Test(RouteConfigurationBuilder routeConfigurationBuilder)
			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(() => new Routing(routeConfigurationBuilder));

				Assert.Equal("route", exception.ParamName!);
			}

			Test(o => o.Delete(null!, noParamsSync));
			Test(o => o.Delete(null!, endpointSync));
			Test(o => o.Delete(null!, noParameters));
			Test(o => o.Delete(null!, endpoint));
			Test(o => o.Endpoint("m", null!, noParamsSync));
			Test(o => o.Endpoint("m", null!, endpointSync));
			Test(o => o.Endpoint("m", null!, noParameters));
			Test(o => o.Endpoint("m", null!, endpoint));
			Test(o => o.Get(null!, noParamsSync));
			Test(o => o.Get(null!, endpointSync));
			Test(o => o.Get(null!, noParameters));
			Test(o => o.Get(null!, endpoint));
			Test(o => o.Post(null!, noParamsSync));
			Test(o => o.Post(null!, endpointSync));
			Test(o => o.Post(null!, noParameters));
			Test(o => o.Post(null!, endpoint));
			Test(o => o.Put(null!, noParamsSync));
			Test(o => o.Put(null!, endpointSync));
			Test(o => o.Put(null!, noParameters));
			Test(o => o.Put(null!, endpoint));
		}
	}
}
