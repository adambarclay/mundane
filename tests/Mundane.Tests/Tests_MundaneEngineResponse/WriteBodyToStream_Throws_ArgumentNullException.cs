using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_MundaneEngineResponse;

[ExcludeFromCodeCoverage]
public static class WriteBodyToStream_Throws_ArgumentNullException
{
	[Fact]
	public static async Task When_The_Stream_Parameter_Is_Null()
	{
		var exception = await Assert.ThrowsAnyAsync<ArgumentNullException>(
			async () =>
			{
				var response = await MundaneEngine.ExecuteRequest(
					MundaneEndpointFactory.Create(Response.Ok),
					RequestHelper.Request());

				await response.WriteBodyToStream(null!);
			});

		Assert.Equal("stream", exception.ParamName!);
	}
}
