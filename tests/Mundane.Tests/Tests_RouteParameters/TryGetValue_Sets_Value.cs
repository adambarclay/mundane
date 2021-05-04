using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_RouteParameters
{
	[ExcludeFromCodeCoverage]
	public static class TryGetValue_Sets_Value
	{
		[Fact]
		public static void To_The_Value_In_The_Collection_When_The_Key_Exists()
		{
			var key = Guid.NewGuid().ToString();
			var value = Guid.NewGuid().ToString();

			var routeParameters = new RouteParameters(new Dictionary<string, string> { { key, value } });

			routeParameters.TryGetValue(key, out var actualValue);

			Assert.Equal(value, actualValue!);
		}
	}
}
