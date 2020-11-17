using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_MundaneEngineResponse
{
	[ExcludeFromCodeCoverage]
	public static class WriteBodyToStream_Throws_EndpointReturnedNull
	{
		[Fact]
		public static async Task When_The_Body_Writer_Returns_A_Null_Task()
		{
			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpoint.Create(() => Response.Ok(o => null!)),
				RequestHelper.Request());

			var exception = await Assert.ThrowsAnyAsync<EndpointReturnedNull>(
				async () =>
				{
					await using (var stream = new MemoryStream())
					{
						await response.WriteBodyToStream(stream);
					}
				});

			Assert.Equal("The response body writer returned a null Task.", exception.Message);
		}
	}
}
