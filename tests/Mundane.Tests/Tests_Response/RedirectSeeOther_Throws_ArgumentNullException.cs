using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Response;

[ExcludeFromCodeCoverage]
public static class RedirectSeeOther_Throws_ArgumentNullException
{
	[Fact]
	public static void When_The_Location_Parameter_Is_Null()
	{
		var exception = Assert.ThrowsAny<ArgumentNullException>(() => Response.RedirectSeeOther(null!));

		Assert.Equal("location", exception.ParamName!);
	}
}
