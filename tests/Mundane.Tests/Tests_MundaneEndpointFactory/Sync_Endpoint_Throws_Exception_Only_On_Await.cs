using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_MundaneEndpointFactory
{
	[ExcludeFromCodeCoverage]
	public static class Sync_Endpoint_Throws_Exception_Only_On_Await
	{
		[Fact]
		public static async Task With_No_Parameters()
		{
			const string message = "Custom Exception";

			var endpoint = MundaneEndpointFactory.Create(
				(MundaneEndpointNoParametersSync)(() => throw new InvalidOperationException(message)));

			var task = endpoint.Invoke(RequestHelper.Request());

			var exception = await Assert.ThrowsAnyAsync<InvalidOperationException>(async () => await task);

			Assert.Equal(message, exception.Message);
		}

		[Fact]
		public static async Task With_Request()
		{
			const string message = "Custom Exception";

			var endpoint = MundaneEndpointFactory.Create(
				(MundaneEndpointSync)(_ => throw new InvalidOperationException(message)));

			var task = endpoint.Invoke(RequestHelper.Request());

			var exception = await Assert.ThrowsAnyAsync<InvalidOperationException>(async () => await task);

			Assert.Equal(message, exception.Message);
		}
	}
}
