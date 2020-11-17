using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_RouteBuilder
{
	[ExcludeFromCodeCoverage]
	public static class Adding_An_Endpoint_Throws_ArgumentException
	{
		[Fact]
		public static void When_The_Method_Parameter_Is_Empty()
		{
			var noParametersSync = (MundaneEndpointDelegateNoParametersSync)Response.Ok;
			var endpointSync = (MundaneEndpointDelegateSync)(r => Response.Ok());
			var noParameters = (MundaneEndpointDelegateNoParameters)(() => Task.FromResult(Response.Ok()));
			var endpoint = (MundaneEndpointDelegate)(r => Task.FromResult(Response.Ok()));

			Action<ArgumentException> check = exception =>
			{
				Assert.Equal("method", exception.ParamName);
				Assert.StartsWith("Method must have a value.", exception.Message, StringComparison.Ordinal);
			};

			check(
				Assert.ThrowsAny<ArgumentException>(
					() => new Routing(o => o.Endpoint(string.Empty, "/", noParametersSync))));

			check(
				Assert.ThrowsAny<ArgumentException>(
					() => new Routing(o => o.Endpoint(string.Empty, "/", endpointSync))));

			check(
				Assert.ThrowsAny<ArgumentException>(
					() => new Routing(o => o.Endpoint(string.Empty, "/", noParameters))));

			check(Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Endpoint(string.Empty, "/", endpoint))));
		}

		[Theory]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("\\Blah")]
		[InlineData("Blah")]
		public static void When_The_Route_Parameter_Does_Not_Begin_With_A_Forward_Slash(string route)
		{
			var noParamsSync = (MundaneEndpointDelegateNoParametersSync)Response.Ok;
			var endpointSync = (MundaneEndpointDelegateSync)(r => Response.Ok());
			var noParameters = (MundaneEndpointDelegateNoParameters)(() => Task.FromResult(Response.Ok()));
			var endpoint = (MundaneEndpointDelegate)(r => Task.FromResult(Response.Ok()));

			Action<ArgumentException> check = exception =>
			{
				Assert.Equal("route", exception.ParamName);

				Assert.Equal("Route must start with a forward slash \"/\". (Parameter 'route')", exception.Message);
			};

			check(Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Delete(route, noParamsSync))));
			check(Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Delete(route, endpointSync))));
			check(Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Delete(route, noParameters))));
			check(Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Delete(route, endpoint))));
			check(Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Endpoint("m", route, noParamsSync))));
			check(Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Endpoint("m", route, endpointSync))));
			check(Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Endpoint("m", route, noParameters))));
			check(Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Endpoint("m", route, endpoint))));
			check(Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Get(route, noParamsSync))));
			check(Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Get(route, endpointSync))));
			check(Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Get(route, noParameters))));
			check(Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Get(route, endpoint))));
			check(Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Post(route, noParamsSync))));
			check(Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Post(route, endpointSync))));
			check(Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Post(route, noParameters))));
			check(Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Post(route, endpoint))));
			check(Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Put(route, noParamsSync))));
			check(Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Put(route, endpointSync))));
			check(Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Put(route, noParameters))));
			check(Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Put(route, endpoint))));
		}
	}
}
