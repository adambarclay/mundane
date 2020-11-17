using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue
{
	[ExcludeFromCodeCoverage]
	public static class Constructor_Throws_ArgumentException
	{
		[Fact]
		public static void When_The_Name_Parameter_Is_Empty()
		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => new HeaderValue(string.Empty, Guid.NewGuid().ToString()));

			Assert.Equal("name", exception.ParamName);
			Assert.StartsWith("Header name must have a value.", exception.Message, StringComparison.Ordinal);
		}

		[Fact]
		public static void When_The_Name_Parameter_Is_Whitespace()
		{
			var exception = Assert.ThrowsAny<ArgumentException>(() => new HeaderValue("  ", Guid.NewGuid().ToString()));

			Assert.Equal("name", exception.ParamName);
			Assert.StartsWith("Header name must have a value.", exception.Message, StringComparison.Ordinal);
		}
	}
}
