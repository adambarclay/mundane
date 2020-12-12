using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue
{
	[ExcludeFromCodeCoverage]
	public static class Cookie_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_Domain_Parameter_Is_Null()
		{
			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.SessionCookie("name", "value", "/", null!));

				Assert.Equal("domain", exception.ParamName!);
			}

			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.SessionCookie("name", "value", "/", null!, true, true));

				Assert.Equal("domain", exception.ParamName!);
			}

			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.PersistentCookie("name", "value", TimeSpan.MaxValue, "/", null!));

				Assert.Equal("domain", exception.ParamName!);
			}

			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.PersistentCookie("name", "value", TimeSpan.MaxValue, "/", null!, true, true));

				Assert.Equal("domain", exception.ParamName!);
			}
		}

		[Fact]
		public static void When_The_Name_Parameter_Is_Null()
		{
			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.SessionCookie(null!, "value"));

				Assert.Equal("name", exception.ParamName!);
			}

			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.SessionCookie(null!, "value", "/"));

				Assert.Equal("name", exception.ParamName!);
			}

			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.SessionCookie(null!, "value", "/", string.Empty));

				Assert.Equal("name", exception.ParamName!);
			}

			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.SessionCookie(null!, "value", "/", string.Empty, true, true));

				Assert.Equal("name", exception.ParamName!);
			}

			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.PersistentCookie(null!, "value", TimeSpan.MaxValue));

				Assert.Equal("name", exception.ParamName!);
			}

			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.PersistentCookie(null!, "value", TimeSpan.MaxValue, "/"));

				Assert.Equal("name", exception.ParamName!);
			}

			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.PersistentCookie(null!, "value", TimeSpan.MaxValue, "/", string.Empty));

				Assert.Equal("name", exception.ParamName!);
			}

			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.PersistentCookie(
						null!,
						"value",
						TimeSpan.MaxValue,
						"/",
						string.Empty,
						true,
						true));

				Assert.Equal("name", exception.ParamName!);
			}
		}

		[Fact]
		public static void When_The_Path_Parameter_Is_Null()
		{
			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.SessionCookie("name", "value", null!));

				Assert.Equal("path", exception.ParamName!);
			}

			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.SessionCookie("name", "value", null!, string.Empty));

				Assert.Equal("path", exception.ParamName!);
			}

			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.SessionCookie("name", "value", null!, string.Empty, true, true));

				Assert.Equal("path", exception.ParamName!);
			}

			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.PersistentCookie("name", "value", TimeSpan.MaxValue, null!));

				Assert.Equal("path", exception.ParamName!);
			}

			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.PersistentCookie("name", "value", TimeSpan.MaxValue, null!, string.Empty));

				Assert.Equal("path", exception.ParamName!);
			}

			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.PersistentCookie(
						"name",
						"value",
						TimeSpan.MaxValue,
						null!,
						string.Empty,
						true,
						true));

				Assert.Equal("path", exception.ParamName!);
			}
		}

		[Fact]
		public static void When_The_Value_Parameter_Is_Null()
		{
			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(() => HeaderValue.SessionCookie("name", null!));

				Assert.Equal("value", exception.ParamName!);
			}

			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.SessionCookie("name", null!, "/"));

				Assert.Equal("value", exception.ParamName!);
			}

			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.SessionCookie("name", null!, "/", string.Empty));

				Assert.Equal("value", exception.ParamName!);
			}

			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.SessionCookie("name", null!, "/", string.Empty, true, true));

				Assert.Equal("value", exception.ParamName!);
			}

			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.PersistentCookie("name", null!, TimeSpan.MaxValue));

				Assert.Equal("value", exception.ParamName!);
			}

			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.PersistentCookie("name", null!, TimeSpan.MaxValue, "/"));

				Assert.Equal("value", exception.ParamName!);
			}

			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.PersistentCookie("name", null!, TimeSpan.MaxValue, "/", string.Empty));

				Assert.Equal("value", exception.ParamName!);
			}

			{
				var exception = Assert.ThrowsAny<ArgumentNullException>(
					() => HeaderValue.PersistentCookie(
						"name",
						null!,
						TimeSpan.MaxValue,
						"/",
						string.Empty,
						true,
						true));

				Assert.Equal("value", exception.ParamName!);
			}
		}
	}
}
