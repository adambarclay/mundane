using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_ResponseStream;

[ExcludeFromCodeCoverage]
public static class Request_Returns_The_Request
{
	[Fact]
	public static async Task Which_Was_Passed_To_The_Constructor()
	{
		var expected = RequestHelper.Request();

		Request? actual = null;

		var response = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(
				() => Response.Ok(
					o =>
					{
						actual = o.Request;

						return ValueTask.CompletedTask;
					})),
			expected);

		await using (var stream = new MemoryStream())
		{
			await response.WriteBodyToStream(stream);
		}

		Assert.Equal(expected, actual);
	}
}
