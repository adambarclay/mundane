using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_RequestEndpoint;

[ExcludeFromCodeCoverage]
public static class Operator_Equals_Returns_False
{
	[Fact]
	public static void When_The_Objects_Have_Different_Endpoints_And_Different_Parameters()
	{
		var routing = new Routing(
			o =>
			{
				o.Get("/one/{param}", _ => ValueTask.FromResult(Response.Ok()));
				o.Get("/two/{param}", _ => ValueTask.FromResult(Response.NotFound()));
			});

		var first = routing.FindEndpoint(HttpMethod.Get, "/one/three");
		var second = routing.FindEndpoint(HttpMethod.Get, "/two/four");

		Assert.False(first == second);
	}

	[Fact]
	public static void When_The_Objects_Have_The_Same_Endpoint_But_Different_Parameters()
	{
		var endpoint = (MundaneEndpoint)(_ => ValueTask.FromResult(Response.Ok()));

		var routing = new Routing(
			o =>
			{
				o.Get("/one/{param}", endpoint);
				o.Get("/two/{param}", endpoint);
			});

		var first = routing.FindEndpoint(HttpMethod.Get, "/one/three");
		var second = routing.FindEndpoint(HttpMethod.Get, "/two/four");

		Assert.False(first == second);
	}

	[Fact]
	public static void When_The_Objects_Have_The_Same_Parameters_But_Different_Endpoints()
	{
		var routing = new Routing(
			o =>
			{
				o.Get("/one/{param}", _ => ValueTask.FromResult(Response.Ok()));
				o.Get("/two/{param}", _ => ValueTask.FromResult(Response.NotFound()));
			});

		var first = routing.FindEndpoint(HttpMethod.Get, "/one/hello");
		var second = routing.FindEndpoint(HttpMethod.Get, "/two/hello");

		Assert.False(first == second);
	}
}
