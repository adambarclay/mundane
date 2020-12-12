using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_RouteConfiguration
{
	[ExcludeFromCodeCoverage]
	public static class Adding_A_Not_Found_Handler_Succeeds
	{
		[Fact]
		public static async Task When_The_Endpoint_Is_Valid()
		{
			var statusCode = RandomNumberGenerator.GetInt32(0, int.MaxValue);

			var noParametersSync = (MundaneEndpointDelegateNoParametersSync)(() => new Response(statusCode));
			var endpointSync = (MundaneEndpointDelegateSync)(r => new Response(statusCode));

			var noParameters =
				(MundaneEndpointDelegateNoParameters)(() => ValueTask.FromResult(new Response(statusCode)));

			var endpoint = (MundaneEndpointDelegate)(r => ValueTask.FromResult(new Response(statusCode)));

			static async Task Test(RouteConfigurationBuilder routeConfigurationBuilder, int statusCode)
			{
				var route = new Routing(routeConfigurationBuilder).FindEndpoint(HttpMethod.Get, "/");

				var response = await MundaneEngine.ExecuteRequest(route.Endpoint, RequestHelper.Request());

				Assert.Equal(statusCode, response.StatusCode);
			}

			await Test(o => o.NotFound(noParametersSync), statusCode);
			await Test(o => o.NotFound(endpointSync), statusCode);
			await Test(o => o.NotFound(noParameters), statusCode);
			await Test(o => o.NotFound(endpoint), statusCode);
		}
	}
}
