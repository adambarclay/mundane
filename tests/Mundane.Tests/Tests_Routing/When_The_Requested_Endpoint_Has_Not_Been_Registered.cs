using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_Routing
{
	[ExcludeFromCodeCoverage]
	public static class When_The_Requested_Endpoint_Has_Not_Been_Registered
	{
		[Fact]
		public static async Task FindEndpoint_Returns_Not_Found_Handler_MundaneEndpoint()
		{
			var path = "/" + Guid.NewGuid();

			var statusCode = RandomNumberGenerator.GetInt32(0, int.MaxValue);
			var message = Guid.NewGuid().ToString();

			var routing = new Routing(
				o => o.Get("/" + Guid.NewGuid(), () => Response.Ok(x => x.Write(Guid.NewGuid().ToString()))),
				_ => ValueTask.FromResult(new Response(statusCode, x => x.Write(message))));

			var response = await MundaneEngine.ExecuteRequest(
				routing.FindEndpoint(HttpMethod.Get, path).Endpoint,
				RequestHelper.Request(HttpMethod.Get, path));

			Assert.Equal(statusCode, response.StatusCode);
			Assert.Equal(message, await ResponseHelper.Body(response));
		}

		[Fact]
		public static async Task FindEndpoint_Returns_Not_Found_Handler_MundaneEndpointNoParameters()
		{
			var path = "/" + Guid.NewGuid();

			var statusCode = RandomNumberGenerator.GetInt32(0, int.MaxValue);
			var message = Guid.NewGuid().ToString();

			var routing = new Routing(
				o => o.Get("/" + Guid.NewGuid(), () => Response.Ok(x => x.Write(Guid.NewGuid().ToString()))),
				() => ValueTask.FromResult(new Response(statusCode, x => x.Write(message))));

			var response = await MundaneEngine.ExecuteRequest(
				routing.FindEndpoint(HttpMethod.Get, path).Endpoint,
				RequestHelper.Request(HttpMethod.Get, path));

			Assert.Equal(statusCode, response.StatusCode);
			Assert.Equal(message, await ResponseHelper.Body(response));
		}

		[Fact]
		public static async Task FindEndpoint_Returns_Not_Found_Handler_MundaneEndpointNoParametersSync()
		{
			var path = "/" + Guid.NewGuid();

			var statusCode = RandomNumberGenerator.GetInt32(0, int.MaxValue);
			var message = Guid.NewGuid().ToString();

			var routing = new Routing(
				o => o.Get("/" + Guid.NewGuid(), () => Response.Ok(x => x.Write(Guid.NewGuid().ToString()))),
				() => new Response(statusCode, x => x.Write(message)));

			var response = await MundaneEngine.ExecuteRequest(
				routing.FindEndpoint(HttpMethod.Get, path).Endpoint,
				RequestHelper.Request(HttpMethod.Get, path));

			Assert.Equal(statusCode, response.StatusCode);
			Assert.Equal(message, await ResponseHelper.Body(response));
		}

		[Fact]
		public static async Task FindEndpoint_Returns_Not_Found_Handler_MundaneEndpointSync()
		{
			var path = "/" + Guid.NewGuid();

			var statusCode = RandomNumberGenerator.GetInt32(0, int.MaxValue);
			var message = Guid.NewGuid().ToString();

			var routing = new Routing(
				o => o.Get("/" + Guid.NewGuid(), () => Response.Ok(x => x.Write(Guid.NewGuid().ToString()))),
				_ => new Response(statusCode, x => x.Write(message)));

			var response = await MundaneEngine.ExecuteRequest(
				routing.FindEndpoint(HttpMethod.Get, path).Endpoint,
				RequestHelper.Request(HttpMethod.Get, path));

			Assert.Equal(statusCode, response.StatusCode);
			Assert.Equal(message, await ResponseHelper.Body(response));
		}
	}
}
