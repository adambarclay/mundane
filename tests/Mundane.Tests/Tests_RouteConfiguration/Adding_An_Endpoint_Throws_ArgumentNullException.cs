using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_RouteBuilder
{
	[ExcludeFromCodeCoverage]
	public static class Adding_An_Endpoint_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_Endpoint_Parameter_Is_Null()
		{
			var noParamsSync = Adding_An_Endpoint_Throws_ArgumentNullException
				.NullEndpointDelegate<MundaneEndpointDelegateNoParametersSync>();

			var endpointSync = Adding_An_Endpoint_Throws_ArgumentNullException
				.NullEndpointDelegate<MundaneEndpointDelegateSync>();

			var noParameters = Adding_An_Endpoint_Throws_ArgumentNullException
				.NullEndpointDelegate<MundaneEndpointDelegateNoParameters>();

			var endpoint =
				Adding_An_Endpoint_Throws_ArgumentNullException.NullEndpointDelegate<MundaneEndpointDelegate>();

			Action<ArgumentNullException> check = exception => Assert.Equal("endpoint", exception.ParamName);

			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Delete("/", noParamsSync))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Delete("/", endpointSync))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Delete("/", noParameters))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Delete("/", endpoint))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Endpoint("m", "/", noParamsSync))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Endpoint("m", "/", endpointSync))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Endpoint("m", "/", noParameters))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Endpoint("m", "/", endpoint))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Get("/", noParamsSync))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Get("/", endpointSync))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Get("/", noParameters))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Get("/", endpoint))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Post("/", noParamsSync))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Post("/", endpointSync))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Post("/", noParameters))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Post("/", endpoint))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Put("/", noParamsSync))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Put("/", endpointSync))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Put("/", noParameters))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Put("/", endpoint))));
		}

		[Fact]
		public static void When_The_Method_Parameter_Is_Null()
		{
			var noParamsSync = (MundaneEndpointDelegateNoParametersSync)Response.Ok;
			var endpointSync = (MundaneEndpointDelegateSync)(r => Response.Ok());

			var noParameters = (MundaneEndpointDelegateNoParameters)(() => Task.FromResult(Response.Ok()));

			var endpoint = (MundaneEndpointDelegate)(r => Task.FromResult(Response.Ok()));

			Action<ArgumentNullException> check = exception => Assert.Equal("method", exception.ParamName);

			check(
				Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Endpoint(null!, "/", noParamsSync))));

			check(
				Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Endpoint(null!, "/", endpointSync))));

			check(
				Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Endpoint(null!, "/", noParameters))));

			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Endpoint(null!, "/", endpoint))));
		}

		[Fact]
		public static void When_The_Route_Parameter_Is_Null()
		{
			var noParamsSync = (MundaneEndpointDelegateNoParametersSync)Response.Ok;
			var endpointSync = (MundaneEndpointDelegateSync)(r => Response.Ok());
			var noParameters = (MundaneEndpointDelegateNoParameters)(() => Task.FromResult(Response.Ok()));
			var endpoint = (MundaneEndpointDelegate)(r => Task.FromResult(Response.Ok()));

			Action<ArgumentNullException> check = exception => Assert.Equal("route", exception.ParamName);

			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Delete(null!, noParamsSync))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Delete(null!, endpointSync))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Delete(null!, noParameters))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Delete(null!, endpoint))));

			check(
				Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Endpoint("m", null!, noParamsSync))));

			check(
				Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Endpoint("m", null!, endpointSync))));

			check(
				Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Endpoint("m", null!, noParameters))));

			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Endpoint("m", null!, endpoint))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Get(null!, noParamsSync))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Get(null!, endpointSync))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Get(null!, noParameters))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Get(null!, endpoint))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Post(null!, noParamsSync))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Post(null!, endpointSync))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Post(null!, noParameters))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Post(null!, endpoint))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Put(null!, noParamsSync))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Put(null!, endpointSync))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Put(null!, noParameters))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.Put(null!, endpoint))));
		}

		private static T NullEndpointDelegate<T>()
			where T : class
		{
			return null!;
		}
	}
}
