using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_ResponseStream;

[ExcludeFromCodeCoverage]
public static class Write_Writes_The_Correct_Byte_Sequence_To_The_Stream
{
	[Fact]
	public static async Task When_The_Value_Is_A_Byte_Array()
	{
		const string expected = "Test String";

		var response = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(() => Response.Ok(o => o.Write(Encoding.UTF8.GetBytes(expected)))),
			RequestHelper.Request());

		Assert.Equal(expected, await ResponseHelper.Body(response));
	}

	[Fact]
	public static async Task When_The_Value_Is_A_Stream()
	{
		const string expected = "Test String";

		var response = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(
				() => Response.Ok(
					async o =>
					{
						await using (var value = new MemoryStream(Encoding.UTF8.GetBytes(expected)))
						{
							await o.Write(value);
						}
					})),
			RequestHelper.Request());

		Assert.Equal(expected, await ResponseHelper.Body(response));
	}

	[Fact]
	public static async Task When_The_Value_Is_A_String()
	{
		const string expected = "Test String";

		var response = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(() => Response.Ok(o => o.Write(expected))),
			RequestHelper.Request());

		Assert.Equal(expected, await ResponseHelper.Body(response));
	}
}
