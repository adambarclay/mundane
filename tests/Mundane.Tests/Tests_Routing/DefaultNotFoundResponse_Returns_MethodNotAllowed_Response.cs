using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_Routing
{
	[ExcludeFromCodeCoverage]
	public static class DefaultNotFoundResponse_Returns_MethodNotAllowed_Response
	{
		[Fact]
		public static async Task When_The_Endpoint_Is_Registered_For_A_Different_Method()
		{
			var routing = new Routing(
				o =>
				{
					o.Post("/", Response.Ok);
					o.Delete("/", Response.Ok);
					o.Put("/blah", Response.Ok);
				});

			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpointFactory.Create(routing.DefaultNotFoundResponse),
				RequestHelper.Request(HttpMethod.Get, "/"));

			Assert.Equal(405, response.StatusCode);
			Assert.Equal("DELETE,POST", response.Headers.First(o => o.Name == "allow").Value);
		}
	}
}
