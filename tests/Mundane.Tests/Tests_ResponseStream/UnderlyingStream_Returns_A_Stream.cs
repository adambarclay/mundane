using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_ResponseStream;

[ExcludeFromCodeCoverage]
public static class UnderlyingStream_Returns_A_Stream
{
	[Fact]
	public static async Task Which_Was_Passed_To_The_Constructor()
	{
		await using (var expected = new MemoryStream())
		{
			Stream actual = null!;

			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpointFactory.Create(
					() => Response.Ok(
						o =>
						{
							actual = o.Stream;

							return ValueTask.CompletedTask;
						})),
				RequestHelper.Request());

			await response.WriteBodyToStream(expected);

			Assert.Equal(expected, actual);
		}
	}
}
