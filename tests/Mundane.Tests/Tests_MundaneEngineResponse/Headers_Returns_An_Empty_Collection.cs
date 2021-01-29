using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_MundaneEngineResponse
{
	[ExcludeFromCodeCoverage]
	public static class Headers_Returns_An_Empty_Collection
	{
		[Fact]
		public static async Task When_No_Headers_Have_Been_Set()
		{
			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpointFactory.Create(Response.Ok),
				RequestHelper.Request());

			Assert.Empty(response.Headers);
		}
	}
}
