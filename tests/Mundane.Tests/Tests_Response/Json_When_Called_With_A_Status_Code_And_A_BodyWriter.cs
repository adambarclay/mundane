using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_Response;

[ExcludeFromCodeCoverage]
public static class Json_When_Called_With_A_Status_Code_And_A_BodyWriter
{
	[Fact]
	public static async Task Adds_The_Content_Type_Header_For_Json()
	{
		var response = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(
				() => Response.Json(RandomNumberGenerator.GetInt32(0, int.MaxValue), _ => ValueTask.CompletedTask)),
			RequestHelper.Request());

		Assert.Single(response.Headers);

		Assert.Equal("application/json", response.Headers.First(o => o.Name == "content-type").Value);
	}

	[Fact]
	public static async Task Sets_The_BodyWriter_To_The_Value_Passed_To_It()
	{
		var output = Guid.NewGuid().ToString();

		var response = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(
				() => Response.Json(RandomNumberGenerator.GetInt32(0, int.MaxValue), o => o.Write(output))),
			RequestHelper.Request());

		Assert.Equal(output, await ResponseHelper.Body(response));
	}

	[Fact]
	public static async Task Sets_The_Status_Code_To_The_Value_Passed_To_It()
	{
		var statusCode = RandomNumberGenerator.GetInt32(0, int.MaxValue);

		var response = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(() => Response.Json(statusCode, _ => ValueTask.CompletedTask)),
			RequestHelper.Request());

		Assert.Equal(statusCode, response.StatusCode);
	}
}
