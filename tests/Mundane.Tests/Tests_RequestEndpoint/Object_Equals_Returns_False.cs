using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_RequestEndpoint
{
	[ExcludeFromCodeCoverage]
	public static class Object_Equals_Returns_False
	{
		[Fact]
		public static void When_The_Objects_Have_Different_Endpoints_And_Different_Parameters()
		{
			var routing = new Routing(
				o =>
				{
					o.Get("/one/{param}", r => Task.FromResult(Response.Ok()));
					o.Get("/two/{param}", r => Task.FromResult(Response.NotFound()));
				});

			var first = routing.FindEndpoint(HttpMethod.Get, "/one/three");
			var second = routing.FindEndpoint(HttpMethod.Get, "/two/four");

			Assert.False(first.Equals((object)second));
		}

		[Fact]
		public static void When_The_Objects_Have_The_Same_Endpoint_But_Different_Parameters()
		{
			var endpoint = (MundaneEndpointDelegate)(r => Task.FromResult(Response.Ok()));

			var routing = new Routing(
				o =>
				{
					o.Get("/one/{param}", endpoint);
					o.Get("/two/{param}", endpoint);
				});

			var first = routing.FindEndpoint(HttpMethod.Get, "/one/three");
			var second = routing.FindEndpoint(HttpMethod.Get, "/two/four");

			Assert.False(first.Equals((object)second));
		}

		[Fact]
		public static void When_The_Objects_Have_The_Same_Parameters_But_Different_Endpoints()
		{
			var routing = new Routing(
				o =>
				{
					o.Get("/one/{param}", r => Task.FromResult(Response.Ok()));
					o.Get("/two/{param}", r => Task.FromResult(Response.NotFound()));
				});

			var first = routing.FindEndpoint(HttpMethod.Get, "/one/hello");
			var second = routing.FindEndpoint(HttpMethod.Get, "/two/hello");

			Assert.False(first.Equals((object)second));
		}

		[Fact]
		public static void When_The_Other_Object_Is_Not_A_RequestEndpoint()
		{
			var endpoint = (MundaneEndpointDelegate)(r => Task.FromResult(Response.Ok()));

			var routing = new Routing(o => o.Get("/one/{param}", endpoint));

			var first = routing.FindEndpoint(HttpMethod.Get, "/one/hello");

			var second = new
			{
				Endpoint = endpoint,
				RouteParameters = new EnumerableDictionary<string, string>(
					new Dictionary<string, string> { { "param", "hello" } })
			};

			Assert.False(first.Equals(second));
		}
	}
}
