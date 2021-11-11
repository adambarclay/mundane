using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue;

[ExcludeFromCodeCoverage]
public static class IfModifiedSince_Returns_The_Correct_DateTime
{
	[Fact]
	public static void When_The_Header_Contains_A_Valid_Value()
	{
		var date = DateTime.UtcNow;

		date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);

		var headers = new Dictionary<string, string>
		{
			{ "if-modified-since", date.ToString("ddd, dd MM yyyy HH:mm:ss 'GMT'", CultureInfo.InvariantCulture) }
		};

		var request = RequestHelper.Request(HttpMethod.Get, "/", new Dictionary<string, string>(0), headers);

		var value = HeaderValue.IfModifiedSince(request);

		Assert.Equal(date, value);
	}
}
