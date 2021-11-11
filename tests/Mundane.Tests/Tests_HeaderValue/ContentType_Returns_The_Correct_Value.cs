using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue;

[ExcludeFromCodeCoverage]
public static class ContentType_Returns_The_Correct_Value
{
	[Fact]
	public static void When_The_Content_Type_Has_Been_Supplied()
	{
		var contentType = Guid.NewGuid().ToString();

		var header = HeaderValue.ContentType(contentType);

		Assert.Equal("content-type", header.Name);
		Assert.Equal(contentType, header.Value);
	}
}
