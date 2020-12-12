using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_MundaneEndpoint
{
	[ExcludeFromCodeCoverage]
	public static class Create_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_MundaneEndpointDelegate_Endpoint_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() => MundaneEndpoint.Create((MundaneEndpointDelegate)null!));

			Assert.Equal("endpoint", exception.ParamName!);
		}

		[Fact]
		public static void When_The_MundaneEndpointDelegateNoParameters_Endpoint_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() => MundaneEndpoint.Create((MundaneEndpointDelegateNoParameters)null!));

			Assert.Equal("endpoint", exception.ParamName!);
		}

		[Fact]
		public static void When_The_MundaneEndpointDelegateNoParametersSync_Endpoint_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() => MundaneEndpoint.Create((MundaneEndpointDelegateNoParametersSync)null!));

			Assert.Equal("endpoint", exception.ParamName!);
		}

		[Fact]
		public static void When_The_MundaneEndpointDelegateSync_Endpoint_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() => MundaneEndpoint.Create((MundaneEndpointDelegateSync)null!));

			Assert.Equal("endpoint", exception.ParamName!);
		}
	}
}
