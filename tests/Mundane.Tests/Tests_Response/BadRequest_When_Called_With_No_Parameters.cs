using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_Response
{
	[ExcludeFromCodeCoverage]
	public static class BadRequest_When_Called_With_No_Parameters
	{
		[Fact]
		public static async Task Does_Not_Set_Any_Headers()
		{
			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpointFactory.Create(Response.BadRequest),
				RequestHelper.Request());

			Assert.Empty(response.Headers);
		}

		[Fact]
		public static async Task Sets_The_BodyWriter_To_Return_Empty()
		{
			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpointFactory.Create(Response.BadRequest),
				RequestHelper.Request(HttpMethod.Get, "/"));

			Assert.Equal(string.Empty, await ResponseHelper.Body(response));
		}

		[Fact]
		public static async Task Sets_The_Status_Code_To_400()
		{
			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpointFactory.Create(Response.BadRequest),
				RequestHelper.Request());

			Assert.Equal(400, response.StatusCode);
		}
	}
}
