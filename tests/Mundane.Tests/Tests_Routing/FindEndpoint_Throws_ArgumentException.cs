using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Routing;

[ExcludeFromCodeCoverage]
public static class FindEndpoint_Throws_ArgumentException
{
	[Fact]
	public static void When_The_Path_Does_Not_Begin_With_A_Forward_Slash()
	{
		var exception = Assert.ThrowsAny<ArgumentException>(
			() => new Routing(_ => { }).FindEndpoint(HttpMethod.Get, "path"));

		Assert.Equal("path", exception.ParamName!);

		Assert.StartsWith(
			"The path must start with a forward slash \"/\".",
			exception.Message,
			StringComparison.Ordinal);
	}

	[Fact]
	public static void When_The_Path_Is_Empty()
	{
		var exception = Assert.ThrowsAny<ArgumentException>(
			() => new Routing(_ => { }).FindEndpoint(HttpMethod.Get, string.Empty));

		Assert.Equal("path", exception.ParamName!);

		Assert.StartsWith(
			"The path must start with a forward slash \"/\".",
			exception.Message,
			StringComparison.Ordinal);
	}
}
