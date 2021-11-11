using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_MundaneEngineResponse;

[ExcludeFromCodeCoverage]
public static class StatusCode_Returns_The_Same_Value
{
	[Fact]
	public static async Task As_Supplied_To_The_Response()
	{
		var statusCode = RandomNumberGenerator.GetInt32(0, int.MaxValue);

		var response = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(() => new Response(statusCode)),
			RequestHelper.Request());

		Assert.Equal(statusCode, response.StatusCode);
	}
}
