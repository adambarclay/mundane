using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue
{
	[ExcludeFromCodeCoverage]
	public static class Persistent_Cookie_Returns_The_Correct_Value
	{
		[Fact]
		public static void When_Domain_Has_Been_Set()
		{
			var name = Guid.NewGuid().ToString();
			var value = Guid.NewGuid().ToString();
			var maxAge = TimeSpan.FromSeconds(new Random().Next());
			var domain = Guid.NewGuid().ToString();

			var cookie = HeaderValue.PersistentCookie(name, value, maxAge, "/", domain);

			var expected = name +
				"=" +
				value +
				";path=/;max-age=" +
				maxAge.TotalSeconds +
				";domain=" +
				domain +
				";secure;httponly";

			Assert.Equal("set-cookie", cookie.Name);
			Assert.Equal(expected, cookie.Value);
		}

		[Fact]
		public static void When_HttpOnly_Has_Been_Set_To_False()
		{
			var name = Guid.NewGuid().ToString();
			var value = Guid.NewGuid().ToString();
			var maxAge = TimeSpan.FromSeconds(new Random().Next());

			var cookie = HeaderValue.PersistentCookie(name, value, maxAge, "/", string.Empty, false, true);

			var expected = name + "=" + value + ";path=/;max-age=" + maxAge.TotalSeconds + ";secure";

			Assert.Equal("set-cookie", cookie.Name);
			Assert.Equal(expected, cookie.Value);
		}

		[Fact]
		public static void When_Only_The_Required_Properties_Have_Been_Set()
		{
			var name = Guid.NewGuid().ToString();
			var value = Guid.NewGuid().ToString();
			var maxAge = TimeSpan.FromSeconds(new Random().Next());

			var cookie = HeaderValue.PersistentCookie(name, value, maxAge);

			var expected = name + "=" + value + ";path=/;max-age=" + maxAge.TotalSeconds + ";secure;httponly";

			Assert.Equal("set-cookie", cookie.Name);
			Assert.Equal(expected, cookie.Value);
		}

		[Fact]
		public static void When_Path_Has_Been_Set()
		{
			var name = Guid.NewGuid().ToString();
			var value = Guid.NewGuid().ToString();
			var maxAge = TimeSpan.FromSeconds(new Random().Next());
			var path = "/" + Guid.NewGuid();

			var cookie = HeaderValue.PersistentCookie(name, value, maxAge, path);

			var expected = name +
				"=" +
				value +
				";path=" +
				path +
				";max-age=" +
				maxAge.TotalSeconds +
				";secure;httponly";

			Assert.Equal("set-cookie", cookie.Name);
			Assert.Equal(expected, cookie.Value);
		}

		[Fact]
		public static void When_Secure_Has_Been_Set_To_False()
		{
			var name = Guid.NewGuid().ToString();
			var value = Guid.NewGuid().ToString();
			var maxAge = TimeSpan.FromSeconds(new Random().Next());

			var cookie = HeaderValue.PersistentCookie(name, value, maxAge, "/", string.Empty, true, false);

			var expected = name + "=" + value + ";path=/;max-age=" + maxAge.TotalSeconds + ";httponly";

			Assert.Equal("set-cookie", cookie.Name);
			Assert.Equal(expected, cookie.Value);
		}
	}
}
