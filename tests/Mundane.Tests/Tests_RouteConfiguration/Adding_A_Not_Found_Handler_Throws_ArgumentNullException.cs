using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_RouteConfiguration
{
	[ExcludeFromCodeCoverage]
	public static class Adding_A_Not_Found_Handler_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_Endpoint_Is_Null()
		{
			var noParametersSync = Adding_A_Not_Found_Handler_Throws_ArgumentNullException
				.NullEndpointDelegate<MundaneEndpointDelegateNoParametersSync>();

			var endpointSync = Adding_A_Not_Found_Handler_Throws_ArgumentNullException
				.NullEndpointDelegate<MundaneEndpointDelegateSync>();

			var noParameters = Adding_A_Not_Found_Handler_Throws_ArgumentNullException
				.NullEndpointDelegate<MundaneEndpointDelegateNoParameters>();

			var endpoint = Adding_A_Not_Found_Handler_Throws_ArgumentNullException
				.NullEndpointDelegate<MundaneEndpointDelegate>();

			Action<ArgumentNullException> check = exception => Assert.Equal("endpoint", exception.ParamName!);

			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.NotFound(noParametersSync))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.NotFound(endpointSync))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.NotFound(noParameters))));
			check(Assert.ThrowsAny<ArgumentNullException>(() => new Routing(o => o.NotFound(endpoint))));
		}

		private static T NullEndpointDelegate<T>()
			where T : class
		{
			return null!;
		}
	}
}
