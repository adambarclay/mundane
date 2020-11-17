using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_MundaneEngine
{
	[ExcludeFromCodeCoverage]
	public static class ExecuteRequest_Throws_EndpointReturnedNull
	{
		[Fact]
		public static async Task When_The_Endpoint_Returns_A_Null_Task()
		{
			var exception = await Assert.ThrowsAnyAsync<EndpointReturnedNull>(
				() => MundaneEngine.ExecuteRequest(
					MundaneEndpoint.Create((MundaneEndpointDelegateNoParameters)(() => null!)),
					RequestHelper.Request()));

			Assert.Equal("The endpoint returned a null Task<Response>.", exception.Message);
		}

		[Fact]
		public static async Task When_The_Endpoint_Returns_A_Task_Which_Returns_Null()
		{
			var exception = await Assert.ThrowsAnyAsync<EndpointReturnedNull>(
				() => MundaneEngine.ExecuteRequest(
					MundaneEndpoint.Create((MundaneEndpointDelegateNoParametersSync)(() => null!)),
					RequestHelper.Request()));

			Assert.Equal("The endpoint returned a null Response.", exception.Message);
		}
	}
}
