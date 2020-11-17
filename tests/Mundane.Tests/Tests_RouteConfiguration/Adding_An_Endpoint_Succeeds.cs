using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_RouteBuilder
{
	[ExcludeFromCodeCoverage]
	public static class Adding_An_Endpoint_Succeeds
	{
		[Fact]
		public static async Task When_The_Parameters_Are_Valid()
		{
			var statusCode = new Random().Next();

			var noParametersSync = (MundaneEndpointDelegateNoParametersSync)(() => new Response(statusCode));
			var endpointSync = (MundaneEndpointDelegateSync)(r => new Response(statusCode));
			var noParameters = (MundaneEndpointDelegateNoParameters)(() => Task.FromResult(new Response(statusCode)));
			var endpoint = (MundaneEndpointDelegate)(r => Task.FromResult(new Response(statusCode)));

			await Adding_An_Endpoint_Succeeds.Test(HttpMethod.Delete, o => o.Delete("/", noParametersSync), statusCode);
			await Adding_An_Endpoint_Succeeds.Test(HttpMethod.Delete, o => o.Delete("/", endpointSync), statusCode);
			await Adding_An_Endpoint_Succeeds.Test(HttpMethod.Delete, o => o.Delete("/", noParameters), statusCode);
			await Adding_An_Endpoint_Succeeds.Test(HttpMethod.Delete, o => o.Delete("/", endpoint), statusCode);
			await Adding_An_Endpoint_Succeeds.Test("m", o => o.Endpoint("m", "/", noParametersSync), statusCode);
			await Adding_An_Endpoint_Succeeds.Test("m", o => o.Endpoint("m", "/", endpointSync), statusCode);
			await Adding_An_Endpoint_Succeeds.Test("m", o => o.Endpoint("m", "/", noParameters), statusCode);
			await Adding_An_Endpoint_Succeeds.Test("m", o => o.Endpoint("m", "/", endpoint), statusCode);
			await Adding_An_Endpoint_Succeeds.Test(HttpMethod.Get, o => o.Get("/", noParametersSync), statusCode);
			await Adding_An_Endpoint_Succeeds.Test(HttpMethod.Get, o => o.Get("/", endpointSync), statusCode);
			await Adding_An_Endpoint_Succeeds.Test(HttpMethod.Get, o => o.Get("/", noParameters), statusCode);
			await Adding_An_Endpoint_Succeeds.Test(HttpMethod.Get, o => o.Get("/", endpoint), statusCode);
			await Adding_An_Endpoint_Succeeds.Test(HttpMethod.Post, o => o.Post("/", noParametersSync), statusCode);
			await Adding_An_Endpoint_Succeeds.Test(HttpMethod.Post, o => o.Post("/", endpointSync), statusCode);
			await Adding_An_Endpoint_Succeeds.Test(HttpMethod.Post, o => o.Post("/", noParameters), statusCode);
			await Adding_An_Endpoint_Succeeds.Test(HttpMethod.Post, o => o.Post("/", endpoint), statusCode);
			await Adding_An_Endpoint_Succeeds.Test(HttpMethod.Put, o => o.Put("/", noParametersSync), statusCode);
			await Adding_An_Endpoint_Succeeds.Test(HttpMethod.Put, o => o.Put("/", endpointSync), statusCode);
			await Adding_An_Endpoint_Succeeds.Test(HttpMethod.Put, o => o.Put("/", noParameters), statusCode);
			await Adding_An_Endpoint_Succeeds.Test(HttpMethod.Put, o => o.Put("/", endpoint), statusCode);
		}

		private static async Task Test(string method, RouteConfigurationDelegate routeConfiguration, int statusCode)
		{
			var route = new Routing(routeConfiguration).FindEndpoint(method, "/");

			var response = await MundaneEngine.ExecuteRequest(route.Endpoint, RequestHelper.Request());

			Assert.Equal(statusCode, response.StatusCode);
		}
	}
}
