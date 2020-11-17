using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_MundaneEngineResponse
{
	[ExcludeFromCodeCoverage]
	public static class WriteBodyToStream_Writes_Body_Contents_To_The_Stream
	{
		[Fact]
		public static async Task When_Something_Is_Written_To_The_Response_Stream()
		{
			var output = Guid.NewGuid().ToString();

			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpoint.Create(() => Response.Ok(o => o.Write(output))),
				RequestHelper.Request());

			Assert.Equal(output, await ResponseHelper.Body(response));
		}
	}
}
