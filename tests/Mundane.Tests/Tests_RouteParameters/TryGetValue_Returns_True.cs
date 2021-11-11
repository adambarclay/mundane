using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_RouteParameters;

[ExcludeFromCodeCoverage]
public static class TryGetValue_Returns_True
{
	[Fact]
	public static void When_The_Key_Exists()
	{
		var key = Guid.NewGuid().ToString();

		var routeParameters =
			new RouteParameters(new Dictionary<string, string> { { key, Guid.NewGuid().ToString() } });

		Assert.True(routeParameters.TryGetValue(key, out var _));
	}
}
