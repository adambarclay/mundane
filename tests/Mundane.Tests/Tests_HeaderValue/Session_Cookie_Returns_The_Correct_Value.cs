using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue
{
	[ExcludeFromCodeCoverage]
	public static class Session_Cookie_Returns_The_Correct_Value
	{
		[Fact]
		public static void When_Domain_Has_Been_Set()
		{
			var name = Guid.NewGuid().ToString();
			var value = Guid.NewGuid().ToString();
			var domain = Guid.NewGuid().ToString();

			var cookie = HeaderValue.SessionCookie(name, value, "/", domain);

			var expected = $"{name}={value};path=/;domain={domain};secure;httponly";

			Assert.Equal("set-cookie", cookie.Name);
			Assert.Equal(expected, cookie.Value);
		}

		[Fact]
		public static void When_HttpOnly_Has_Been_Set_To_False()
		{
			var name = Guid.NewGuid().ToString();
			var value = Guid.NewGuid().ToString();

			var cookie = HeaderValue.SessionCookie(name, value, "/", string.Empty, false, true);

			var expected = $"{name}={value};path=/;secure";

			Assert.Equal("set-cookie", cookie.Name);
			Assert.Equal(expected, cookie.Value);
		}

		[Fact]
		public static void When_Only_The_Required_Properties_Have_Been_Set()
		{
			var name = Guid.NewGuid().ToString();
			var value = Guid.NewGuid().ToString();

			var cookie = HeaderValue.SessionCookie(name, value);

			var expected = $"{name}={value};path=/;secure;httponly";

			Assert.Equal("set-cookie", cookie.Name);
			Assert.Equal(expected, cookie.Value);
		}

		[Fact]
		public static void When_Path_Has_Been_Set()
		{
			var name = Guid.NewGuid().ToString();
			var value = Guid.NewGuid().ToString();
			var path = "/" + Guid.NewGuid();

			var cookie = HeaderValue.SessionCookie(name, value, path);

			var expected = $"{name}={value};path={path};secure;httponly";

			Assert.Equal("set-cookie", cookie.Name);
			Assert.Equal(expected, cookie.Value);
		}

		[Fact]
		public static void When_Secure_Has_Been_Set_To_False()
		{
			var name = Guid.NewGuid().ToString();
			var value = Guid.NewGuid().ToString();

			var cookie = HeaderValue.SessionCookie(name, value, "/", string.Empty, true, false);

			var expected = $"{name}={value};path=/;httponly";

			Assert.Equal("set-cookie", cookie.Name);
			Assert.Equal(expected, cookie.Value);
		}
	}
}
