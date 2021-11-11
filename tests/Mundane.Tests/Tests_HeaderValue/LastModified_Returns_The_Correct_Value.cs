using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue;

[ExcludeFromCodeCoverage]
public static class LastModified_Returns_The_Correct_Value
{
	[Fact]
	public static void When_The_Last_Modified_Date_Has_Been_Supplied()
	{
		var lastModified = DateTime.UtcNow;

		var header = HeaderValue.LastModified(lastModified);

		var expectedValue = lastModified.ToString("ddd, dd MM yyyy HH:mm:ss 'GMT'", CultureInfo.InvariantCulture);

		Assert.Equal("last-modified", header.Name);
		Assert.Equal(expectedValue, header.Value);
	}
}
