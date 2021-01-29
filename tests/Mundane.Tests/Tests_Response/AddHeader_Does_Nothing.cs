using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_Response
{
	[ExcludeFromCodeCoverage]
	public static class AddHeader_Does_Nothing
	{
		[Fact]
		public static async Task When_HeaderValue_Has_Not_Been_Initialised()
		{
			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpointFactory.Create(() => Response.Ok().AddHeader(default)),
				RequestHelper.Request());

			Assert.Empty(response.Headers);
		}
	}
}
