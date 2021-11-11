using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue;

[ExcludeFromCodeCoverage]
public static class IfModifiedSince_Returns_DateTime_MinValue
{
	[Fact]
	public static void When_No_Header_Has_Been_Supplied()
	{
		var request = RequestHelper.Request();

		var value = HeaderValue.IfModifiedSince(request);

		Assert.Equal(DateTime.MinValue, value);
	}

	[Fact]
	public static void When_The_Header_Is_Malformed()
	{
		var request = RequestHelper.Request(
			HttpMethod.Get,
			"/",
			new Dictionary<string, string>(0),
			new Dictionary<string, string> { { "if-modified-since", Guid.NewGuid().ToString() } });

		var value = HeaderValue.IfModifiedSince(request);

		Assert.Equal(DateTime.MinValue, value);
	}
}
