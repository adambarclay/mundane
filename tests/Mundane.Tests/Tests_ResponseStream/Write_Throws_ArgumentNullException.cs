using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_ResponseStream
{
	[ExcludeFromCodeCoverage]
	public static class Write_Throws_ArgumentNullException
	{
		[Fact]
		public static async Task When_The_Byte_Array_Value_Is_Null()
		{
			var exception = await Assert.ThrowsAnyAsync<ArgumentNullException>(
				async () =>
				{
					var response = await MundaneEngine.ExecuteRequest(
						MundaneEndpoint.Create(() => Response.Ok(o => o.Write((byte[])null!))),
						RequestHelper.Request());

					await ResponseHelper.Body(response);
				});

			Assert.Equal("value", exception.ParamName);
		}

		[Fact]
		public static async Task When_The_Stream_Value_Is_Null()
		{
			var exception = await Assert.ThrowsAnyAsync<ArgumentNullException>(
				async () =>
				{
					var response = await MundaneEngine.ExecuteRequest(
						MundaneEndpoint.Create(() => Response.Ok(o => o.Write((Stream)null!))),
						RequestHelper.Request());

					await ResponseHelper.Body(response);
				});

			Assert.Equal("value", exception.ParamName);
		}

		[Fact]
		public static async Task When_The_String_Value_Is_Null()
		{
			var exception = await Assert.ThrowsAnyAsync<ArgumentNullException>(
				async () =>
				{
					var response = await MundaneEngine.ExecuteRequest(
						MundaneEndpoint.Create(() => Response.Ok(o => o.Write((string)null!))),
						RequestHelper.Request());

					await ResponseHelper.Body(response);
				});

			Assert.Equal("value", exception.ParamName);
		}
	}
}
