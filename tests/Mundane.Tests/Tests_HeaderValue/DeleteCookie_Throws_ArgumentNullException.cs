using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue;

[ExcludeFromCodeCoverage]
public static class DeleteCookie_Throws_ArgumentNullException
{
	[Fact]
	public static void When_The_Name_Parameter_Is_Null()
	{
		var exception = Assert.ThrowsAny<ArgumentNullException>(() => HeaderValue.DeleteCookie(null!));

		Assert.Equal("name", exception.ParamName!);
	}
}
