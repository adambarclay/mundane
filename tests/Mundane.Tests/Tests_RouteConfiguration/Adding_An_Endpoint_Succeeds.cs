using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_RouteConfiguration
{
	[ExcludeFromCodeCoverage]
	public static class Adding_An_Endpoint_Succeeds
	{
		[Fact]
		public static async Task When_The_Parameters_Are_Valid()
		{
			var statusCode = RandomNumberGenerator.GetInt32(0, int.MaxValue);

			var noParametersSync = (MundaneEndpointNoParametersSync)(() => new Response(statusCode));
			var endpointSync = (MundaneEndpointSync)(_ => new Response(statusCode));

			var noParameters = (MundaneEndpointNoParameters)(() => ValueTask.FromResult(new Response(statusCode)));

			var endpoint = (MundaneEndpoint)(_ => ValueTask.FromResult(new Response(statusCode)));

			static async Task Test(string method, RouteConfigurationBuilder routeConfigurationBuilder, int statusCode)
			{
				var route = new Routing(routeConfigurationBuilder).FindEndpoint(method, "/");

				var response = await MundaneEngine.ExecuteRequest(route.Endpoint, RequestHelper.Request());

				Assert.Equal(statusCode, response.StatusCode);
			}

			await Test(HttpMethod.Delete, o => o.Delete("/", noParametersSync), statusCode);
			await Test(HttpMethod.Delete, o => o.Delete("/", endpointSync), statusCode);
			await Test(HttpMethod.Delete, o => o.Delete("/", noParameters), statusCode);
			await Test(HttpMethod.Delete, o => o.Delete("/", endpoint), statusCode);
			await Test("m", o => o.Endpoint("m", "/", noParametersSync), statusCode);
			await Test("m", o => o.Endpoint("m", "/", endpointSync), statusCode);
			await Test("m", o => o.Endpoint("m", "/", noParameters), statusCode);
			await Test("m", o => o.Endpoint("m", "/", endpoint), statusCode);
			await Test(HttpMethod.Get, o => o.Get("/", noParametersSync), statusCode);
			await Test(HttpMethod.Get, o => o.Get("/", endpointSync), statusCode);
			await Test(HttpMethod.Get, o => o.Get("/", noParameters), statusCode);
			await Test(HttpMethod.Get, o => o.Get("/", endpoint), statusCode);
			await Test(HttpMethod.Post, o => o.Post("/", noParametersSync), statusCode);
			await Test(HttpMethod.Post, o => o.Post("/", endpointSync), statusCode);
			await Test(HttpMethod.Post, o => o.Post("/", noParameters), statusCode);
			await Test(HttpMethod.Post, o => o.Post("/", endpoint), statusCode);
			await Test(HttpMethod.Put, o => o.Put("/", noParametersSync), statusCode);
			await Test(HttpMethod.Put, o => o.Put("/", endpointSync), statusCode);
			await Test(HttpMethod.Put, o => o.Put("/", noParameters), statusCode);
			await Test(HttpMethod.Put, o => o.Put("/", endpoint), statusCode);
		}
	}
}
