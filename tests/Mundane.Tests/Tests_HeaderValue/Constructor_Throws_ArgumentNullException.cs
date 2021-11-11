using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue;

[ExcludeFromCodeCoverage]
public static class Constructor_Throws_ArgumentNullException
{
	[Fact]
	public static void When_The_Name_Parameter_Is_Null()
	{
		var exception = Assert.ThrowsAny<ArgumentNullException>(
			() => new HeaderValue(null!, Guid.NewGuid().ToString()));

		Assert.Equal("name", exception.ParamName!);
	}

	[Fact]
	public static void When_The_Value_Parameter_Is_Null()
	{
		var exception = Assert.ThrowsAny<ArgumentNullException>(
			() => new HeaderValue(Guid.NewGuid().ToString(), null!));

		Assert.Equal("value", exception.ParamName!);
	}
}
