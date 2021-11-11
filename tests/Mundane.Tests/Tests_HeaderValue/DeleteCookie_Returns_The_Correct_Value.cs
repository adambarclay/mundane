using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue;

[ExcludeFromCodeCoverage]
public static class DeleteCookie_Returns_The_Correct_Value
{
	[Fact]
	public static void When_The_Name_Is_Supplied()
	{
		var name = Guid.NewGuid().ToString();

		var cookie = HeaderValue.DeleteCookie(name);

		var expected = name + "=;path=/;max-age=0;secure;httponly";

		Assert.Equal("set-cookie", cookie.Name);
		Assert.Equal(expected, cookie.Value);
	}
}
