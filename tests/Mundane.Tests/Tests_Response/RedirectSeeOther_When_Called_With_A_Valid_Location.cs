using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_Response;

[ExcludeFromCodeCoverage]
public static class RedirectSeeOther_When_Called_With_A_Valid_Location
{
	[Fact]
	public static async Task Adds_The_Content_Type_Header_For_Html()
	{
		var location = Guid.NewGuid().ToString();

		var response = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(() => Response.RedirectSeeOther(location)),
			RequestHelper.Request());

		Assert.Single(response.Headers);
		Assert.Equal(location, response.Headers.First(o => o.Name == "location").Value);
	}

	[Fact]
	public static async Task Sets_The_Status_Code_To_303()
	{
		var response = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(() => Response.RedirectSeeOther("http://example.com")),
			RequestHelper.Request());

		Assert.Equal(303, response.StatusCode);
	}
}
