using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue;

[ExcludeFromCodeCoverage]
public static class DeleteCookie_Throws_ArgumentException
{
	[Fact]
	public static void When_The_Name_Parameter_Is_Empty()
	{
		var exception = Assert.ThrowsAny<ArgumentException>(() => HeaderValue.DeleteCookie(string.Empty));

		Assert.Equal("name", exception.ParamName!);
		Assert.StartsWith("The cookie name must not be empty.", exception.Message, StringComparison.Ordinal);
	}
}
