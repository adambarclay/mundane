using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_RequestEndpoint
{
	[ExcludeFromCodeCoverage]
	public static class Endpoint_Returns_A_Value
	{
		[Fact]
		public static void Which_Was_Passed_To_The_Constructor()
		{
			var endpoint = (MundaneEndpoint)(_ => ValueTask.FromResult(Response.Ok()));

			var routing = new Routing(o => o.Get("/", endpoint));

			var requestEndpoint = routing.FindEndpoint(HttpMethod.Get, "/");

			Assert.Equal(endpoint, requestEndpoint.Endpoint);
		}
	}
}
