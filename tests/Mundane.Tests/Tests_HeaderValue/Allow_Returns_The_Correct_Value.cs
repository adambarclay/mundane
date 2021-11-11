using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue;

[ExcludeFromCodeCoverage]
public static class Allow_Returns_The_Correct_Value
{
	[Fact]
	public static void When_The_Allowed_Methods_Have_Been_Supplied()
	{
		var allowedMethods = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };

		var header = HeaderValue.Allow(allowedMethods);

		Assert.Equal("allow", header.Name);
		Assert.Equal(string.Join(",", allowedMethods), header.Value);
	}
}
