using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_Response
{
	[ExcludeFromCodeCoverage]
	public static class Constructor_When_Called_With_A_Status_Code
	{
		[Fact]
		public static async Task Does_Not_Set_Any_Headers()
		{
			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpoint.Create(() => new Response(new Random().Next())),
				RequestHelper.Request());

			Assert.Empty(response.Headers);
		}

		[Fact]
		public static async Task Sets_The_BodyWriter_To_Return_Empty()
		{
			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpoint.Create(() => new Response(new Random().Next())),
				RequestHelper.Request(HttpMethod.Get, "/"));

			Assert.Equal(string.Empty, await ResponseHelper.Body(response));
		}

		[Fact]
		public static async Task Sets_The_Status_Code_To_The_Value_Passed_To_It()
		{
			var statusCode = new Random().Next();

			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpoint.Create(() => new Response(statusCode)),
				RequestHelper.Request());

			Assert.Equal(statusCode, response.StatusCode);
		}
	}
}
