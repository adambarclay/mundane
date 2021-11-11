using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_MundaneEngineResponse;

[ExcludeFromCodeCoverage]
public static class Headers_Returns_The_Same_Values
{
	[Fact]
	public static async Task As_Those_Added_To_The_Response()
	{
		var headers = new[]
		{
			new HeaderValue(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
			new HeaderValue(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
			new HeaderValue(Guid.NewGuid().ToString(), Guid.NewGuid().ToString())
		};

		var response = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(
				() => Response.Ok().AddHeader(headers[0]).AddHeader(headers[1]).AddHeader(headers[2])),
			RequestHelper.Request());

		Assert.Equal(headers, response.Headers);
	}
}
