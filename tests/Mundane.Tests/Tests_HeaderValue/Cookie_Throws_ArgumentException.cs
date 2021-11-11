using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue;

[ExcludeFromCodeCoverage]
public static class Cookie_Throws_ArgumentException
{
	[Fact]
	public static void When_The_Name_Parameter_Is_Empty()
	{
		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => HeaderValue.SessionCookie(string.Empty, "value", "/"));

			Assert.Equal("name", exception.ParamName!);
			Assert.StartsWith("The cookie name must not be empty.", exception.Message, StringComparison.Ordinal);
		}

		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => HeaderValue.SessionCookie(string.Empty, "value", "/", string.Empty));

			Assert.Equal("name", exception.ParamName!);
			Assert.StartsWith("The cookie name must not be empty.", exception.Message, StringComparison.Ordinal);
		}

		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => HeaderValue.SessionCookie(string.Empty, "value", "/", string.Empty, true, true));

			Assert.Equal("name", exception.ParamName!);
			Assert.StartsWith("The cookie name must not be empty.", exception.Message, StringComparison.Ordinal);
		}

		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => HeaderValue.PersistentCookie(string.Empty, "value", TimeSpan.MaxValue, "/"));

			Assert.Equal("name", exception.ParamName!);
			Assert.StartsWith("The cookie name must not be empty.", exception.Message, StringComparison.Ordinal);
		}

		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => HeaderValue.PersistentCookie(string.Empty, "value", TimeSpan.MaxValue, "/", string.Empty));

			Assert.Equal("name", exception.ParamName!);
			Assert.StartsWith("The cookie name must not be empty.", exception.Message, StringComparison.Ordinal);
		}

		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => HeaderValue.PersistentCookie(
					string.Empty,
					"value",
					TimeSpan.MaxValue,
					"/",
					string.Empty,
					true,
					true));

			Assert.Equal("name", exception.ParamName!);
			Assert.StartsWith("The cookie name must not be empty.", exception.Message, StringComparison.Ordinal);
		}
	}

	[Fact]
	public static void When_The_Path_Parameter_Does_Not_Begin_With_A_Forward_Slash()
	{
		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => HeaderValue.SessionCookie("name", "value", Guid.NewGuid().ToString()));

			Assert.Equal("path", exception.ParamName!);

			Assert.StartsWith("The cookie path must begin with a \"/\".", exception.Message, StringComparison.Ordinal);
		}

		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => HeaderValue.SessionCookie("name", "value", Guid.NewGuid().ToString(), string.Empty));

			Assert.Equal("path", exception.ParamName!);

			Assert.StartsWith("The cookie path must begin with a \"/\".", exception.Message, StringComparison.Ordinal);
		}

		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => HeaderValue.SessionCookie("name", "value", Guid.NewGuid().ToString(), string.Empty, true, true));

			Assert.Equal("path", exception.ParamName!);

			Assert.StartsWith("The cookie path must begin with a \"/\".", exception.Message, StringComparison.Ordinal);
		}

		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => HeaderValue.PersistentCookie("name", "value", TimeSpan.MaxValue, Guid.NewGuid().ToString()));

			Assert.Equal("path", exception.ParamName!);

			Assert.StartsWith("The cookie path must begin with a \"/\".", exception.Message, StringComparison.Ordinal);
		}

		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => HeaderValue.PersistentCookie(
					"name",
					"value",
					TimeSpan.MaxValue,
					Guid.NewGuid().ToString(),
					string.Empty));

			Assert.Equal("path", exception.ParamName!);

			Assert.StartsWith("The cookie path must begin with a \"/\".", exception.Message, StringComparison.Ordinal);
		}

		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => HeaderValue.PersistentCookie(
					"name",
					"value",
					TimeSpan.MaxValue,
					Guid.NewGuid().ToString(),
					string.Empty,
					true,
					true));

			Assert.Equal("path", exception.ParamName!);

			Assert.StartsWith("The cookie path must begin with a \"/\".", exception.Message, StringComparison.Ordinal);
		}
	}

	[Fact]
	public static void When_The_Path_Parameter_Is_Empty()
	{
		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => HeaderValue.SessionCookie("name", "value", string.Empty));

			Assert.Equal("path", exception.ParamName!);

			Assert.StartsWith("The cookie path must begin with a \"/\".", exception.Message, StringComparison.Ordinal);
		}

		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => HeaderValue.SessionCookie("name", "value", string.Empty, string.Empty));

			Assert.Equal("path", exception.ParamName!);

			Assert.StartsWith("The cookie path must begin with a \"/\".", exception.Message, StringComparison.Ordinal);
		}

		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => HeaderValue.SessionCookie("name", "value", string.Empty, string.Empty, true, true));

			Assert.Equal("path", exception.ParamName!);

			Assert.StartsWith("The cookie path must begin with a \"/\".", exception.Message, StringComparison.Ordinal);
		}

		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => HeaderValue.PersistentCookie("name", "value", TimeSpan.MaxValue, string.Empty));

			Assert.Equal("path", exception.ParamName!);

			Assert.StartsWith("The cookie path must begin with a \"/\".", exception.Message, StringComparison.Ordinal);
		}

		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => HeaderValue.PersistentCookie("name", "value", TimeSpan.MaxValue, string.Empty, string.Empty));

			Assert.Equal("path", exception.ParamName!);

			Assert.StartsWith("The cookie path must begin with a \"/\".", exception.Message, StringComparison.Ordinal);
		}

		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => HeaderValue.PersistentCookie(
					"name",
					"value",
					TimeSpan.MaxValue,
					string.Empty,
					string.Empty,
					true,
					true));

			Assert.Equal("path", exception.ParamName!);

			Assert.StartsWith("The cookie path must begin with a \"/\".", exception.Message, StringComparison.Ordinal);
		}
	}
}
