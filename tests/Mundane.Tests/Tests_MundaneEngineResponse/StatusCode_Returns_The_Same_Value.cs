using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_MundaneEngineResponse
{
	[ExcludeFromCodeCoverage]
	public static class StatusCode_Returns_The_Same_Value
	{
		[Fact]
		public static async Task As_Supplied_To_The_Response()
		{
			var statusCode = new Random().Next();

			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpoint.Create(() => new Response(statusCode)),
				RequestHelper.Request());

			Assert.Equal(statusCode, response.StatusCode);
		}
	}
}
