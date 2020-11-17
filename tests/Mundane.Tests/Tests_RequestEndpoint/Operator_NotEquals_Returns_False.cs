using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_RequestEndpoint
{
	[ExcludeFromCodeCoverage]
	public static class Operator_NotEquals_Returns_False
	{
		[Fact]
		public static void When_The_Objects_Have_The_Same_Endpoint_And_The_Same_Parameters()
		{
			var endpoint = (MundaneEndpointDelegate)(r => Task.FromResult(Response.Ok()));

			var routing = new Routing(
				o =>
				{
					o.Get("/one/{param}", endpoint);
					o.Get("/two/{param}", endpoint);
				});

			var first = routing.FindEndpoint(HttpMethod.Get, "/one/hello");
			var second = routing.FindEndpoint(HttpMethod.Get, "/two/hello");

			Assert.False(first != second);
		}
	}
}