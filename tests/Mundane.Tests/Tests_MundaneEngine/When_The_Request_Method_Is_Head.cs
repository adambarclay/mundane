using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_MundaneEngine
{
	[ExcludeFromCodeCoverage]
	public static class When_The_Request_Method_Is_Head
	{
		[Fact]
		public static async Task The_Response_Body_Generation_Process_Is_Not_Invoked()
		{
			var bodyGenerationHasRun = false;

			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpoint.Create(
					() => Response.Ok(
						o =>
						{
							bodyGenerationHasRun = true;

							return ValueTask.CompletedTask;
						})),
				RequestHelper.Request(HttpMethod.Head, "/"));

			await ResponseHelper.Body(response);

			Assert.False(bodyGenerationHasRun);
		}

		[Fact]
		public static async Task The_Response_Body_Is_Empty()
		{
			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpoint.Create(() => Response.Ok(o => o.Write(Guid.NewGuid().ToString()))),
				RequestHelper.Request(HttpMethod.Head, "/"));

			Assert.Equal(string.Empty, await ResponseHelper.Body(response));
		}
	}
}
