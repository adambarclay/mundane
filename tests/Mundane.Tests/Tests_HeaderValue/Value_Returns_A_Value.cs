using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue;

[ExcludeFromCodeCoverage]
public static class Value_Returns_A_Value
{
	[Fact]
	public static void Which_Was_Passed_To_The_Constructor()
	{
		var value = Guid.NewGuid().ToString();

		Assert.Equal(value, new HeaderValue(Guid.NewGuid().ToString(), value).Value);
	}
}
