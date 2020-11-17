using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_EnumerableDictionary
{
	[ExcludeFromCodeCoverage]
	public static class Operator_Equals_Returns_True
	{
		[Fact]
		public static void When_The_Dictionaries_Are_The_Same()
		{
			var dictionary = new Dictionary<string, string>();

			var first = new EnumerableDictionary<string, string>(dictionary);
			var second = new EnumerableDictionary<string, string>(dictionary);

			Assert.True(first == second);
		}
	}
}
