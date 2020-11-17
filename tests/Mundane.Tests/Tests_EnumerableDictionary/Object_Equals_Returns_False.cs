using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_EnumerableDictionary
{
	[ExcludeFromCodeCoverage]
	public static class Object_Equals_Returns_False
	{
		[Fact]
		public static void When_The_Dictionaries_Are_Different()
		{
			var first = new EnumerableDictionary<string, string>(new Dictionary<string, string>());
			var second = new EnumerableDictionary<string, string>(new Dictionary<string, string>());

			Assert.False(first.Equals((object)second));
		}

		[Fact]
		public static void When_The_Other_Object_Is_Not_An_EnumerableDictionary()
		{
			var dictionary = new Dictionary<string, string>();

			var first = new EnumerableDictionary<string, string>(dictionary);
			var second = new { dictionary };

			Assert.False(first.Equals(second));
		}
	}
}
