using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_RouteParameters
{
	[ExcludeFromCodeCoverage]
	public static class TryGetValue_Returns_False
	{
		[Fact]
		public static void When_The_Key_Does_Not_Exist()
		{
			var routeParameters = new RouteParameters(
				new Dictionary<string, string> { { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() } });

			Assert.False(routeParameters.TryGetValue(Guid.NewGuid().ToString(), out var _));
		}

		/*[Fact]
		public static void When_The_RouteParameters_Is_The_Default_Value()
		{
			RouteParameters routeParameters = default;

			Assert.False(routeParameters.TryGetValue(Guid.NewGuid().ToString(), out var _));
		}*/
	}
}
