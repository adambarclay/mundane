using System;
using System.Diagnostics.CodeAnalysis;
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

			var message = Guid.NewGuid().ToString();

			var routing = new Routing(
				o =>
				{
					o.NotFound(() => Response.NotFound(x => x.Write(message)));

					o.Get("/" + Guid.NewGuid(), () => Response.Ok(x => x.Write(Guid.NewGuid().ToString())));
				});

			var response = await MundaneEngine.ExecuteRequest(
				routing.FindEndpoint(HttpMethod.Get, path).Endpoint,
				RequestHelper.Request(HttpMethod.Get, path));

			Assert.Equal(404, response.StatusCode);
			Assert.Equal(message, await ResponseHelper.Body(response));
		}
	}
}
