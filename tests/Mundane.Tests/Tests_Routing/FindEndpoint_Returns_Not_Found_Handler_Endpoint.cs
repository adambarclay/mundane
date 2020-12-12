using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_Routing
{
	[ExcludeFromCodeCoverage]
	public static class FindEndpoint_Returns_Not_Found_Handler_Endpoint
	{
		[Fact]
		public static async Task When_The_Endpoint_Has_Not_Been_Registered()
		{
			var path = "/" + Guid.NewGuid();

			var statusCode = RandomNumberGenerator.GetInt32(0, int.MaxValue);
			var message = Guid.NewGuid().ToString();

			var routing = new Routing(
				o => o.Get("/" + Guid.NewGuid(), () => Response.Ok(x => x.Write(Guid.NewGuid().ToString()))),
				MundaneEndpoint.Create(() => new Response(statusCode, x => x.Write(message))));

			var response = await MundaneEngine.ExecuteRequest(
				routing.FindEndpoint(HttpMethod.Get, path).Endpoint,
				RequestHelper.Request(HttpMethod.Get, path));

			Assert.Equal(statusCode, response.StatusCode);
			Assert.Equal(message, await ResponseHelper.Body(response));
		}
	}
}
