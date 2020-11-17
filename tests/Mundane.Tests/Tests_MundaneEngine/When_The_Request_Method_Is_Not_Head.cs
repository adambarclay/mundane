using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_MundaneEngine
{
	[ExcludeFromCodeCoverage]
	public static class When_The_Request_Method_Is_Not_Head
	{
		[Fact]
		public static async Task The_Response_Body_Generation_Process_Is_Invoked()
		{
			var bodyGenerationHasRun = false;

			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpoint.Create(
					() => Response.Ok(
						o =>
						{
							bodyGenerationHasRun = true;

							return Task.CompletedTask;
						})),
				RequestHelper.Request(HttpMethod.Get, "/"));

			await ResponseHelper.Body(response);

			Assert.True(bodyGenerationHasRun);
		}

		[Fact]
		public static async Task The_Response_Body_Is_Not_Empty()
		{
			var output = Guid.NewGuid().ToString();

			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpoint.Create(() => Response.Ok(o => o.Write(output))),
				RequestHelper.Request(HttpMethod.Get, "/"));

			Assert.Equal(output, await ResponseHelper.Body(response));
		}
	}
}
