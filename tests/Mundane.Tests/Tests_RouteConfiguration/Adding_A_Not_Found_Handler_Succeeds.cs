using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_RouteBuilder
{
	[ExcludeFromCodeCoverage]
	public static class Adding_A_Not_Found_Handler_Succeeds
	{
		[Fact]
		public static async Task When_The_Endpoint_Is_Valid()
		{
			var statusCode = new Random().Next();

			var noParametersSync = (MundaneEndpointDelegateNoParametersSync)(() => new Response(statusCode));
			var endpointSync = (MundaneEndpointDelegateSync)(r => new Response(statusCode));
			var noParameters = (MundaneEndpointDelegateNoParameters)(() => Task.FromResult(new Response(statusCode)));
			var endpoint = (MundaneEndpointDelegate)(r => Task.FromResult(new Response(statusCode)));

			await Adding_A_Not_Found_Handler_Succeeds.Test(o => o.NotFound(noParametersSync), statusCode);
			await Adding_A_Not_Found_Handler_Succeeds.Test(o => o.NotFound(endpointSync), statusCode);
			await Adding_A_Not_Found_Handler_Succeeds.Test(o => o.NotFound(noParameters), statusCode);
			await Adding_A_Not_Found_Handler_Succeeds.Test(o => o.NotFound(endpoint), statusCode);
		}

		private static async Task Test(RouteConfigurationBuilder routeConfiguration, int statusCode)
		{
			var route = new Routing(routeConfiguration).FindEndpoint(HttpMethod.Get, "/");

			var response = await MundaneEngine.ExecuteRequest(route.Endpoint, RequestHelper.Request());

			Assert.Equal(statusCode, response.StatusCode);
		}
	}
}
