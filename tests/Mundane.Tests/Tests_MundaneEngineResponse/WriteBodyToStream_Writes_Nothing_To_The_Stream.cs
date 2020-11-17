using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_MundaneEngineResponse
{
	[ExcludeFromCodeCoverage]
	public static class WriteBodyToStream_Writes_Nothing_To_The_Stream
	{
		[Fact]
		public static async Task When_Nothing_Is_Written_To_The_Response_Stream()
		{
			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpoint.Create(Response.Ok),
				RequestHelper.Request());

			Assert.Equal(string.Empty, await ResponseHelper.Body(response));
		}
	}
}
