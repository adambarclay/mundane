using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_MundaneEndpointFactory
{
	[ExcludeFromCodeCoverage]
	public static class Create_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_MundaneEndpoint_Endpoint_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() => MundaneEndpointFactory.Create((MundaneEndpoint)null!));

			Assert.Equal("endpoint", exception.ParamName!);
		}

		[Fact]
		public static void When_The_MundaneEndpointNoParameters_Endpoint_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() => MundaneEndpointFactory.Create((MundaneEndpointNoParameters)null!));

			Assert.Equal("endpoint", exception.ParamName!);
		}

		[Fact]
		public static void When_The_MundaneEndpointNoParametersSync_Endpoint_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() => MundaneEndpointFactory.Create((MundaneEndpointNoParametersSync)null!));

			Assert.Equal("endpoint", exception.ParamName!);
		}

		[Fact]
		public static void When_The_MundaneEndpointSync_Endpoint_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() => MundaneEndpointFactory.Create((MundaneEndpointSync)null!));

			Assert.Equal("endpoint", exception.ParamName!);
		}
	}
}
