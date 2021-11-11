using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_Routing;

[ExcludeFromCodeCoverage]
public static class DefaultNotFoundResponse_Returns_NotFound_Response
{
	[Fact]
	public static async Task When_The_Route_Does_Not_Exist_For_Any_Method()
	{
		var routing = new Routing(_ => { });

		var response = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(routing.DefaultNotFoundResponse),
			RequestHelper.Request(HttpMethod.Get, "/"));

		Assert.Equal(404, response.StatusCode);
	}
}
