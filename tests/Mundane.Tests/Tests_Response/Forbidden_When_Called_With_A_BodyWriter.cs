using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_Response;

[ExcludeFromCodeCoverage]
public static class Forbidden_When_Called_With_A_BodyWriter
{
	[Fact]
	public static async Task Adds_The_Content_Type_Header_For_Html()
	{
		var response = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(() => Response.Forbidden(_ => ValueTask.CompletedTask)),
			RequestHelper.Request());

		Assert.Single(response.Headers);

		Assert.Equal("text/html;charset=utf-8", response.Headers.First(o => o.Name == "content-type").Value);
	}

	[Fact]
	public static async Task Sets_The_BodyWriter_To_The_Value_Passed_To_It()
	{
		var output = Guid.NewGuid().ToString();

		var response = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(() => Response.Forbidden(o => o.Write(output))),
			RequestHelper.Request());

		Assert.Equal(output, await ResponseHelper.Body(response));
	}

	[Fact]
	public static async Task Sets_The_Status_Code_To_403()
	{
		var response = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(() => Response.Forbidden(_ => ValueTask.CompletedTask)),
			RequestHelper.Request());

		Assert.Equal(403, response.StatusCode);
	}
}
