using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_Response;

[ExcludeFromCodeCoverage]
public static class MethodNotAllowed_When_Called_With_Valid_Allowed_Methods
{
	[Fact]
	public static async Task Adds_The_Allow_Header_With_The_Allowed_Methods()
	{
		var allowedMethods = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };

		var response = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(() => Response.MethodNotAllowed(allowedMethods)),
			RequestHelper.Request());

		Assert.Single(response.Headers);

		Assert.Equal(string.Join(",", allowedMethods), response.Headers.First(o => o.Name == "allow").Value);
	}

	[Fact]
	public static async Task Sets_The_Status_Code_To_405()
	{
		var response = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(
				() => Response.MethodNotAllowed(
					new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() })),
			RequestHelper.Request());

		Assert.Equal(405, response.StatusCode);
	}
}
